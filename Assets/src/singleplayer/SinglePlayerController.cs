using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class SinglePlayerController : MonoBehaviour, SocketConnectionInterface {

	public GameObject transporter;
	public GameObject ruleListContainer;
	public GameObject loading;

	private Transporter _tr;
	private JToken _responseToken = null;


	// Use this for initialization
	void Start () {


		_tr = transporter.GetComponent<Transporter>();
		_tr.setSocketDelegate(this);

		_tr.requestRules();


		/* show loading gif */

		loading.SetActive(true);


		/* Start coroutine */

		StartCoroutine(parseData());

	}
	
	// Update is called once per frame
	void Update () {
	
	}



	/* SocketConnectionInterface */

	IEnumerator parseData()
	{

		while(_responseToken == null)
			yield return null;
		
		loading.SetActive(false);

		JArray rms = (JArray)_responseToken;
		RuleListContainer r = ruleListContainer.GetComponent<RuleListContainer> ();

		foreach (JToken s in rms) 
		{
			Dictionary<string,string> ruleData = s.ToObject<Dictionary<string,string>> ();
			yield return new WaitForSeconds(1.4f);
			r.addRule (ruleData);
		}

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

	/* Button Handlers */

	public void back()
	{
		SceneManager.LoadScene ("main");
	}

	public void next()
	{

		RuleListContainer r = ruleListContainer.GetComponent<RuleListContainer> ();
		string rules = r.getSelectedRules();
		PlayerPrefs.SetString("rules",rules);
		SceneManager.LoadScene ("avatar");
		
	}


}
