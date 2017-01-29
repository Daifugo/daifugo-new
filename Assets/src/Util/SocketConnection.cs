using UnityEngine;
using System.Collections;

using System.Text;
using System.IO;
using System.Collections.Generic;
using WebSocketSharp;
using Newtonsoft.Json;

public class SocketConnection : MonoBehaviour {

	private SocketConnectionInterface _delegator = null;
	private WebSocket _sock;

	void Start()
	{

		Debug.Log ("start connecting");

		_sock = new WebSocket ("ws://192.168.2.1:3000",null);

		//OnMessage event handler

		_sock.OnMessage += (sender, e) => {

			Debug.Log("receive message");

			if(e.IsText){
				Debug.Log(e.Data);
				(this._delegator).receiveData(e.Data);
			}

		};

		// OnError event handler

		_sock.OnError += (sender, e) => {
			Debug.Log("error occured");
			Debug.Log(e.Message);
		};

		_sock.ConnectAsync ();

	}


	public void setDelegate(SocketConnectionInterface i)
	{
		this._delegator = i;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
