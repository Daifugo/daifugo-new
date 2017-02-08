using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSceneController : MonoBehaviour {

	GameObject[] playerSpaces = null;
	public GameObject mainPlayer;
	public GameObject overlay;

	private GameObject playerWithTurn = null;

	void Start () {
		
		playerSpaces = GameObject.FindGameObjectsWithTag("player");

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

	public void setMainUserId(string s)
	{
		mainPlayer.GetComponent<MainPlayer>().setId(s);
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
			// List<GameObject> list = new List<GameObject>(playerSpaces);
			// GameObject b = list.Find(x => x.GetComponent<Player>().getId() == s);
			// b.GetComponent<Player>().toggleTurn();
		}
	}
	

	public void addUserCard(Card s)
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
			mainPlayer.GetComponent<MainPlayer>().toggleTurn();
		}
		else
		{
			List<GameObject> list = new List<GameObject>(playerSpaces);
			GameObject b = list.Find(x => x.GetComponent<Player>().getId() == id);
			b.GetComponent<Player>().toggleTurn();
		}
	}

	public void showInvalidMove()
	{
		GameSceneOverlay overlayScript = overlay.GetComponent<GameSceneOverlay>();
		overlayScript.showInvalid();
	}


	public void addCardCount(string id, int count)
	{
		List<GameObject> list = new List<GameObject>(playerSpaces);
		GameObject b = list.Find(x => x.GetComponent<Player>().getId() == id);
		b.GetComponent<Player>().setCardCount(count);
	}



}
