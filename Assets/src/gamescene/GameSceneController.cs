using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSceneController : MonoBehaviour {

	GameObject[] playerSpaces = null;
	public GameObject mainPlayer;
	public GameObject overlay;

	private GameObject playerWithTurn = null;
	private Transporter _transporter;
	private Dictionary<string, object> _userDetails = null;

	void Start () {
		

		var userDetails = new Dictionary<string, object>();
		userDetails.Add("userId", PlayerPrefs.GetString ("userId"));
		_userDetails = userDetails;
		
		// set Id 	

		mainPlayer.GetComponent<MainPlayer>().setId(userDetails);
		overlay.GetComponent<GameSceneOverlay>().setId(userDetails);


		// get player spaces

		playerSpaces = GameObject.FindGameObjectsWithTag("player");


		// Obtain transporter object and send 
		// greet message

		_transporter = GameObject.Find("Transporter").GetComponent<Transporter>();
		_transporter.greetServer(userDetails);

	}
	

	// Update is called once per frame
	void Update () {
	
	}

	/* Overlay Functions */


	public void setTextState(int s)
	{
		overlay.GetComponent<GameSceneOverlay>().changeText(s);
	}

	public void setTimer(int time)
	{
		overlay.GetComponent<GameSceneOverlay>().changeTime(time);
	}


	/* End Overlay Functions */


	public void addPlayer(string userid, int photoId)
	{

		bool spaceFound = false;
		int counter = 0;

		while(!spaceFound)
		{

			Player p = playerSpaces[counter].GetComponent<Player>();

			if (p.isSpaceVacant()) {
				p.setPlayer(userid,photoId);
				spaceFound = true;
			}

			counter++;
		}

	}


	public void renderDealtCard(string userId, Card[] s)
	{
		MainPlayer main = mainPlayer.GetComponent<MainPlayer>();

		if(main.getId() == userId)
		{
			mainPlayer.GetComponent<MainPlayer>().renderDealt(s);
		}
		else
		{
			List<GameObject> list = new List<GameObject>(playerSpaces);
			GameObject b = list.Find(x => x.GetComponent<Player>().getId() == userId);
			b.GetComponent<Player>().showCard(s);
		}
	}
	

	public void addUserCard(Card[] s)
	{
		mainPlayer.GetComponent<MainPlayer>().addCard(s);
	}


	public void switchTurn(string id, int photoId, string prevTurn)
	{
		MainPlayer main = mainPlayer.GetComponent<MainPlayer>();

		if(id != null)
		{
			GameSceneOverlay overlayScript = overlay.GetComponent<GameSceneOverlay>();
			overlayScript.showTurn(main.getId() == id, photoId);
		}

		if(main.getId() == id || main.getId() == prevTurn)
		{
			mainPlayer.GetComponent<MainPlayer>().toggleTurn(main.getId() == prevTurn);
		}

	}


	public void showPassedTurn(string Id, int photoId)
	{
		MainPlayer main = mainPlayer.GetComponent<MainPlayer>();
		GameSceneOverlay overlayScript = overlay.GetComponent<GameSceneOverlay>();

		overlayScript.showPassedTurn(main.getId() == Id, photoId);
	}


	public void showInvalidMove()
	{
		GameSceneOverlay overlayScript = overlay.GetComponent<GameSceneOverlay>();
		overlayScript.showInvalid();
	}


	public void deleteDealt(string userId, string deleteCaller)
	{
		MainPlayer main = mainPlayer.GetComponent<MainPlayer>();

		if(main.getId() == userId)
		{
			main.deleteDealt();
		}
		else
		{
			List<GameObject> list = new List<GameObject>(playerSpaces);
			GameObject b = list.Find(x => x.GetComponent<Player>().getId() == userId);
			b.GetComponent<Player>().remove();
		}


		if(deleteCaller == main.getId())
		{
			_transporter.sendMessageSeq(_userDetails);
		}
	}


	public void showExtraRule(string name)
	{
		GameSceneOverlay overlayScript = overlay.GetComponent<GameSceneOverlay>();
		overlayScript.showRules(name);
	}


	public void showNewRound()
	{
		GameSceneOverlay overlayScript = overlay.GetComponent<GameSceneOverlay>();
		overlayScript.showNewRound();

		foreach(var j in playerSpaces)
			j.GetComponent<Player>().remove();

		MainPlayer main = mainPlayer.GetComponent<MainPlayer>();
		main.deleteDealt();
	}


	public void addCardCount(string id, int count)
	{
		List<GameObject> list = new List<GameObject>(playerSpaces);
		GameObject b = list.Find(x => x.GetComponent<Player>().getId() == id);
		b.GetComponent<Player>().setCardCount(count);
	}


	public void showRoundWin(string id, int photoId)
	{
		MainPlayer main = mainPlayer.GetComponent<MainPlayer>();
		GameSceneOverlay overlayScript = overlay.GetComponent<GameSceneOverlay>();

		overlayScript.showWin(main.getId() == id, photoId);
	}


}
