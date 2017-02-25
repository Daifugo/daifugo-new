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
	}
	
	
	// From AvatarSceneModel
	
	public void receiveUserId(string id)
	{
		PlayerPrefs.SetString ("userId", id);
		SceneManager.LoadScene("game");
	}
	
	public void disableAvatar(int[] avatarIds)
	{	
		foreach(int avatarId in avatarIds)
		{
			AvatarsContainer ava = avatarContainer.GetComponent<AvatarsContainer> ();
			ava.disableAvatars (avatarId);
		}
	}
	
	public void showError()
	{
		overlay.GetComponent<AvatarSceneOverlay>().showError();
	}
	
	
	// From AvatarsContainer

	public void notify()
	{
		// show next Button
	
		nextButton.SetActive(true);
		
		
		// request selected avatars
		
		if(PlayerPrefs.HasKey("roomId"))
		{
			var roomId = PlayerPrefs.GetString("roomId");
			
			var data = new Dictionary<string,object>();
			data.Add("roomId", roomId);

			_tr.requestSelectedAvatars(data);
		}
	}
	



	/* Button Handlers */

	public void nextButtonHandler()
	{
		// Show loading overlay

		overlay.GetComponent<AvatarSceneOverlay>().showLoading();


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
