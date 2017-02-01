using UnityEngine;
using System.Collections;

public class GameSceneController : MonoBehaviour {

	GameObject[] playerSpaces = null;

	void Start () {
		
		playerSpaces = GameObject.FindGameObjectsWithTag("player");

	}
	
	// Update is called once per frame
	void Update () {
	
	}


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



}
