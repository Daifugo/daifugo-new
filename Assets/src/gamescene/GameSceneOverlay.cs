using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSceneOverlay : MonoBehaviour {

	public GameObject stateText;
	public GameObject time;
	public GameObject parentTime;
	public GameObject parentFooter;

	public GameObject playerPassedTurn;
	public GameObject MainPlayerPassedTurn;
	public GameObject playerTurn;

	Text textComponent = null;

	public void changeText(int state = -1)
	{
		switch(state)
		{
			case -1:
				textComponent.text = "Loading...";
			break;

			case 1:
				textComponent.text = "Deploying CPU Bot...";
			break;

			case 2:
				textComponent.text = "Distributing Cards...";
			break;

			case 3:
				textComponent.text = "Waiting for other players to join...";
			break;

			case 4:
				textComponent.text = "Start Game...";
				stateText.SetActive(false);
			break;
		}
	}


	public void showPassedTurn(bool isMainPlayer, int photoId)
	{
		gameObject.SetActive(true);

		if(isMainPlayer)
		{
			MainPlayerPassedTurn.SetActive(true);
			MainPlayerPassedTurn.GetComponent<Animator>().SetBool("show",true);
		}
		else
		{
			playerPassedTurn.SetActive(true);

			var avatar = returnAvatar(photoId+1, playerPassedTurn.transform);
			avatar.GetComponent<Animator>().SetBool("show",true);

			playerPassedTurn.GetComponent<Animator>().SetBool("show",true);
		}
	}


	GameObject returnAvatar(int photoId, Transform parent)
	{
		var avatar1 = Resources.Load("prefabs/overlayAvatar", typeof(GameObject)) as GameObject;
		var avatar = Instantiate (avatar1, Vector3.zero, Quaternion.identity) as GameObject;	

		avatar.transform.SetParent(parent);

		avatar.GetComponent<Image>().sprite = Util.getSprite("overlay/ava" + photoId);

		var rect = avatar.GetComponent<RectTransform>();
		rect.anchoredPosition = new Vector2(66.0f,0.0f);
			
		return avatar;
	}


	public void changeTime(int timeInt)
	{

		if(!parentTime.activeInHierarchy)
		{
			parentFooter.SetActive(true);
			parentTime.SetActive(true);
		}

		char[] splitChar = {' '};
		Text timeTxt = time.GetComponent<Text>();
		string[] splitTxt = timeTxt.text.Split(splitChar);
		timeTxt.text = splitTxt + " " + timeInt.ToString();
	}


	void Start()
	{
		gameObject.GetComponent<Image>().enabled = true;
		textComponent = stateText.GetComponent<Text>();
		changeText();
	}


}
