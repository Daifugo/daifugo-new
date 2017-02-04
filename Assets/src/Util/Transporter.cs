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

	public void sendStartRequest(Dictionary<string,object> dt)
	{
		_socket.sendJSON(Constants.LOBBYDETAILS_CODE,dt);
	}


	/* Game Scene */

	public void greetServer(Dictionary<string,object> dt)
	{
		_socket.sendJSON(Constants.GREET_CODE,dt);
	}

	public void requestCards(Dictionary<string,object> dt)
	{
		_socket.sendJSON(Constants.CARD_CODE,dt);
	}

	public void requestTurn(Dictionary<string,object> dt)
	{
		_socket.sendJSON(Constants.TURN_CODE,dt);
	}

	public void sendPlayerMove(Dictionary<string,object> dt)
	{
		_socket.sendJSON(Constants.MOVE_CODE, dt);
	}

	public void sendPassTurn(Dictionary<string,object> dt)
	{
		_socket.sendJSON(Constants.PASSTURN_CODE, dt);
	}

}
