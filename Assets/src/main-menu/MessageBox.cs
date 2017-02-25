using UnityEngine;
using System.Collections;

public class MessageBox : MonoBehaviour {

	public void closeBoxHandler()
	{
		gameObject.GetComponent<Animator>().SetBool("show",false);
	}
	
	// Unity Animation Event Callback
	
	public void disableParent()
	{
		transform.parent.gameObject.SetActive(false);
	}
}
