using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AvatarSceneOverlay : MonoBehaviour {

	public GameObject textOBject;
	public GameObject errorMessageBox; 
	
	public void showError()
	{
		gameObject.SetActive(true);
		textOBject.GetComponent<Text>().text ="";
		errorMessageBox.GetComponent<Animator>().SetBool("show",true);
	}

	public void showLoading()
	{
		gameObject.SetActive(true);
		textOBject.GetComponent<Text>().text = "Loading..";
	}

}
