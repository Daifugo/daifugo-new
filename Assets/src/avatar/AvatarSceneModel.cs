using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public class AvatarSceneModel : MonoBehaviour, SocketConnectionInterface {

	public GameObject controller;
	public GameObject transporter;
	
	JToken _responseToken = null;
	int _responseCode = 0;
	Transporter _tr = null;
	AvatarSceneController _controller;

	void Start () {
		
		StartCoroutine(parseData());
		_tr = transporter.GetComponent<Transporter>();
		_tr.setSocketDelegate(this);
		
		_controller = controller.GetComponent<AvatarSceneController>();
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
					_controller.receiveUserId(tempId);

				break;

				case Constants.REQUESTAVATARS_CODE:

					JArray rms = (JArray)_responseToken;
					int[] avatars = new int[rms.Count];
					
					for(int index = 0; index < rms.Count;index++)
						avatars[index] = (int)rms[index];
					
					_controller.disableAvatar(avatars);

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
		_responseCode = (int)response.SelectToken("code");
	}

	public void handleError()
	{
		
	}
}
