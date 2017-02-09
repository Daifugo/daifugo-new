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

	protected override float calculateX(int length)
	{
		if(length == 1)
		{
			return 100.0f;
		}
		else if(length < 5)
		{
			float u = (pivotX2Coor / length);
			float x = length == 2?u-5.0f:u;
			return (x + 20);
		}
		else
		{
			var h = (pivotX2Coor / length) + 15;
			return h;
		}
	}


}
