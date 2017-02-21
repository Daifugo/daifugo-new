using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public GameObject avatar;
	public GameObject cardsCount;
	public GameObject parent;

	public GameObject cardRenderTop;


	bool _isOccupied = false;
	string _userId = null;


	public bool isSpaceVacant()
	{
		return (_isOccupied == false);
	}


	public void setPlayer(string userId, int photoId)
	{
		setAvatar(photoId);
		_userId = userId;
		_isOccupied = true;
	}

	public string getId()
	{
		return this._userId;
	}

	public void setCardCount(int s)
	{
		char[] space = new char[]{' '};
		Text txt = cardsCount.GetComponent<Text>(); 
		string[] str = txt.text.Split(space);
		txt.text = s.ToString() + " " + str[1];

	}

	private void setAvatar(int photoId)
	{
		GameObject ava = Resources.Load ("prefabs/gameAvatar"+(photoId+1), typeof(GameObject)) as GameObject;
		GameObject s = (GameObject)Instantiate(ava,Vector3.zero,Quaternion.identity,avatar.transform);
		s.GetComponent<RectTransform>().offsetMax = Vector2.zero;
		s.GetComponent<RectTransform>().offsetMin = Vector2.zero;
		s.GetComponent<RectTransform>().sizeDelta = new Vector2(150.0f,150.0f);
	}

	public void toggleTurn()
	{
		
	}

	public void remove()
	{
		PlayerRightCardRenderer c = cardRenderTop.GetComponent<PlayerRightCardRenderer>();
		PlayerLeftCardRenderer x = cardRenderTop.GetComponent<PlayerLeftCardRenderer>();
		DealtCardRenderer m = cardRenderTop.GetComponent<DealtCardRenderer>();

		if(c == null && x == null)
		{
			m.removeCard(null);
		}
		else if(c == null && m == null)
		{
			x.removeCard(null);
		}
		else if(x == null && m == null)
		{
			c.removeCard(null);
		}
	}

	public void showCard(Card[] s)
	{
		PlayerRightCardRenderer c = cardRenderTop.GetComponent<PlayerRightCardRenderer>();
		PlayerLeftCardRenderer x = cardRenderTop.GetComponent<PlayerLeftCardRenderer>();
		DealtCardRenderer m = cardRenderTop.GetComponent<DealtCardRenderer>();

		if(c == null && x == null)
		{
			m.render(s);
		}
		else if(c == null && m == null)
		{
			x.render(s);
		}
		else if(x == null && m == null)
		{
			c.render(s);
		}

	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
