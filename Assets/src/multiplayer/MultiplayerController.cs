using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class MultiplayerController : MonoBehaviour, SocketConnectionInterface {

	public GameObject transporter;
	public GameObject roomList;
	public GameObject nextButton;

	private Transporter _tr;
	private JToken _responseToken = null;

	// Use this for initialization
	void Start () {

		_tr = transporter.GetComponent<Transporter>();
		_tr.setSocketDelegate(this);

		_tr.requestRooms();

		StartCoroutine (parseData ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/* SocketConnectionInterface */

	IEnumerator parseData()
	{
			
		/* Only expect data with the ROOMLIST_CODE */

		while(_responseToken == null)
			yield return null;

		JArray rms = (JArray)_responseToken;
		RoomListContainer r = roomList.GetComponent<RoomListContainer> ();

		foreach (JToken s in rms) 
		{
			Dictionary<string,object> roomData = s.ToObject<Dictionary<string,object>> ();
			r.addRooms (roomData);
		}

		nextButton.SetActive (true);

	}

	public void receiveData(string dt)
	{
		JArray resArray = JArray.Parse (dt);
		JToken response = resArray.First["response"];

		_responseToken = response.SelectToken ("data");
	}

	public void handleError()
	{

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
