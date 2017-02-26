using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading;

public class GameSceneModel : MonoBehaviour,SocketConnectionInterface {

	public GameObject controllerObject;
	private GameSceneController _controller;

	Mutex _dataMutex = null;
	List<Data> _dataBuffer;

	// Use this for initialization

	void Start () {
	
		_dataBuffer = new List<Data>();
		_dataMutex = new Mutex();
		_controller = controllerObject.GetComponent<GameSceneController>();


		/* Obtain and set delegate of transporter object */

		var transporter = GameObject.Find("Transporter").GetComponent<Transporter>();
		transporter.setSocketDelegate (this);

		StartCoroutine(parseData());
	}


	IEnumerator parseData()
	{

		while(true)
		{

			while(_dataBuffer.Count == 0)
				yield return null;

			
			for(var i = 0;i < _dataBuffer.Count;i++)
			{

				_dataMutex.WaitOne();

				JToken d = _dataBuffer[i].getData();
				int code = _dataBuffer[i].getCode();

				switch(code)
				{
					case Constants.NEWPLAYER_CODE:
						StartCoroutine(newPlayerHandler(d));
					break;

					case Constants.STATE_CODE:
						yield return new WaitForSeconds(1.0f);
						stateCodeHandler(d);
					break;

					case Constants.MULTIPLAYERTIME_CODE:
						multiplayerTimeHandler(d);
					break;

					case Constants.CARD_CODE:
						StartCoroutine(cardCodeHandler(d));
					break;

					case Constants.TURN_CODE:
						turnCodeHandler(d);
					break;

					case Constants.MOVE_CODE:
						moveCodeHandler(d);
					break;

					case Constants.PASSTURN_CODE:
						passTurnHandler(d);
					break;

					case Constants.INVALIDMOVE_CODE:
						invalidMoveHandler();
					break;

					case Constants.DELETEDEALT_CODE:
						deleteDealtHandler(d);
					break;

					case Constants.ROUNDWIN_CODE:
						roundWinHandler(d);
					break;

					case Constants.NEWROUND_CODE:
						newRoundHandler();
					break;

					case Constants.RULESLIST_CODE:
						rulesListHandler(d);	
					break;

				}

				_dataMutex.ReleaseMutex();
			}

			_dataBuffer.Clear();
		}
	}

	/* Code Handlers */

	void rulesListHandler(JToken data)
	{
		JObject ruleObject = (JObject)data;
		string ruleName = (string)ruleObject.GetValue("ruleName");

		_controller.showExtraRule(ruleName);
	}

	void roundWinHandler(JToken data)
	{
		JObject dataObject = (JObject)data;
		string Id = (string)dataObject.GetValue("userId");
		int photoId = (int)dataObject.GetValue("photoId");

		_controller.showRoundWin(Id,photoId);
	}


	void deleteDealtHandler(JToken data)
	{
		JObject dataObject = (JObject)data;
		string Id = (string)dataObject.GetValue("userId");
		string deleteCaller = (string)dataObject.GetValue("caller");

		_controller.deleteDealt(Id, deleteCaller);
	}


	void newRoundHandler()
	{
		_controller.showNewRound();
	}

	void invalidMoveHandler()
	{
		_controller.showInvalidMove();
	}

	void passTurnHandler(JToken dt)
	{
		JObject dataObject = (JObject)dt;
		string Id = (string)dataObject.GetValue("userId");
		int photoId = (int)dataObject.GetValue("photoId");

		_controller.showPassedTurn(Id,photoId);
	}

	void moveCodeHandler(JToken dt)
	{
		JObject dataObject = (JObject)dt;

		string Id = (string)dataObject.GetValue("userId");
		JArray dealtCards = (JArray)dataObject.GetValue("cards");
		int qty = (int)dataObject.GetValue("qty");
		

		Card[] cards = new Card[dealtCards.Count];

		for(int i = 0;i < cards.Length;i++)
		{
			JObject cardObject = (JObject)dealtCards[i];

			var suit = (int)cardObject.GetValue("_suit");
			var rank = (int)cardObject.GetValue("_kind");

			Card s = new Card(suit,rank);
			cards[i] = s;
		}

		_controller.renderDealtCard(Id,cards,qty);

	}


	void turnCodeHandler(JToken dt)
	{
		JObject dataObject = (JObject)dt;

		string prevId = null;
		string Id = null;
		int photoId = 0;

		try
		{
			Id = (string)dataObject.GetValue("userId");
			prevId = (string)dataObject.GetValue("prevTurnId");
			photoId = (int)dataObject.GetValue("photoId");
		}
		catch{}

		_controller.switchTurn(Id,photoId,prevId);
	}


	IEnumerator cardCodeHandler(JToken dt)
	{
		JObject dataObject = (JObject)dt;
		string Id = (string)dataObject.GetValue("userId");
		JToken cardsToken = dataObject.GetValue("cards");

		if(Id != PlayerPrefs.GetString ("userId"))
		{
			int cardCount = (int)cardsToken;
			_controller.addCardCount(Id,cardCount);
		}
		else
		{
			JArray cards = (JArray)cardsToken;
			Card[] c = new Card[cards.Count];

			for(int i = 0; i <cards.Count;i++)
			{
				JObject cardObject = (JObject)cards[i];

				var suit = (int)cardObject.GetValue("_suit");
				var rank = (int)cardObject.GetValue("_kind");

				Card s = new Card(suit,rank);

				c[i] = s;
			}

			_controller.addUserCard(c);
		}

		yield break;
	}


	void multiplayerTimeHandler(JToken dt)
	{
		JObject dataObject = (JObject)dt;
		int time = (int)dataObject.GetValue ("time");
		_controller.setTimer(time);
	}


	void stateCodeHandler(JToken dt)
	{

		JObject dataObject = (JObject)dt;
		int state = (int)dataObject.GetValue ("state");
		_controller.setTextState(state);

	}


	IEnumerator newPlayerHandler(JToken dt)
	{
			
		JArray plArray = (JArray)dt;

		for(int i =0;i < plArray.Count;i++)
		{
			JObject dataObject = (JObject)plArray[i];

			string userId = (string)dataObject.GetValue ("userId");
			int photoId = (int) dataObject.GetValue ("photoId");

			_controller.addPlayer(userId,photoId);

			if(i != plArray.Count -1)
				yield return new WaitForSeconds(1.2f);
		}

		yield return null;

	}	

	/* End Code Handlers */


	public void receiveData(string dt)
	{
		JArray resArray = JArray.Parse (dt);
		JToken response = resArray.First["response"];

		JToken responseToken = response.SelectToken ("data");
		int responseCode = (int)response.SelectToken("code");

		_dataMutex.WaitOne();

		var d = new Data(responseCode,responseToken);
		_dataBuffer.Add(d);

		_dataMutex.ReleaseMutex();
	}


	public void handleError()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
