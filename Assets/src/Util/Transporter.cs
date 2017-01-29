using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Transporter : MonoBehaviour {

	public GameObject sockConn;

	private SocketConnection _socket = null;

	void Awake()
	{
		_socket = sockConn.GetComponent<SocketConnection> ();

		DontDestroyOnLoad (sockConn);
	}


	public void setSocketDelegate(SocketConnectionInterface i)
	{
		_socket.setDelegate (i);
	}


	public void requestRules()
	{
		_socket.sendJSON(Constants.RULELIST_CODE,new Dictionary<string,object>());
	}

}
