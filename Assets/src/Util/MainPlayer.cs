using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainPlayer : MonoBehaviour {

	public GameObject cardLocation;
	public GameObject actions;

	string _id = null;
	bool _hasTurn = false;

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

	public void setId(string id)
	{
		this._id = id;
	}

	public string getId()
	{
		return this._id;
	}

	public void toggleTurn()
	{
		_hasTurn = !_hasTurn;
		actions.SetActive(_hasTurn);
	}

	public void addCard(Card s)
	{
		GameObject c = cardLocation.GetComponent<MainUserCardRenderer>().renderCard(s.getSuit(),s.getRank());
		c.GetComponent<GameCard>().addHandler(delegate{
			buttonCardHandler(c);
		});
	}

}
