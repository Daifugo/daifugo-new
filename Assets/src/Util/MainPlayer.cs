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
		cardLocation.GetComponent<OwnedCardRenderer>().toggleCardInteractable();
		actions.SetActive(_hasTurn);

		_transport.sendM(getDictionary());
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

		_transport.requestTurn(getDictionary());

		yield break;
	}


	public void renderDealt(Card[] s)
	{
		dealtCard.GetComponent<DealtCardRenderer>().render(s);
		_selectedCards.Clear();
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

		var obj = getDictionary();
		obj.Add("cards",converted.ToArray());

		_transport.sendPlayerMove(obj);
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
