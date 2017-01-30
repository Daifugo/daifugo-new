﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class MultiplayerController : MonoBehaviour, SocketConnectionInterface {

	public GameObject transporter;
	public GameObject roomList;

	private Transporter _tr;
	private JToken _responseToken = null;

	// Use this for initialization
	void Start () {

		_tr = transporter.GetComponent<Transporter>();
		_tr.setSocketDelegate(this);

		_tr.requestRooms();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/* SocketConnectionInterface */

	IEnumerator parseData()
	{

		/* Only expect data with the ROOMLIST_CODE */
		while(_responseToken == null)
			yield return null;

		JArray rms = (JArray)_responseToken;
		RoomListContainer r = roomList.GetComponent<RoomListContainer> ();

		foreach (JToken s in rms) 
		{
			Dictionary<string,object> roomData = s.ToObject<Dictionary<string,object>> ();
			r.addRooms (roomData);
		}

	}

	public void receiveData(string dt)
	{
		JArray resArray = JArray.Parse (dt);
		JToken response = resArray.First["response"];

		_responseToken = response.SelectToken ("data");
	}

	public void handleError()
	{

	}


}
