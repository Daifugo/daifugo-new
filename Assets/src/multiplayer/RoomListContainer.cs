using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomListContainer : MonoBehaviour {


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
	}

	void Start()
	{

	}

}
