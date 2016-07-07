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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
	//Private:

	//Public
	public static List<GameObject> li_Enemys = new List<GameObject> ();

	//Pause assets
	public bool ispaused = false;
	public  bool isDead = false;

	//Game Manager static object
	public static GameManager GM;

	//Player Object.
	public PlayerState Player;

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
		//Player Support
		Player = new PlayerState ();
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

		if (isDead == true) {
			Time.timeScale = 0;
			DieCanvas.SetActive (true);
		}

	}

	public void Save ()
	{ //Saves data to a file
		BinaryFormatter bF = new BinaryFormatter (); //the formater that will write the data to the file
		FileStream playerFile = File.Open (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open); // the file that will contain the data
		//Datacontainer data
		bF.Serialize (playerFile, Player);
//		bF.Serialize (supportfile, );
	}

	
}

public class SupportData //Data container for support characters.
{
	// support level, can be adjust in training place
	public int in_Level;
	// did the player unlocked this support or not
	public bool isOpen;
	//Cool down for each support
	public float fl_CoolDown = 100;
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
//ADD INVINTORY OBJECT REFERENCE TO SCENE MANAGER

public class PlayerState //Data Container for Player state.
{
	//Player Skills state:- (All skills scale upon leveling up)

	//Health data. Initially 2000.
	public float health = 2000f;
	public float maxHealth = 2000f;
	//number of lives that player has. Initially 3
	public int lives = 3;
	//Mana data. Initially 1000.
	public float mana = 1000f;
	public float maxMana = 1000f;
	//Sprint data. Initially 20.
	public float sprintAmout = 20f;

	//Magic styles EXP:-(leveling up as 200,400,800,1600,3200... Max level is 30)

	//Thunder
	public int ThunderEXP = 3200;
	//Fire
	public int FireEXP = 0;
	//Ice
	public int IceEXP = 0;
	//BlackMagic
	public int BlackMagicEXP = 0;

	//Player Wisdom:- (Increase upon levleing up in a specific magic style. Each shot with a magic style increase it's EXP)

	//Thunder Wisdom. Initially five.
	public int ThunderWisdom = 5;
	//Fire Wisdom. Initially one.
	public int FireWisdom = 1;
	//Ice Wisdom. Initially one.
	public int IceWisdom = 1;
	//Black Magic Wisdom. Initially one.
	public int BlackMagicWisdom = 1;

	//Exp for player character. Initially zero. (leveling up as 200,400,800,1600,3200... Max level is 50) Player can aqquire EXP from killing enemies.
	public int EXP = 0;
	//Player Gold
	public int gold = 0;
	//Player Backbag that contain treasures, spell books, and any other item drop that can be used outside the level. Initially 20 free slots.
	public GameObject[] Backbag = new GameObject[20];
	//Player avilable magic styles. Initially only Thunder magic is true.
	public bool ThunderMagic = true;
	public bool FireMagic = false;
	public bool IceMagic = false;
	public bool BlackMagic = false;

}

[Serializable]
class DataContainer
{
	// This contain all the objects that are going to be saved in a file
	public PlayerState player;
	//Supports states
	public SupportData[] Supports = new SupportData[4];
	//public SceneManager
	public SMData Sm;
	//Current Inventory
	public INVData Inv;

	public DataContainer (PlayerState pl, SupportData cl, SupportData ad, SupportData et, SupportData la, SMData sm, INVData inv)
	{
		this.player = pl;
		this.Sm = sm;
		this.Inv = inv;
		this.Supports [0] = cl;
		this.Supports [1] = ad;
		this.Supports [2] = et;
		this.Supports [3] = la;
	}
}