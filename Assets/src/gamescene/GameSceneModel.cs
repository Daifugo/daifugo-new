using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading;

public class GameSceneModel : MonoBehaviour,SocketConnectionInterface {

	public GameObject controllerObject;
	private GameSceneController _controller;

	Transporter _transporter = null;
	Mutex _dataMutex = null;

	List<Data> dataBuffer;
	Dictionary<string,object> _userId;

	// Use this for initialization

	void Start () {
	
		dataBuffer = new List<Data>();
		_dataMutex = new Mutex();

		/* Obtain transporter object */

		_transporter = GameObject.Find("Transporter").GetComponent<Transporter>();
		_transporter.setSocketDelegate (this);

		/* end transporter object */

		/* send greet message to server */

		_userId = new Dictionary<string, object> {
			{ "userId",PlayerPrefs.GetString ("userId") }
		};

		_transporter.greetServer(_userId);

		/* end send greet */

		_controller = controllerObject.GetComponent<GameSceneController>();
		_controller.setMainUserId(PlayerPrefs.GetString ("userId"));

		StartCoroutine(parseData());
	}


	IEnumerator parseData()
	{

		while(true)
		{

			while(dataBuffer.Count == 0)
				yield return null;

			
			for(var i = 0;i < dataBuffer.Count;i++)
			{

				_dataMutex.WaitOne();

				JToken d = dataBuffer[i].getData();
				int code = dataBuffer[i].getCode();

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

				}

				_dataMutex.ReleaseMutex();
			}

			dataBuffer.Clear();
		}
	}

	/* Code Handlers */

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
		_controller.deleteDealt(Id);
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

		Card[] cards = new Card[dealtCards.Count];

		for(int i = 0;i < cards.Length;i++)
		{
			JObject cardObject = (JObject)dealtCards[i];

			var suit = (int)cardObject.GetValue("_suit");
			var rank = (int)cardObject.GetValue("_kind");

			Card s = new Card(suit,rank);
			cards[i] = s;
		}

		_controller.renderDealtCard(Id,cards);

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
			foreach(var c in cards)
			{
				JObject cardObject = (JObject)c;

				var suit = (int)cardObject.GetValue("_suit");
				var rank = (int)cardObject.GetValue("_kind");

				Card s = new Card(suit,rank);
				_controller.addUserCard(s);
				yield return new WaitForSeconds(0.8f);
			}

			_transporter.requestTurn(_userId);

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

		_transporter.requestCards(_userId);
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
		dataBuffer.Add(d);

		_dataMutex.ReleaseMutex();
	}


	public void handleError()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
