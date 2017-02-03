using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainPlayer : MonoBehaviour {

	public GameObject cardLocation;
	public GameObject actions;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void buttonCardHandler(GameObject c)
	{
		c.GetComponent<GameCard>().selected();
	}

	public void addCard(Card s)
	{
		GameObject c = cardLocation.GetComponent<MainUserCardRenderer>().renderCard(s.getSuit(),s.getRank());
		c.GetComponent<GameCard>().addHandler(delegate{
			buttonCardHandler(c);
		});
	}

}
