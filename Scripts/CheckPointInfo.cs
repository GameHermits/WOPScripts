﻿using UnityEngine;
using System.Collections;

public class CheckPointInfo : MonoBehaviour
{
	/*
	//when player pass on this checkpoint any time it sets isPassed to true always
	[HideInInspector]
	public bool isPassed = false;
	//if this checkpoint is the last checkpoit that the player passed by ,marke it as the save point
	[HideInInspector]
	public bool isActive = false;
	*/
	//To have the checkpoint order in the array.
	public int checkPointNum;
	//An array for all the enemys in the space of this checkpoint.
	public GameObject[] EmenyAroundCP;

	//Private:
	private bool isSaved = false;

	void OnTriggerEnter (Collider enterd)
	{
		if (enterd.gameObject.tag == "Player" && SceneManager.SM.PassedCPs [checkPointNum + 1] == 0) {
			SceneManager.SM.activePoint = checkPointNum;//Mark this checkpoint as the active one
			SceneManager.SM.PassedCPs [checkPointNum] = 1;//Mark this checkpoint as passed
			isSaved = true;//Display save message on the screen
			GameManager.GM.Player.currentScene = SceneManager.SM.sceneName;//Store current scene name in playerstate data
			GameManager.GM.Save ();
			Debug.Log (GameManager.GM.Player.currentScene);

		} else if (enterd.gameObject.tag == "Player" && SceneManager.SM.PassedCPs [checkPointNum] == 0) {
			SceneManager.SM.PassedCPs [checkPointNum] = 1;//Mark this checkpoint as passed only. not active.
		} 

	}

	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (2f);
		isSaved = false;
	}

	[SerializeField]
	void onGUI ()
	{
		//display message to player inforimg them that the game is saved.
		if (isSaved == true) {
			GUI.contentColor = Color.yellow;
			GUI.skin.label.fontSize = 20;
			GUI.Box (new Rect (1000, 2000, 400, 200), "");
			GUI.Label (new Rect (1000, 2000, 100, 200), "Game Saved");
			StartCoroutine ("Wait");
		}

	}
}
