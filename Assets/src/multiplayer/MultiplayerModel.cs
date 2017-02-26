using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class MultiplayerModel : MonoBehaviour, SocketConnectionInterface {


	public GameObject transporter;
	public GameObject controller;
	
	private Transporter _tr;
	private JToken _responseToken = null;
	
	
	// Use this for initialization
	void Start () {
		
		_tr = transporter.GetComponent<Transporter>();
		_tr.setSocketDelegate(this);
		
		StartCoroutine(parseData());
	
	}
	

	public void receiveData(string dt)
	{
		JArray resArray = JArray.Parse (dt);
		JToken response = resArray.First["response"];

		_responseToken = response.SelectToken ("data");
	}


	IEnumerator parseData()
	{
			
		/* Only expect data with the ROOMLIST_CODE */

		while(_responseToken == null)
			yield return null;


		JArray rms = (JArray)_responseToken;
		var rooms = new List<Dictionary<string,object>>();

		foreach (JToken s in rms) 
		{
			var roomData = s.ToObject<Dictionary<string,object>> ();
			rooms.Add(roomData);
		}


		MultiplayerController m = controller.GetComponent<MultiplayerController>();
		m.renderRooms(rooms.ToArray());
	}
	
	
	public void handleError()
	{

	}

}
