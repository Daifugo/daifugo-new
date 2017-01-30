using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarController : MonoBehaviour {


	private Transform _selectedAvatar = null;

	// Use this for initialization
	void Start () {
	
	}

	/* Animation Event Callback */

	public void addClickHandlers()
	{

		avatarThumbnailHandler(transform.GetChild(0));

		for(int i = 0;i < transform.childCount;i++)
		{
			Transform child = transform.GetChild(i);

			child.GetComponent<Button>().onClick.AddListener(delegate{

				avatarThumbnailHandler(child);

			});
		}
	}
	
	private void avatarThumbnailHandler(Transform s)
	{

		if(_selectedAvatar != null)
			a(_selectedAvatar,false);

		_selectedAvatar = s;
		a(s,true);
	}


	private void a(Transform c, bool status)
	{

		for(int i = 0;i < c.childCount;i++)
		{
			Transform child = c.GetChild(i);

			if(child.tag == "avatarOverlay")
				child.gameObject.SetActive(status);
		}

	}

}
