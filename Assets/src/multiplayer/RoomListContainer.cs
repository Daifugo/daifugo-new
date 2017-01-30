using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomListContainer : MonoBehaviour {

	private float startRoomYCoor = -100.0f;
	private float roomItemHeight = 244.0f;
	private int roomCount = 0;
	private float roomItemGap = 5.0f;


	private GameObject selectedRoom = null;


	public void toggleHandler(GameObject s)
	{

		if(this.selectedRoom != null)
		{
			this.selectedRoom.GetComponent<RoomItem> ().deactivateCheck ();
		}

		this.selectedRoom = s;
	}



	public void addRooms(Dictionary<string,object> rule)
	{

		GameObject roomPrefab = Resources.Load ("prefabs/room", typeof(GameObject)) as GameObject;
		GameObject roomObj = Instantiate (roomPrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;

		object roomName = null;
		object rules = null;
		object numOfPlayer = null;

		rule.TryGetValue ("roomName", out roomName);
		rule.TryGetValue ("rules", out rules);
		rule.TryGetValue("numOfPlayer", out numOfPlayer);


		RoomItem r = roomObj.GetComponent<RoomItem> ();

		/* set details */

		r.setRoomDetails((string[])rules,(string)roomName,(string)numOfPlayer);

		/* set geometry */

		float YCoor = (((roomCount * roomItemHeight) + (roomCount * roomItemGap)) * (-1)) + startRoomYCoor;
		r.setGeometry(YCoor, roomItemHeight);


		/* add handler for the checkmark UI */

		r.setToggleUIHandler (delegate{
			this.toggleHandler (roomObj);
		});

		roomCount++;
	}

	void Start()
	{

	}

}
