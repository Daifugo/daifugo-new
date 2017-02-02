using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarSceneOverlay : MonoBehaviour {

	public GameObject textOBject; 

	public void showJoin()
	{
		textOBject.GetComponent<Text>().text = "Joining Room...";
	}

	public void showStart()
	{
		textOBject.GetComponent<Text>().text = "Creating A Room...";
	}

	public void show()
	{
		gameObject.GetComponent<Image>().enabled = true;
	}

}
