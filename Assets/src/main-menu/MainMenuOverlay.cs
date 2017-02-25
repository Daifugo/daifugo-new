using UnityEngine;
using System.Collections;

public class MainMenuOverlay : MonoBehaviour {

	public GameObject messageBox;

	public void showMessage()
	{
		gameObject.SetActive(true);
		messageBox.GetComponent<Animator>().SetBool("show", true);
	}
}
