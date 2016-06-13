/*Class: GameManager
 * Requires: none
 * Provides: Over all controll of each level, saves the states of the player and manage game timer and pause and death functions
 * Definition: This class is the manager class in the game where it combine all the basic controlls of each level and if there's different control for a specific level,
 * it is added here as a function and called using GM static object.
 * Note: This object must be attached to an empty game object in the scene, recommended to be at origin (0, 0, 0).
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

	public static List<GameObject> li_Enemys = new List<GameObject> ();

	
	public bool ispaused = false;
	public  bool isDead = false;
	private float life = 3;
	private static GameManager gm;

	public static GameManager GM {
		get {
			if (gm == null)
				gm = GameObject.FindObjectOfType<GameManager> ();
			return GameManager.gm;
		}
	}

	public GameObject PauseCanvas;
	public GameObject PlayerCanvas;
	public GameObject DieCanvas;
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.P)) {
			ispaused = !ispaused;

			if (ispaused == true) {
				Time.timeScale = 0;
				PauseCanvas.SetActive (true);
				PlayerCanvas.SetActive (false);
				Cursor.visible = true;
			} else {
				Time.timeScale = 1;
				PauseCanvas.SetActive (false);
				PlayerCanvas.SetActive (true);
			}
		}

		if (isDead == true && life > 0) {

			Time.timeScale = 0;
			DieCanvas.SetActive (true);
			life--;
		}
	}
	
}