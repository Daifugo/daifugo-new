using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainmenuController : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void singlePlayer()
	{
		SceneManager.LoadScene ("select");
	}

		public void join()
	{
		SceneManager.LoadScene ("join");
	}

}
