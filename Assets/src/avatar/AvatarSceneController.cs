using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvatarSceneController : MonoBehaviour,SocketConnectionInterface {


	// Use this for initialization

	Transporter _tr = null;

	void Start () {
	
		/* Get Transporter Object from previous scene */

		_tr = GameObject.Find("Transporter").GetComponent<Transporter>();
		_tr.setSocketDelegate(this);


		/* If user wants multiplayer */

		// if(PlayerPrefs.HasKey("roomId"))
		// {	
			Dictionary<string,object> data = new Dictionary<string,object>(){
				{"roomId", "asdfs"}
			};

			_tr.requestSelectedAvatars(data);
		// }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void receiveData(string dt)
	{

	}

	public void handleError()
	{

	}

}
