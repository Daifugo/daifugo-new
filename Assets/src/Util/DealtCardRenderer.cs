using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DealtCardRenderer : PlayerRightCardRenderer {


	private const float _dealtCardWidth = 70.0f;
	private const float _dealtCardHeight = 100.0f;
	private const float _DEALTCARD_SPACE = 30.0f;
	private const float _dealtCardY = 0.0f;


	public override void initializeXCoor(Card[] cards)
	{
		float length = cards.Length;

		float numOfLeftSideCards = length / 2;
		float xCoor = 0.0f;

		if (length % 2 != 0) {
			
			xCoor = numOfLeftSideCards * _DEALTCARD_SPACE * -1;

		} else {

			float m = ((_dealtCardWidth / 2) / 2);
			float h = (numOfLeftSideCards - 1) * _DEALTCARD_SPACE;
			xCoor = (h + m) * -1;

		}

		this.startX = xCoor;
	}


	public override void render(Card card)
	{
		var mainPlayer = transform.parent.GetComponent<MainPlayer>();

		if(mainPlayer != null)
			mainPlayer.requestRemove(card);

		base.render(card);
	}

	protected override void setGeometry(GameObject card, float xCoor = 0.0f)
	{
		RectTransform rect = card.GetComponent<RectTransform>();

		rect.sizeDelta = new Vector2(_dealtCardWidth,_dealtCardHeight);
		rect.anchoredPosition= new Vector2(xCoor,_dealtCardY);

		rect.anchorMax = new Vector2(0.5f,0.5f);
		rect.anchorMin = new Vector2(0.5f,0.5f);
	}

	// Use this for initialization
	void Start () 
	{
		_spawner = new CardGameSpawner();
	}

}
