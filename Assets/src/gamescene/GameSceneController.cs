﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSceneController : MonoBehaviour {

	GameObject[] playerSpaces = null;

	public GameObject overlay;

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

	public void addCardCount(string id, int count)
	{
		List<GameObject> list = new List<GameObject>(playerSpaces);
		GameObject b = list.Find(x => x.GetComponent<Player>().getId() == id);
		b.GetComponent<Player>().setCardCount(count);
	}



}
