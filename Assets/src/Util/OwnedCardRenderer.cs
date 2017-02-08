using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class OwnedCardRenderer : CardRenderer {


	private float _cardStartX = 105.0f;
	private const float _cardYCoor = 9.0f;
	private const float _cardWidth = 153.0f;
	private const float _cardHeight = 220.0f;


	public override GameObject render(Card s)
	{
		GameObject card = getObject("GameCard");
		card.GetComponent<GameCard>().addCard(s);

		card.GetComponent<Button>().interactable = false;

		setImage(card, s.getSuit(), s.getRank());
		setGeometry(card);

		return card;
	}


	protected override void setImage(GameObject card, int suit, int rank)
	{
		var sprite = _spawner.getSprite (suit, rank);
		card.GetComponent<GameCard>().setImage(sprite);
	}


	protected override void setGeometry(GameObject card, float xCoor = 0.0f)
	{
		card.GetComponent<RectTransform> ().sizeDelta = new Vector2(_cardWidth,_cardHeight);
		card.GetComponent<RectTransform>().anchoredPosition= new Vector2(_cardStartX,_cardYCoor);
	
		_cardStartX += (_cardWidth / 2.2f);

		float width = gameObject.GetComponent<RectTransform>().sizeDelta.x; 
		float height = gameObject.GetComponent<RectTransform>().sizeDelta.y;

		float newWidth = width + (_cardWidth / 2.2f);
		gameObject.GetComponent<RectTransform>().sizeDelta  = new Vector2(newWidth,height);
	}


	public override void removeCard(Card s)
	{
		foreach(Transform child in transform)
		{
			GameCard c = child.GetComponent<GameCard>();

			if(c.isCardSimilar(s))
				child.gameObject.SetActive(false);
		}
	}

	public void toggleCardInteractable()
	{
		bool interactable = transform.GetChild(0).GetComponent<Button>().interactable;

		for(int i = 0;i < transform.childCount;i++)
			transform.GetChild(i).GetComponent<Button>().interactable = !interactable;
	}
	

	void Start()
	{
		_spawner = new CardGameSpawner();
	}

}
