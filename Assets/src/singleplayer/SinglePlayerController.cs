using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SinglePlayerController : MonoBehaviour, SocketConnectionInterface {

	public GameObject transporter;
	private Transporter _tr;

	// Use this for initialization
	void Start () {

		_tr = transporter.GetComponent<Transporter>();
		_tr.setSocketDelegate(this);

		_tr.requestRules();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/* SocketConnectionInterface */

	public void receiveData(string dt)
	{

	}

	public void handleError()
	{

	}

	/* Button Handlers */

	public void back()
	{
		SceneManager.LoadScene ("main");
	}

	public void next()
	{

	}


}
