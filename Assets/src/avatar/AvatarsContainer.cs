using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarsContainer : MonoBehaviour {


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
			_selectedAvatar.GetComponent<AvatarItem>().toggleShowSelected();

		_selectedAvatar = s;
		s.GetComponent<AvatarItem>().toggleShowSelected();

	}

}
