using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AvatarsContainer : MonoBehaviour {

	public GameObject controller;

	private Transform _selectedAvatar = null;
	private List<int> _selectedAvatars = null;

	private bool _finishAnimation = false;

	// Use this for initialization
	void Awake () {
	
		_selectedAvatars = new List<int>();
	}

	public void disableAvatars(int index)
	{

		if(_finishAnimation)
		{
			transform.GetChild(index).GetComponent<AvatarItem>().disableAvatar();
		}
		else
		{
			_selectedAvatars.Add(index);
		}

	}


	/* Animation Event Callback */

	public void finishAnim()
	{
		controller.GetComponent<AvatarSceneController>().showNextButton();
	}


	public void addClickHandlers()
	{

		if(!_selectedAvatars.Contains(0))
			avatarThumbnailHandler(transform.GetChild(0));


		for(int i = 0;i < transform.childCount;i++)
		{
			Transform child = transform.GetChild(i);

			if(_selectedAvatars.Contains(i))
			{
				child.GetComponent<AvatarItem>().disableAvatar();
			}
			else
			{
				child.GetComponent<Button>().onClick.AddListener(delegate{
					avatarThumbnailHandler(child);
				});
			}
		}

		_finishAnimation = true;

	}

	
	private void avatarThumbnailHandler(Transform s)
	{

		if(_selectedAvatar != null)
			_selectedAvatar.GetComponent<AvatarItem>().toggleShowSelected();

		_selectedAvatar = s;
		s.GetComponent<AvatarItem>().toggleShowSelected();

	}

	public int getIndexSelectedAvatar()
	{
		return _selectedAvatar.GetSiblingIndex ();
	}

}
