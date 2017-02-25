using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Net;

public class MainmenuController : MonoBehaviour {

	public GameObject overlay;


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
		check("select");
	}

	public void join()
	{
		check("join");
	}
	
	
	void check(string sceneName)
	{
		bool isConnected = CheckForInternetConnection();
		
		if(isConnected)
		{
			SceneManager.LoadScene (sceneName);
		}
		else
		{
			overlay.GetComponent<MainMenuOverlay>().showMessage();
		}
	}
	
	
	bool CheckForInternetConnection()
	{
	    try
	    {
	        using (var client = new WebClient())
	        {
	            using (var stream = client.OpenRead("http://www.baidu.com"))
	            {
	                return true;
	            }
	        }
	    }
	    catch
	    {
	        return false;
	    }
	}
}
