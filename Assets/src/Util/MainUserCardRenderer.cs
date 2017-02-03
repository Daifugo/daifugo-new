using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainUserCardRenderer : MonoBehaviour {

	private CardGameSpawner _spawner = null;

	private float _cardStartX = 105.0f;
	private const float _cardYCoor = 9.0f;
	private const float _cardWidth = 153.0f;
	private const float _cardHeight = 220.0f;


	public GameObject renderCard(int suit, int rank)
	{
		GameObject card = Resources.Load("prefabs/GameCard", typeof(GameObject)) as GameObject;
		card = Instantiate (card, Vector3.zero, Quaternion.identity, transform) as GameObject;

		setImage(card, suit, rank);
		setGeometry(card);

		return card;
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
