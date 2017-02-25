using UnityEngine;
using System.Collections;

public class MessageBox : MonoBehaviour {

	public void closeBoxHandler()
	{
		gameObject.SetActive(false);
		transform.parent.gameObject.SetActive(false);
	}
}
