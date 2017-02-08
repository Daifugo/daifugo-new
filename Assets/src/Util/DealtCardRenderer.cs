using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DealtCardRenderer : CardRenderer {


	private const float _dealtCardWidth = 70.0f;
	private const float _dealtCardHeight = 100.0f;
	private const float _DEALTCARD_SPACE = 30.0f;
	private const float _dealtCardY = 0.0f;


	public override void render(Card[] s)
	{
		StartCoroutine(displayDealt(s));
	}


	IEnumerator displayDealt(Card[] cards)
	{
		float xCoor = calculateStartX(cards.Length);
		var MainPlayer = transform.parent.GetComponent<MainPlayer>();

		yield return new WaitForSeconds (1.3f);

		foreach(var card in cards)
		{
			if(MainPlayer != null)
				MainPlayer.requestRemove(card);

			GameObject cardPrefab = getObject("maincard");

			setImage(cardPrefab, card.getSuit(),card.getRank());
			setGeometry(cardPrefab, xCoor);

			xCoor += _DEALTCARD_SPACE;

			yield return new WaitForSeconds (0.9f);
		}
	}


	float calculateStartX(int length)
	{
		int numOfLeftSideCards = length / 2;
		float xCoor = 0.0f;

		if (length % 2 != 0) {
			
			xCoor = numOfLeftSideCards * _DEALTCARD_SPACE * -1;

		} else {

			float m = ((_dealtCardWidth / 2) / 2);
			float h = (numOfLeftSideCards - 1) * _DEALTCARD_SPACE;
			xCoor = (h + m) * -1;

		}

		return xCoor;
	}


	protected override void setImage(GameObject card, int suit, int rank)
	{
		card.GetComponent<Image>().sprite = _spawner.getSprite (suit, rank);
	}


	protected override void setGeometry(GameObject card, float xCoor = 0.0f)
	{
		RectTransform rect = card.GetComponent<RectTransform>();

		rect.sizeDelta = new Vector2(_dealtCardWidth,_dealtCardHeight);
		rect.anchoredPosition= new Vector2(xCoor,_dealtCardY);

		rect.anchorMax = new Vector2(0.5f,0.5f);
		rect.anchorMin = new Vector2(0.5f,0.5f);
	}


	public override void removeCard(Card s)
	{
		foreach(Transform child in transform)
			Destroy(child.gameObject);
	}

	// Use this for initialization
	void Start () 
	{
		_spawner = new CardGameSpawner();
	}

}
