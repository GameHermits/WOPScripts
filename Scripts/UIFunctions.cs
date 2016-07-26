using UnityEngine;
using System.Collections;

public class UIFunctions : MonoBehaviour
{

	//public:
	// null by default
	public string ScenetoLoad = null;

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
		//Quits and clsoe Unity Player
		Application.Quit ();
	}

	public void NewGame ()
	{
		//starts a new game
		GameObject.Destroy (SceneManager.SM.gameObject);
		Application.LoadLevel ("The Fortress Of The Dark Lands (Before)");
		SceneManager.SM.InstantiatePlayer ();
	}

	public void NextScene ()
	{
		//load the new level after completeing the current one
		GameObject.Destroy (SceneManager.SM.gameObject);
		Application.LoadLevel (GameManager.GM.Player.currentSceneIndex + 1);
	}

	public void RestartScene ()
	{
		//Restart the current level
		GameObject.Destroy (SceneManager.SM.gameObject);
		Application.LoadLevel (GameManager.GM.Player.currentSceneIndex);
		GameManager.GM.Save ();
	}

	public void TryAgain ()
	{
		//resume level after death
		SceneManager.SM.Revive ();
	}

	void OnCollisionEnter (Collision col)
	{
		Debug.Log ("here");
		if (col.gameObject.tag == "Player") {
			Debug.Log ("working");
			NewGame ();
		}
	}
}
