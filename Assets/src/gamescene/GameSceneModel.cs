using UnityEngine;
using System.Collections;

public class GameSceneModel : MonoBehaviour,SocketConnectionInterface {


	Transporter _transporter = null;


	// Use this for initialization
	void Start () {
	
		_transporter = GameObject.Find("Transporter").GetComponent<Transporter>();
		_transporter.setSocketDelegate (this);

	}


	public void receiveData(string dt)
	{

	}


	public void handleError()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
