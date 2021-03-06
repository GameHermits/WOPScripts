﻿using UnityEngine;
using System.Collections;

public class UIFunctions : MonoBehaviour
{

	//public:
	// null by default
	public string ScenetoLoad = null;
	//private:
	private AudioSource clicksound;

	void Awake ()
	{
		clicksound = gameObject.GetComponent <AudioSource> ();
	}

	public void LoadMainMenu ()
	{
		Application.LoadLevel (1);
	}

	public void LoadspecificScene ()
	{
		//Load a specific scene inputed manually in the inspector

		GameManager.GM.DieCanvas.SetActive (false);
		GameManager.GM.ReviveCanvas.SetActive (false);
		GameManager.GM.Player.ResetState ();
		Time.timeScale = 1;
		Application.LoadLevel (ScenetoLoad);
	}

	public void LoadScene ()
	{
		//Load scene from a file.

		GameManager.GM.Load ();
		GameManager.GM.DieCanvas.SetActive (false);
		GameManager.GM.ReviveCanvas.SetActive (false);
	}

	public void ExitGame ()
	{
		//Quits and clsoe Unity Player
		Application.Quit ();
	}

	public void NewGame ()
	{
		//starts a new game
		PlaySound ();
		GameObject.Destroy (SceneManager.SM.gameObject);
		Application.LoadLevel ("The Fortress Of The Dark Lands (Before)");
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
		GameManager.GM.Player.Revivetimes = 3;
		Application.LoadLevel (GameManager.GM.Player.currentSceneIndex);
		GameManager.GM.PlayerGameObject.GetComponent <Health_General> ().Heal ((GameManager.GM.Player.maxHealth), (GameManager.GM.Player.maxMana));
		Time.timeScale = 1;
	}

	public void TryAgain ()
	{
		//resume level after death
		SceneManager.SM.Revive ();
	}

	private void PlaySound ()
	{
		clicksound.Play ();
	}

	void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Q)) {
			switch (gameObject.name) {
			case "DieCanvas":
				LoadScene ();
				break;
			case "ReviveCanvas":
				TryAgain ();
				break;
			}
		} else if (Input.GetKeyUp (KeyCode.E)) {
			LoadspecificScene ();
		}
	}
}
