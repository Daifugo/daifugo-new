using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarItem : MonoBehaviour {


	public GameObject overlay;
	private bool _selected = false;

	public void toggleShowSelected()
	{
		_selected = !_selected;
		overlay.SetActive(_selected);
	}

	public void disableAvatar()
	{
		overlay.SetActive(true);
		overlay.transform.DetachChildren();
		transform.GetComponent<Button>().onClick.RemoveAllListeners();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
