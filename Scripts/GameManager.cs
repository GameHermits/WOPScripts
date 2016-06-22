/*Class: GameManager
 * Requires: none
 * Provides: Over all controll of each level, saves the states of the player and manage game timer and pause and death functions, Data saving and providing for all other classes
 * Definition: This class is the manager class in the game where it combine all the basic controlls of each level and if there's different control for a specific level,
 * it is added here as a function and called using GM static object.
 * Note: This object must be attached to an empty game object in the scene, recommended to be at origin (0, 0, 0).
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
	//Private:
	private float fl_Revive = 3;

	//Public
	public static List<GameObject> li_Enemys = new List<GameObject> ();

	//Pause assets
	public bool ispaused = false;
	public  bool isDead = false;
	//Game Manager static object
	public static GameManager GM;

	//Canvas Refrences
	public GameObject PauseCanvas;
	public GameObject PlayerCanvas;
	public GameObject DieCanvas;

	//Support Characters
	public SupportData Clover;
	public SupportData Adam;
	public SupportData Ethan;
	public SupportData Lauren;

	void Awake ()
	{//Making sure there is only this Game Manager in all scenes and that it doesn't destroy when loading other scenes.
		if (GM == null) {
			DontDestroyOnLoad (gameObject);
			GM = this;
		} else if (GM != this) {
			Destroy (gameObject);
		}
		//Support Initilaization
		Clover = new SupportData (1, true, true);
		Adam = new SupportData (1, true, false);
		Ethan = new SupportData (1, true, false);
		Lauren = new SupportData (1, true, false);
	}

	void Start ()
	{//Initilaizations, Note: Start is called only once in the first scene of the game.

	}
	// Update is called once per frame
	void Update ()
	{
		// if player pressed "P" on keyboard pause the game
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

		if (isDead == true && fl_Revive > 0) {

			Time.timeScale = 0;
			DieCanvas.SetActive (true);
			fl_Revive--;
		}
		/*else{ This should indicate the global saving point for the player that can be changer whenever a player unlocks a new city.
			Application.LoadLevel ("");
		}*/
	}
	
}

public class SupportData //Data container for support characters.
{
	// support level, can be adjust in training place
	public int in_Level;
	// did the player unlocked this support or not
	public bool isOpen;
	// limit of use for one level, can be adjust in shop
	private int in_UseTimes = 3;
	// did the support exceed the limit of uses in one level or not
	private bool canUse;

	//Constructor for Initilaization
	public SupportData (int level, bool canUse, bool isOpen)
	{
		this.in_Level = level;
		this.isOpen = isOpen;
		this.canUse = canUse;
	}

	public bool CanUse ()
	{ // Check if support eceeded the limit of uses for one level and change canUse boolian to false
		if (in_UseTimes <= 0)
			canUse = false;
		return canUse;
	}

	public void Use ()
	{ // decrease in_UseTimes by one whenever it's called, usually called when the support is used in SupportJob function
		in_UseTimes--;
		Debug.Log (in_UseTimes);
	}
}

class PlayerState //Data Container for Player state.
{
	
}