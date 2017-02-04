using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class MainPlayer : MonoBehaviour {

	public GameObject cardLocation;
	public GameObject actions;
	public GameObject dealtCard;

	Transporter _transport = null;
	string _id = null;
	bool _hasTurn = false;
	List<GameCard> _selectedCards = null;

	// Use this for initialization

	void Start () {

		_transport = (GameObject.Find("Transporter")).GetComponent<Transporter> ();
		_selectedCards = new List<GameCard>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/* accessors of _id */

	public void setId(string id)
	{
		this._id = id;
	}

	public string getId()
	{
		return this._id;
	}

	/* end accessor */

	public void toggleTurn()
	{
		_hasTurn = !_hasTurn;
		actions.SetActive(_hasTurn);
	}


	/* handler for card owned */

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

	/* end handler */

	/* Render cards */

	public void addCard(Card s)
	{
		GameObject c = cardLocation.GetComponent<OwnedCardRenderer>().render(s);
		c.GetComponent<GameCard>().addHandler(delegate{
			buttonCardHandler(c);
		});
	}

	public void renderDealt(Card[] s)
	{
		actions.SetActive(false);
		dealtCard.GetComponent<DealtCardRenderer>().render(s);
	}

	/* called from child object */

	public void requestRemove(Card s)
	{
		cardLocation.GetComponent<OwnedCardRenderer>().removeCard(s);
	}


	public void endDistribute()
	{
		var obj = getDictionary();
		_transport.requestTurn(obj);
	}

	/* end */

	/* end render */

	/* Button action handlers */

	public void dealCardHandler()
	{
		var converted = _selectedCards.Select(elem => elem.getCard().getDictionary());

		var obj = getDictionary();
		obj.Add("cards",converted.ToArray());

		_transport.sendPlayerMove(obj);
		_selectedCards.Clear();
	}


	public void passTurn()
	{
		var obj = getDictionary();
		_transport.sendPassTurn(obj);
		_selectedCards.Clear();
	}

	/* End Button action handlers */

	Dictionary<string,object> getDictionary()
	{
		var obj = new Dictionary<string,object>();
		obj.Add("userId",_id);

		return obj;
	}

}
