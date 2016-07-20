using UnityEngine;
using System.Collections;

public class CheckPointInfo : MonoBehaviour
{

	//when player pass on this checkpoint any time it sets isPassed to true always
	public bool isPassed;
	//if this checkpoint is the last checkpoit that the player passed by ,marke it as the save point
	public bool isActive;
	//An array for all the enemys in the space of this checkpoint.
	public GameObject[] EmenyAroundCP;


	void OnTriggerEnter (Collider enterd)
	{
		CheckPlace ();
		if (enterd.gameObject.tag == "Player" && SceneManager.SM.CheckPoints [SceneManager.SM.VIndexer + 1].isPassed == false) {
			this.isPassed = true; //this make this checkpoit marked as passed by
			SceneManager.SM.SetActivePoints (); //this method diactive all the checkpoints from being the last one
			this.isActive = true; //this mark this checkpoint as the last checkpoint and that act as save poit.
			SceneManager.SM.checkpointIndex = SceneManager.SM.VIndexer;//this save it's place in the array as the last checkpoint
			GameManager.GM.Player.currentScene = SceneManager.SM.sceneName;

		} else if (enterd.gameObject.tag == "Player" && this.isPassed == false) {
			this.isPassed = true; //this make this checkpoit marked as passed by
		} 

	}

	void CheckPlace ()
	{
		for (int i = 0; i < SceneManager.SM.CheckPoints.Length; i++) {
			if (SceneManager.SM.CheckPoints [i] == this) {
				SceneManager.SM.VIndexer = i;
			}
		}
	}

}
