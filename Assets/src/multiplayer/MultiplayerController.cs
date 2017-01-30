﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class MultiplayerController : MonoBehaviour, SocketConnectionInterface {

	public GameObject transporter;
	public RuleListContainer ruleList;

	private Transporter _tr;

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

	public void receiveData(string dt)
	{
		JArray resArray = JArray.Parse (dt);
		JToken response = resArray.First["response"];

		JToken responseTkn = response.SelectToken ("data");
		int responseCd = (int)response.SelectToken("code");
	}

	public void handleError()
	{

	}


}
