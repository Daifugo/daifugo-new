using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSceneModel : MonoBehaviour,SocketConnectionInterface {


	Transporter _transporter = null;


	// Use this for initialization
	void Start () {
	
		_transporter = GameObject.Find("Transporter").GetComponent<Transporter>();
		_transporter.setSocketDelegate (this);


		/* send greet message to server */

		Dictionary<string,object> userId = new Dictionary<string, object> {
			{ "userId",PlayerPrefs.GetString ("userId") }
		};

		_transporter.greetServer(userId);
	}


	public void receiveData(string dt)
	{

	}


	public void handleError()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
