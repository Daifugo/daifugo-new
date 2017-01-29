using UnityEngine;
using System.Collections;

public class Transporter : MonoBehaviour {

	public GameObject sockConn;

	private SocketConnection _socket = null;

	void Awake()
	{
		_socket = sockConn.GetComponent<SocketConnection> ();

		DontDestroyOnLoad (sockConn);
	}
}
