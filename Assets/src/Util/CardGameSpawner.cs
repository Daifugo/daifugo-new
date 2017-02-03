using System.Collections;
using System.Collections.Generic;

public class CardGameSpawner{

	private Dictionary<int,string> _clubSuit;
	private Dictionary<int,string> _heartSuit;
	private Dictionary<int,string> _diamondSuit;
	private Dictionary<int,string> _spadesSuit;


	public CardGameSpawner()
	{

		_heartSuit = new Dictionary<int, string> ();
		_clubSuit = new Dictionary<int, string> ();
		_diamondSuit = new Dictionary<int, string> ();
		_spadesSuit = new Dictionary<int, string> ();

	
		var r = new string[] { "_of_hearts", "_of_clubs","_of_diamonds","_of_spades"};

		for (int i = 0; i < r.Length; i++) {	
			for (int j = 3; j < 16; j++) {
				
				Dictionary<int,string> g = getDictionaryForKey (i+1);
				g.Add (j,j.ToString()+r[i] );

			}
		}
	}


	private Dictionary<int,string> getDictionaryForKey(int key)
	{

		if (key == 1) 
		{
			return _heartSuit;
		} 
		else if (key == 2)
		{
			return _clubSuit;
		}
		else if (key == 3)
		{
			return _diamondSuit;
		} 
		else if (key == 4) 
		{
			return _spadesSuit;
		}

		return null;
	}



}
