using UnityEngine;
using System.Collections;

public class UIFunctions : MonoBehaviour
{

	//public:
	// null by default
	public string ScenetoLoad = null;
	//the game object holds the canvas
	public GameObject go_CanvasHolder = null;

	public void LoadspecificScene ()
	{
		//Load a specific scene inputed manually in the inspector 
		GameObject.Destroy (SceneManager.SM.gameObject);
		Application.LoadLevel (ScenetoLoad);
	}


	public void LoadingBarCanvas ()
	{// when click play btn
		go_CanvasHolder.SetActive (true);//show the canvas with the splash screen image
	}

	public void LoadScene ()
	{
		//Load scene from a file.
		GameManager.GM.Load ();
		//Application.LoadLevel (GameManager.GM.Player.currentScene);
		//SceneManager.SM.InstantiatePlayer ();
		//SceneManager.SM.ResetSecneState ();
	}

	public void ExitGame ()
	{
		Application.Quit ();
	}
}
