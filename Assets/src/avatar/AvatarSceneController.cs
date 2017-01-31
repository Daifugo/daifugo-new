using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

public class AvatarSceneController : MonoBehaviour,SocketConnectionInterface {


	public GameObject avatarContainer;

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

	IEnumerator parseData()
	{

		/* Only expect data with the REQUESTAVATARS_CODE */

		while(_responseToken == null)
			yield return null;

		JArray rms = (JArray)_responseToken;
		AvatarsContainer ava = avatarContainer.GetComponent<AvatarsContainer> ();

		foreach (JToken s in rms) 
		{
			ava.disableAvatars ((int)s);
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
