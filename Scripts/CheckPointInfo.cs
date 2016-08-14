using UnityEngine;
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
	//An array for all the enemys in the space of this checkpoint.
	public GameObject[] EnemyAroundCP;

	//Private:
	private bool isSaved = false;

	void OnTriggerEnter (Collider enterd)
	{
		if (enterd.gameObject.tag == "Player") {
			isSaved = true;//Display save message on the screen
			//Setting this checkpoint as the active one
			SceneManager.SM.activeXPosition = transform.position.x;
			SceneManager.SM.activeYPosition = transform.position.y;
			SceneManager.SM.activeZPosition = transform.position.z;
			GameManager.GM.Player.currentSceneIndex = SceneManager.SM.sceneIndex;//Store current scene name in playerstate data
			Debug.Log (SceneManager.SM.activeXPosition + "" + transform.position.x);
			GameManager.GM.Save ();
			for (int i = 0; i < EnemyAroundCP.Length; i++) {
				if (EnemyAroundCP [i] != null) {
					GameObject.Destroy (EnemyAroundCP [i]);
				}
			}
		} 

	}

	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (4f);
		isSaved = false;
		GameObject.Destroy (gameObject);
	}

	void OnGUI ()
	{
		//display message to player inforimg them that the game is saved.
		if (isSaved == true) {
			GUI.contentColor = Color.yellow;
			GUI.skin.label.fontSize = 20;
			GUI.Label (new Rect (600, 0, 300, 200), "Game Saved");
			StartCoroutine ("Wait");
		}

	}
}
