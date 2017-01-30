using UnityEngine;
using System.Collections;

public class AvatarItem : MonoBehaviour {


	public GameObject overlay;
	private bool _selected = false;

	public void toggleShowSelected()
	{
		_selected = !_selected;
		overlay.SetActive(_selected);
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
