using UnityEngine;
using System.Collections;

public class UIFunctions : MonoBehaviour
{

	//public:
	// null by default
	public string ScenetoLoad = null;
	//the game object holds the canvas
	public GameObject go_CanvasHolder;

	void LoadScene ()
	{
		Application.LoadLevel (ScenetoLoad);
	}


	public void LoadingBarCanvas ()
	{// when click play btn
		go_CanvasHolder.SetActive (true);//show the canvas with the splash screen image
	}

}
