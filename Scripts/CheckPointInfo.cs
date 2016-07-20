using UnityEngine;
using System.Collections;

public class CheckPointInfo : MonoBehaviour
{

	//when player pass on this checkpoint any time it sets isPassed to true always
	[HideInInspector]
	public bool isPassed = false;
	//if this checkpoint is the last checkpoit that the player passed by ,marke it as the save point
	[HideInInspector]
	public bool isActive = false;
	//To have the checkpoint order in the array.
	public int checkPointNum;
	//An array for all the enemys in the space of this checkpoint.
	public GameObject[] EmenyAroundCP;

	//Private:
	private bool isSaved = false;

	void Start ()
	{
		Debug.Log (SceneManager.SM.CheckPoints [0]);
		//assigning back values after loading.
		if (this != SceneManager.SM.CheckPoints [checkPointNum]) {
			this.isPassed = SceneManager.SM.CheckPoints [checkPointNum].isPassed;
			this.isActive = SceneManager.SM.CheckPoints [checkPointNum].isActive;
		}
	}

	void OnTriggerEnter (Collider enterd)
	{
		CheckPlace ();
		if (enterd.gameObject.tag == "Player" && SceneManager.SM.CheckPoints [SceneManager.SM.VIndexer + 1].isPassed == false) {
			this.isPassed = true; //this make this checkpoit marked as passed by
			SceneManager.SM.SetActivePoints (); //this method diactive all the checkpoints from being the last one
			this.isActive = true; //this mark this checkpoint as the last checkpoint and that act as save poit.
			SceneManager.SM.checkpointIndex = SceneManager.SM.VIndexer;//this save it's place in the array as the last checkpoint
			GameManager.GM.Player.currentScene = SceneManager.SM.sceneName;
			GameManager.GM.Save ();
			isSaved = true;
			Debug.Log (GameManager.GM.Player.currentScene);

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
