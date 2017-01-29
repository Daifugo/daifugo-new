using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RuleListContainer : MonoBehaviour {

	private float startRuleYCoor = -90.0f;
	private float ruleItemHeight = 177.0f;
	private int ruleCount = 0;
	private float ruleItemGap = 5.0f;

	private List<string> selectedRules;


	public void toggleHandler(GameObject s)
	{
		bool isToggleActive = s.GetComponent<RuleItem> ().isToggleActive();

		if(isToggleActive)
		{
			selectedRules.Add(s.GetComponent<RuleItem> ().getRuleId());
		}
		else
		{
			selectedRules.Remove(s.GetComponent<RuleItem> ().getRuleId());
		}

	}



	public void addRule(Dictionary<string,string> rule)
	{

		GameObject rulePrefab = Resources.Load ("prefabs/rule", typeof(GameObject)) as GameObject;
		GameObject ruleObj = Instantiate (rulePrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;

		string ruleName = null;
		string ruleDescription = null;
		string ruleId = null;

		rule.TryGetValue ("ruleName", out ruleName);
		rule.TryGetValue ("ruleDescription", out ruleDescription);
		rule.TryGetValue("ruleId", out ruleId);

		/* set details */

		ruleObj.GetComponent<RuleItem> ().setRuleDetails (ruleName, ruleDescription,ruleId);

		/* set geometry */

		float YCoor = (((ruleCount * ruleItemHeight) + (ruleCount * ruleItemGap)) * (-1)) + startRuleYCoor;
		ruleObj.GetComponent<RuleItem> ().setGeometry(YCoor, ruleItemHeight);

		/* add handler */

		ruleObj.GetComponent<RuleItem> ().setToggleUIHandler(delegate{
			toggleHandler(ruleObj);
		});

		ruleCount++;
	}

	void Start()
	{
		selectedRules = new List<string>();
	}

}
