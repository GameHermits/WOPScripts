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
		Application.LoadLevel (ScenetoLoad);
	}

	public void LoadScene ()
	{
		//Load scene from a file.
		GameManager.GM.Load ();
	}

	public void ExitGame ()
	{
		Application.Quit ();
	}

	public void NewGame ()
	{
		//starts a new game
		GameObject.Destroy (SceneManager.SM.gameObject);
		Application.LoadLevel ("The Fortress Of The Dark Lands (Before)");
	}

	public void NextScene ()
	{
		GameObject.Destroy (SceneManager.SM.gameObject);
		Application.LoadLevel (GameManager.GM.Player.currentSceneIndex + 1);
	}
}
