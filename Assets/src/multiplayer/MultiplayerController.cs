using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MultiplayerController : MonoBehaviour{

	public GameObject transporter;
	public GameObject roomList;
	public GameObject nextButton;
	public GameObject loading;
	public GameObject error;

	private Transporter _tr;
	
	// Use this for initialization
	void Start () {

		/* Request rooms from server */

		_tr = transporter.GetComponent<Transporter>();
		_tr.requestRooms();


		/* show loading gif */

		loading.SetActive(true);
	}

	
	// From MultiplayerModel
	
	public void renderRooms(Dictionary<string,object>[] rooms)
	{
		StartCoroutine(renderRoomsCoroutine(rooms));
		
	}
	
	public void showError()
	{
		loading.SetActive(false);
		error.SetActive(true);
	}
	
	
	IEnumerator renderRoomsCoroutine(Dictionary<string,object>[] rooms)
	{
		// hide loading gif 
		
		loading.SetActive(false);
		
		
		// Render rooms
		
		RoomListContainer r = roomList.GetComponent<RoomListContainer> ();
		
		foreach(var room in rooms)
		{
			r.addRooms(room);
			yield return new WaitForSeconds(1.5f);
		}
		
		
		// show next button
		
		nextButton.SetActive(true);
	}


	/* Button handlers */

	public void nextButtonHandler()
	{
		/* Get the selected room to join */ 

		RoomListContainer r = roomList.GetComponent<RoomListContainer> ();

		string roomId = r.getSelectedRoomToJoin();

		/* save it */

		PlayerPrefs.SetString ("roomId", roomId);
		SceneManager.LoadScene ("avatar");

	}

}
