using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject avatar;
	public GameObject cardsCount;
	public GameObject parent;


	bool _isOccupied = false;
	string _userId = null;


	public bool isSpaceVacant()
	{
		return (_isOccupied == false);
	}


	public void setPlayer(string userId, int photoId)
	{
		setAvatar(photoId);
		_userId = userId;
		_isOccupied = true;
	}

	public string getId()
	{
		return this._userId;
	}

	private void setAvatar(int photoId)
	{
		GameObject ava = Resources.Load ("prefabs/gameAvatar"+(photoId+1), typeof(GameObject)) as GameObject;
		GameObject s = (GameObject)Instantiate(ava,Vector3.zero,Quaternion.identity,avatar.transform);
		s.GetComponent<RectTransform>().offsetMax = Vector2.zero;
		s.GetComponent<RectTransform>().offsetMin = Vector2.zero;
		s.GetComponent<RectTransform>().sizeDelta = new Vector2(150.0f,150.0f);
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
