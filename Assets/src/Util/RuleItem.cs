using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RuleItem : MonoBehaviour {
 

	public GameObject ruleName;
	public GameObject ruleDescription;

	private float _height = 0.0f;
	private float _YCoor = 0.0f;


	public void setRuleDetails(string name, string description)
	{
		ruleName.GetComponent<Text>().text = name;
		ruleDescription.GetComponent<Text>().text = description;
	}

	public void setGeometry(float YCoor, float height)
	{
		_height = height;
		_YCoor = YCoor;
	}

	// Use this for initialization

	void Start () {

		gameObject.GetComponent<RectTransform>().offsetMax = Vector2.zero;
		gameObject.GetComponent<RectTransform>().offsetMin = Vector2.zero;
		gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2(0.0f,_height);
		gameObject.GetComponent<RectTransform>().anchoredPosition= new Vector2(0.0f,_YCoor);

	}

	// Update is called once per frame
	void Update () {

	}
}
