using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RulesOverlay : MainPlayerTurnview {

	public GameObject rule;

	public void show(bool isPass, string rulesName)
	{
		rule.GetComponent<Text>().text = rulesName;
		base.show(isPass);
	}

}
