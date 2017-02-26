using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class SinglePlayerController : MonoBehaviour{

	public GameObject transporter;
	public GameObject ruleListContainer;
	public GameObject loading;
	public GameObject nextButton;
	public GameObject error;

	private Transporter _tr;

	
	void Start () {

		_tr = transporter.GetComponent<Transporter>();
		_tr.requestRules();


		// show loading gif
		
		loading.SetActive(true);
	}

	// From SinglePlayerModel
	
	public void showError()
	{
		loading.SetActive(false);
		error.SetActive(true);
	}
	
	public void renderRules(Dictionary<string,string>[] rules)
	{
		StartCoroutine(renderRulesCoroutine(rules));
		
	}
	
	IEnumerator renderRulesCoroutine(Dictionary<string,string>[] rules)
	{
		// hide loading gif 
		
		loading.SetActive(false);
		
		
		// Render rules
		
		RuleListContainer r = ruleListContainer.GetComponent<RuleListContainer> ();
		
		foreach(var rule in rules)
		{
			r.addRule(rule);
			yield return new WaitForSeconds(1.5f);
		}
		
		
		// show next button
		
		nextButton.SetActive(true);
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
