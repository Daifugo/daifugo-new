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

	/* Singleplayer scene */

	public void requestRules()
	{
		_socket.sendJSON(Constants.RULELIST_CODE,new Dictionary<string,object>());
	}
	
	/* Multiplayer scene */

	public void requestRooms()
	{
		_socket.sendJSON(Constants.ROOMLIST_CODE,new Dictionary<string,object>(){});
	}

	/* Avatar Scene */

	public void requestSelectedAvatars(Dictionary<string,object> dt)
	{
		_socket.sendJSON(Constants.REQUESTAVATARS_CODE,dt);
	}

	public void sendJoinRequest(Dictionary<string,object> dt)
	{
		_socket.sendJSON(Constants.JOINGAME_CODE,dt);
	}

}
