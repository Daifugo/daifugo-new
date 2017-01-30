using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour {

	public GameObject roomName;
	public GameObject rules;
	public GameObject numOfPlayers;


	public void setRoomDetails(string[] rulesStr, string name, string num)
	{


		/* set rules test */

		Text txtRules = rules.GetComponent<Text> ();

		foreach(string str in rulesStr)
		{
			txtRules.text = txtRules.text + str + ",\n";
		}

		/* set numOfPlayers */

		Text txtNumOfPlayers  = numOfPlayers.GetComponent<Text>();

		txtNumOfPlayers.text = num;


		/* set room name */

		Text txtName = roomName.GetComponent<Text> ();

		txtName.text = txtName.text + " " + name;


	}




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
