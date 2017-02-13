using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSceneOverlay : MonoBehaviour {

	public GameObject stateText;
	public GameObject time;
	public GameObject parentTime;
	public GameObject parentFooter;

	public GameObject playerPassedTurn;
	public GameObject mainPlayerPassedTurn;
	public GameObject playerTurn;
	public GameObject mainPlayerTurn;

	public GameObject invalidMove;

	public GameObject playerWin;
	public GameObject mainPlayerWin;

	public GameObject newRound;
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
			mainPlayerPassedTurn.GetComponent<MainPlayerTurnview>().show(true);
		}
		else
		{
			playerPassedTurn.GetComponent<PlayerTurnView>().show(photoId, true);
		}
	}


	public void showTurn(bool isMainPlayer, int photoId)
	{
		gameObject.SetActive(true);

		if(isMainPlayer)
		{
			mainPlayerTurn.GetComponent<MainPlayerTurnview>().show(false);
		}
		else
		{
			playerTurn.GetComponent<PlayerTurnView>().show(photoId, false);
		}
	}

	public void showWin(bool isMainPlayer, int photoId)
	{
		gameObject.SetActive(true);

		if(isMainPlayer)
		{
			mainPlayerWin.GetComponent<MainPlayerTurnview>().show(true);
		}
		else
		{
			playerWin.GetComponent<PlayerTurnView>().show(photoId, true);
		}
	}

	public void showNewRound()
	{
		gameObject.SetActive(true);
		newRound.GetComponent<MainPlayerTurnview>().show(true);
	}


	public void showInvalid()
	{
		gameObject.SetActive(true);
		invalidMove.SetActive(true);
		invalidMove.GetComponent<Animator>().SetBool("show",true);
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
