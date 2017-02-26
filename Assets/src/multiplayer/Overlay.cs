using UnityEngine;
using System.Collections;

public class Overlay : MonoBehaviour {

	public GameObject messageBox;

	public void showMessageBox()
	{
		gameObject.SetActive(true);
		messageBox.GetComponent<Animator>().SetBool("show",true);
	}
}
