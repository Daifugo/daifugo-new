using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void buttonHandler(string type)
	{
		PlayerPrefs.SetString("mode",type);
		SceneManager.LoadScene("singl1");
	}


}
