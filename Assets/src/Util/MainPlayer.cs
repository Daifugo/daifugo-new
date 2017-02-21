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
	Dictionary<string,object> _id = null;
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

	public void setId(Dictionary<string,object> id)
	{
		this._id = id;
	}

	public string getId()
	{
		return (string)this._id["userId"];
	}

	/* end accessor */

	public void toggleTurn(bool isPrev)
	{
		_hasTurn = !_hasTurn;
		cardLocation.GetComponent<OwnedCardRenderer>().toggleCardInteractable();
		actions.SetActive(_hasTurn);

		if(isPrev)
			_transport.sendM(this._id);
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

	public void deleteDealt()
	{
		dealtCard.GetComponent<DealtCardRenderer>().removeCard(null);
	}


	public void addCard(Card[] s)
	{
		StartCoroutine(addCardCoroutine(s));
	}


	IEnumerator addCardCoroutine(Card[] s)
	{

		foreach(var card in s)
		{
			cardLocation.GetComponent<OwnedCardRenderer>().render(card, buttonCardHandler);
			yield return new WaitForSeconds(0.8f);
		}

		_transport.requestTurn(this._id);

		yield break;
	}


	public void renderDealt(Card[] s)
	{
		dealtCard.GetComponent<DealtCardRenderer>().initializeXCoor(s);
		StartCoroutine(renderDealtCardCoroutine(s));
	}


	IEnumerator renderDealtCardCoroutine(Card[] s)
	{
		foreach(var card in s)
		{
			dealtCard.GetComponent<DealtCardRenderer>().render(card);
			yield return new WaitForSeconds(0.9f);
		}

		_selectedCards.Clear();
		_transport.sendM(this._id);
	}


	/* called from child object */

	public void requestRemove(Card s)
	{
		cardLocation.GetComponent<OwnedCardRenderer>().removeCard(s);
	}

	/* end */

	/* end render */


	/* Button action handlers */

	public void dealCardHandler()
	{
		var converted = _selectedCards.Select(elem => elem.getCard().getDictionary());

		var obj = new Dictionary<string, object>(this._id);
		obj.Add("cards",converted.ToArray());

		_transport.sendPlayerMove(obj);
	}


	public void passTurn()
	{
		_transport.sendPassTurn(this._id);
		_selectedCards.Clear();
	}

	/* End Button action handlers */

}
