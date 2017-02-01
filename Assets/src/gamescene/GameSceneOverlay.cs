using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSceneOverlay : MonoBehaviour {

	public GameObject stateText;
	Text textComponent = null;

	public void changeText(int state = -1)
	{
		switch(state)
		{
			case -1:
				textComponent.text = "Loading...";
			break;

			case 1:
				textComponent.text = "Deploying CPU Bot..";
			break;
		}
	}

	void Start()
	{
		textComponent = stateText.GetComponent<Text>();
	}


}
