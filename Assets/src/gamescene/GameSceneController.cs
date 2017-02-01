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

	
	
}
