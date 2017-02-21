using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerRightCardRenderer : CardRenderer {

	protected const float CARD_SPACE = 30.0f;
	protected const float CARD_WIDTH = 70.0f;
	protected const float CARD_HEIGHT = 100.0f;

	protected float pivotX1Coor = 75.0f;
	protected const float pivotX2Coor = 150.0f;

	protected float startX = 0.0f;


	public virtual void initializeXCoor(Card[] cards)
	{
		float length = cards.Length;

		if(length == 1)
		{
			this.startX = pivotX2Coor;
		}
		else if(length < 5)
		{
			float w = (pivotX2Coor / length) - 5.0f;
			this.startX = (pivotX1Coor + w);
		}
		else
		{
			int y = length == 5?3:((length == 6)?6:10);
			var h = (pivotX2Coor / length) - (length * y);
			this.startX = (pivotX1Coor + h);
		}
	}


	public override void render(Card card)
	{
		GameObject cardPrefab = getObject("maincard");

		setImage(cardPrefab, card.getSuit(),card.getRank());
		setGeometry(cardPrefab, startX);

		this.startX += CARD_SPACE;
	}


	public override void removeCard(Card s)
	{
		foreach(Transform child in transform)
			Destroy(child.gameObject);
	}

	protected override void setImage(GameObject card, int suit, int rank)
	{
		card.GetComponent<Image>().sprite = _spawner.getSprite (suit, rank);
	}

	protected override void setGeometry(GameObject card, float xCoor = 0.0f)
	{
		RectTransform rect = card.GetComponent<RectTransform>();

		rect.sizeDelta = new Vector2(CARD_WIDTH,CARD_HEIGHT);
		rect.anchoredPosition= new Vector2(xCoor,0.0f);
	}

	void Start()
	{
		_spawner = new CardGameSpawner();
	}


}
