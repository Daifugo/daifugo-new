using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

public class AvatarSceneController : MonoBehaviour,SocketConnectionInterface {


	// Use this for initialization

	Transporter _tr = null;
	JToken _responseToken = null;

	void Start () {
	
		/* Get Transporter Object from previous scene */

		_tr = GameObject.Find("Transporter").GetComponent<Transporter>();
		_tr.setSocketDelegate(this);


		/* If user wants multiplayer */

		if(PlayerPrefs.HasKey("roomId"))
		{	
			Dictionary<string,object> data = new Dictionary<string,object>(){
				{"roomId", "asdfs"}
			};

			_tr.requestSelectedAvatars(data);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
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
