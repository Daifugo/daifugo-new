using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

public class AvatarSceneController : MonoBehaviour,SocketConnectionInterface {


	public GameObject avatarContainer;
	public GameObject overlay;

	Transporter _tr = null;
	JToken _responseToken = null;
	int _responseCode = 0;

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

		while(true)
		{
			while(_responseToken == null)
				yield return null;


			switch(_responseCode)
			{
				case Constants.LOBBYDETAILS_CODE:

					JObject userObject = (JObject)_responseToken;
					string tempId = (string)userObject.GetValue("userId");
					PlayerPrefs.SetString ("userId", _tempId);
					SceneManager.LoadScene ("game");

				break;

				case Constants.REQUESTAVATARS_CODE:

					JArray rms = (JArray)_responseToken;
					AvatarsContainer ava = avatarContainer.GetComponent<AvatarsContainer> ();

					foreach (JToken s in rms) 
						ava.disableAvatars ((int)s);

				break;
			}

			_responseToken = null;
		}
	}


	public void receiveData(string dt)
	{
		JArray resArray = JArray.Parse (dt);
		JToken response = resArray.First["response"];

		_responseToken = response.SelectToken ("data");
		_responseCode = response.SelectToken("code");
	}

	public void handleError()
	{

	}


	/* Button Handlers */

	public void nextButtonHandler()
	{
		// Show loading overlay

		overlay.SetActive(true);


		// Send join/start request to server

		Dictionary<string,object> d = new Dictionary<string,object> ();

		AvatarsContainer avc = avatarContainer.GetComponent<AvatarsContainer> ();	
		d.Add("avatarId",avc.getIndexSelectedAvatar());

		if(PlayerPrefs.HasKey("roomId"))
		{
			d.Add("roomId",PlayerPrefs.GetString("roomId"));
			_tr.sendJoinRequest (d);
		}
		else
		{
			d.Add("mode", PlayerPrefs.GetString("mode"));
			d.Add("rules",PlayerPrefs.GetString("rules"));
			_tr.sendStartRequest(d);
		}

	}

}
