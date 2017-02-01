using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class GameSceneModel : MonoBehaviour,SocketConnectionInterface {

	public GameObject controllerObject;
	private GameSceneController _controller;

	Transporter _transporter = null;
	JToken _responseToken = null;
	int _responseCode = 0;


	// Use this for initialization

	void Start () {
	
		_transporter = GameObject.Find("Transporter").GetComponent<Transporter>();
		_transporter.setSocketDelegate (this);

		_controller = controllerObject.GetComponent<GameSceneController>();

		/* send greet message to server */

		Dictionary<string,object> userId = new Dictionary<string, object> {
			{ "userId",PlayerPrefs.GetString ("userId") }
		};

		_transporter.greetServer(userId);
	}


	IEnumerator parseData()
	{

		while(true)
		{
			while(_responseToken == null)
				yield return null;


			switch(_responseCode)
			{
				case Constants.NEWPLAYER_CODE:
					newPlayerHandler(_responseToken);
				break;
			}

		}

	}


	private void newPlayerHandler(JToken dt){
		
		JObject dataObject = (JObject)dt;

		string userId = (string)dataObject.GetValue ("userId");
		int photoId = (int) dataObject.GetValue ("photoId");

		_controller.addPlayer(userId,photoId);

	}	


	public void receiveData(string dt)
	{
		JArray resArray = JArray.Parse (dt);
		JToken response = resArray.First["response"];

		_responseToken = response.SelectToken ("data");
		_responseCode = (int)response.SelectToken("code");
	}


	public void handleError()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
