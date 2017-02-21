using UnityEngine;
using System.Collections;

public class PlayerLeftCardRenderer : PlayerRightCardRenderer {

	// Use this for initialization
	void Start () {
	_spawner = new CardGameSpawner();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void initializeXCoor(Card[] cards)
	{
		float length = cards.Length;
		
		if(length == 1)
		{
			this.startX = 100.0f;
		}
		else if(length < 5)
		{
			float u = (pivotX2Coor / length);
			float x = length == 2?u-5.0f:u;
			this.startX = (x + 20);
		}
		else
		{
			this.startX = (pivotX2Coor / length) + 15;
		}
	}


}
