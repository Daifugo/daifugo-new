using UnityEngine;
using System.Collections;

public class Util{

	public static Sprite getSprite(string path)
	{
		Texture2D tex = Resources.Load ("images/"+path, typeof(Texture2D)) as Texture2D;
		
		var rect = new Rect (0, 0,tex.width, tex.height);
		Sprite x = Sprite.Create (tex, rect, Vector2.zero);

		return x;
	}

}
