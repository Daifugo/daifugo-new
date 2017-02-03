using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainPlayer : MonoBehaviour {

	public GameObject cardLocation;
	public GameObject actions;

	string _id = null;
	bool _hasTurn = false;
	List<GameCard> _selectedCards = null;

	// Use this for initialization

	void Start () {

		_selectedCards = new List<GameCard>();

	}
	
	// Update is called once per frame
	void Update () {
	
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

	public void buttonCardHandler(GameObject c)
	{
		GameCard gc = c.GetComponent<GameCard>();

		if(gc.isSelected())
		{
			_selectedCards.Remove(gc);
		}
		else
		{
			_selectedCards.Add(gc);
		}

		c.GetComponent<GameCard>().toggleSelected();
	}

	public void addCard(Card s)
	{
		GameObject c = cardLocation.GetComponent<MainUserCardRenderer>().renderCard(s);
		c.GetComponent<GameCard>().addHandler(delegate{
			buttonCardHandler(c);
		});
	}

}
