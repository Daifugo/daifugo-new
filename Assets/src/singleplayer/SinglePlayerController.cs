using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

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

	IEnumerator parseData(int code, JToken data)
	{
		yield return null;
	}

	public void receiveData(string dt)
	{
		JArray resArray = JArray.Parse (dt);
		JToken response = resArray.First["response"];

		JToken responseTkn = response.SelectToken ("data");
		int responseCd = (int)response.SelectToken("code");

		StartCoroutine(parseData(responseCd,responseTkn));
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
