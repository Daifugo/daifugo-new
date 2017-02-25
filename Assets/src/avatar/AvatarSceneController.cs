using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class AvatarSceneController : MonoBehaviour{


	public GameObject avatarContainer;
	public GameObject overlay;
	public GameObject nextButton;

	Transporter _tr = null;


	void Start () {
	
		/* Get Transporter Object from previous scene */

		DontDestroyOnLoad(GameObject.Find("Transporter"));
		_tr = GameObject.Find("Transporter").GetComponent<Transporter>();


		/* If user wants multiplayer */

		if(PlayerPrefs.HasKey("roomId"))
		{
			Dictionary<string,object> data = new Dictionary<string,object>(){
				{"roomId", "asdfs"}
			};

			_tr.requestSelectedAvatars(data);
		}

	}
	
	// From AvatarSceneModel
	
	public void receiveUserId(string id)
	{
		PlayerPrefs.SetString ("userId", id);
		SceneManager.LoadScene("game");
	}
	
	

	public void showNextButton()
	{
		nextButton.SetActive(true);
	}


	/* Button Handlers */

	public void nextButtonHandler()
	{
		// Show loading overlay

		overlay.GetComponent<AvatarSceneOverlay>().show();


		// Send join/start request to server

		Dictionary<string,object> d = new Dictionary<string,object> ();

		AvatarsContainer avc = avatarContainer.GetComponent<AvatarsContainer> ();	
		d.Add("avatarId",avc.getIndexSelectedAvatar());

		if(PlayerPrefs.HasKey("roomId"))
		{
			overlay.GetComponent<AvatarSceneOverlay>().showJoin();
			d.Add("roomId",PlayerPrefs.GetString("roomId"));
			_tr.sendJoinRequest (d);
		}
		else
		{
			overlay.GetComponent<AvatarSceneOverlay>().showStart();
			d.Add("mode", PlayerPrefs.GetString("mode"));
			d.Add("rules",PlayerPrefs.GetString("rules"));
			_tr.sendStartRequest(d);
		}

	}

}
