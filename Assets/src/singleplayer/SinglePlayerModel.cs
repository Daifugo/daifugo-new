using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


public class SinglePlayerModel : MonoBehaviour, SocketConnectionInterface {

	public GameObject transporter;
	public GameObject controller;
	
	private Transporter _tr;
	private JToken _responseToken = null;
	
	


	// Use this for initialization
	void Start () {
	
		_tr = transporter.GetComponent<Transporter>();
		_tr.setSocketDelegate(this);
		
		StartCoroutine(parseData());
	
	}
	
	
	IEnumerator parseData()
	{

		while(_responseToken == null)
			yield return null;


		/* Render out the rules item */

		JArray rms = (JArray)_responseToken;
		var rules = new List<Dictionary<string,string>>();

		foreach (JToken s in rms) 
		{
			var rule = s.ToObject<Dictionary<string,string>> ();
			rules.Add(rule);
		}
		
		SinglePlayerController c = controller.GetComponent<SinglePlayerController>();
		c.renderRules(rules.ToArray());
	}
	
	
	public void receiveData(string dt)
	{
		JArray resArray = JArray.Parse (dt);
		JToken response = resArray.First["response"];

		_responseToken = response.SelectToken ("data");
	}


	public void handleError()
	{
		SinglePlayerController c = controller.GetComponent<SinglePlayerController>();
		c.showError();
	}
}
