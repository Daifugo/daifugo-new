using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour {

	public GameObject roomName;
	public GameObject rules;
	public GameObject numOfPlayers;

	private float _height = 0.0f;
	private float _YCoor = 0.0f;


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

	public void setGeometry(float YCoor, float height)
	{
		_height = height;
		_YCoor = YCoor;
	}


	// Use this for initialization
	void Start () {
	
		gameObject.GetComponent<RectTransform>().offsetMax = Vector2.zero;
		gameObject.GetComponent<RectTransform>().offsetMin = Vector2.zero;
		gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2(0.0f,_height);
		gameObject.GetComponent<RectTransform>().anchoredPosition= new Vector2(0.0f,_YCoor);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
