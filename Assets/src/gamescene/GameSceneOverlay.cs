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
