using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainUserCardRenderer : MonoBehaviour {

	private CardGameSpawner _spawner = null;

	private float _cardStartX = 105.0f;
	private const float _cardYCoor = 9.0f;
	private const float _cardWidth = 153.0f;
	private const float _cardHeight = 220.0f;

	private const float _dealtCardWidth = 70.0f;
	private const float _dealtCardHeight = 100.0f;
	private const float _DEALTCARD_SPACE = 30.0f;
	private const float _dealtCardY = 0.0f;


	public GameObject renderCard(Card s)
	{
		GameObject card = Resources.Load("prefabs/GameCard", typeof(GameObject)) as GameObject;
		card = Instantiate (card, Vector3.zero, Quaternion.identity, transform) as GameObject;

		card.GetComponent<GameCard>().addCard(s);

		setImage(card, s.getSuit(), s.getRank());
		setGeometry(card);

		return card;
	}


	public void renderDealt(Card[] s)
	{
		StartCoroutine(displayDealt(s));
	}


	IEnumerator displayDealt(Card[] cards)
	{
		float xCoor = calculateStartX(cards.Length);

		yield return new WaitForSeconds (1.3f);

		foreach(var card in cards)
		{

			GameObject cardPrefab = Resources.Load("prefabs/maincard", typeof(GameObject)) as GameObject;
			cardPrefab = Instantiate (cardPrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;

			setDealtImage(cardPrefab, card.getSuit(),card.getRank());
			setDealtGeometry(cardPrefab, xCoor);

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

	void setDealtImage(GameObject card, int suit, int rank)
	{
		card.GetComponent<Image>().sprite = _spawner.getSprite (suit, rank);
	}

	void setDealtGeometry(GameObject card, float xCoor)
	{
		RectTransform rect = card.GetComponent<RectTransform>();

		rect.sizeDelta = new Vector2(_dealtCardWidth,_dealtCardHeight);
		rect.anchoredPosition= new Vector2(xCoor,_dealtCardY);

		rect.anchorMax = new Vector2(0.5f,0.5f);
		rect.anchorMin = new Vector2(0.5f,0.5f);

	}

	void setImage(GameObject card, int suit, int rank)
	{
		card.GetComponent<GameCard>().setImage(_spawner.getSprite (suit, rank));
	}


	void setGeometry(GameObject card)
	{
		card.GetComponent<RectTransform> ().sizeDelta = new Vector2(_cardWidth,_cardHeight);
		card.GetComponent<RectTransform>().anchoredPosition= new Vector2(_cardStartX,_cardYCoor);
	
		_cardStartX += (_cardWidth / 2.2f);

		float width = gameObject.GetComponent<RectTransform>().sizeDelta.x; 
		float height = gameObject.GetComponent<RectTransform>().sizeDelta.y;

		float newWidth = width + (_cardWidth / 2.2f);
		gameObject.GetComponent<RectTransform>().sizeDelta  = new Vector2(newWidth,height);
	}


	void Start()
	{

		_spawner = new CardGameSpawner();
	}

}
