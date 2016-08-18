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
	//Public:

	//Keep track of player current scene.
	[HideInInspector]
	public int ScenesIndexer = 0;
	//Pause assets
	[HideInInspector]
	public bool ispaused = false;
	[HideInInspector]
	public  bool isDead = false;

	//Game Manager static object
	public static GameManager GM;

	//Player Object.
	public PlayerState Player;
	[HideInInspector]
	public GameObject PlayerGameObject;
	//Canvas Refrences
	public GameObject PauseCanvas;
	public GameObject PlayerCanvas;
	public GameObject DieCanvas;
	public GameObject ReviveCanvas;
	//Support Characters
	[HideInInspector]
	public SupportData Clover;
	[HideInInspector]
	public SupportData Adam;
	[HideInInspector]
	public SupportData Ethan;
	[HideInInspector]
	public SupportData Lauren;
	//Is the game loaded or was a new game/level (for controlling player starting locaiton upon loading a scene)
	[HideInInspector]
	public bool isLoadedGame;
	//Private:
	private DataContainer data;
	private bool doesExists = true;

	void Awake ()
	{//Making sure there is only this Game Manager in all scenes and that it doesn't destroy when loading other scenes.
		if (GM == null) {
			DontDestroyOnLoad (gameObject);
			GM = this;
		} else if (GM != this) {
			Destroy (gameObject);
		}
		//Player State and object.
		Player = new PlayerState ();
		//Support Initilaization
		Clover = new SupportData (1, true, true);
		Adam = new SupportData (1, true, false);
		Ethan = new SupportData (1, true, false);
		Lauren = new SupportData (1, true, false);
		isLoadedGame = false;
	}

	// Update is called once per frame
	void Update ()
	{
		
		// if player pressed "P" on keyboard pause the game
		if (Input.GetKeyDown (KeyCode.P) || Input.GetKeyDown (KeyCode.Escape)) { 
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

	}

	public void Dead ()
	{
		// called when player loses all revive times and has to restart the level.
		Time.timeScale = 0;
		DieCanvas.SetActive (true);
	}

	public void Revive ()
	{
		//called when player is revived.
		Time.timeScale = 0;
		ReviveCanvas.SetActive (true);
	}

	public void Save ()
	{ //Saves data to a file
		BinaryFormatter bF = new BinaryFormatter (); //the formater that will write the data to the file
		FileStream playerFile = File.Create (Application.persistentDataPath + "/PlayerInfo.dat"); // the file that will contain the data

		DataContainer data = new DataContainer (Player, Clover, Adam, Ethan, Lauren);
		bF.Serialize (playerFile, data);
		playerFile.Close ();
	}

	public void Load ()
	{ //Load data from a file
		if (File.Exists (Application.persistentDataPath + "/PlayerInfo.dat") == true) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream playerFile = File.Open (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);

			data = (DataContainer)bf.Deserialize (playerFile);
			playerFile.Close ();

			AssignBack (data);
			isLoadedGame = true;
			Application.LoadLevel (Player.currentSceneIndex);
			Time.timeScale = 1;

		} else {
			doesExists = false;
		}
	}

	private void AssignBack (DataContainer data)
	{
		//Assigne values from the object that pulled the data from a file to each spacific object

		//Player Assignment.
		Player = data.player;
		//Support Assingments. 
		Clover = data.Supports [0];
		Adam = data.Supports [1];
		Ethan = data.Supports [2];
		Lauren = data.Supports [3];
		//Inventory Assignments.

		//Scenemanager assignments
		for (int i = 0; i < SceneManager.SM.Objectives_Strings.Length; i++) {
			SceneManager.SM.Objectives_Strings [i] = data.Sm.Objectives_Strings [i];
		}
		for (int i = 0; i < SceneManager.SM.objectives.Length; i++) {
			SceneManager.SM.objectives [i] = data.Sm.objectives [i];
		}
		SceneManager.SM.enemiesLevel = data.Sm.enemiesLevel;
		SceneManager.SM.treasureNumber = data.Sm.treasureNumber;
		SceneManager.SM.TotalEnemies = data.Sm.TotalEnemies;
		SceneManager.SM.totalProgress = data.Sm.totalProgress;
		SceneManager.SM.activeXPosition = data.Sm.x;
		SceneManager.SM.activeYPosition = data.Sm.y;
		SceneManager.SM.activeZPosition = data.Sm.z;
		SceneManager.SM.sceneIndex = data.Sm.sceneIndex;
	}

	void OnGUI ()
	{
		if (doesExists == false) {
			GUI.contentColor = Color.yellow;
			GUI.skin.label.fontSize = 20;
			GUI.Label (new Rect (600, 1000, 300, 200), "No Saved Data");
		}
	}
}

[Serializable]
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
	}
}
//ADD INVINTORY OBJECT REFERENCE TO SCENE MANAGER

[Serializable]
public class PlayerState //Data Container for Player state.
{
	//Player Skills state:- (All skills scale upon leveling up)

	//Health data. Initially 2000.
	public float health = 2000f;
	public float maxHealth = 2000f;

	//Mana data. Initially 1000.
	public float mana = 1000f;
	public float maxMana = 1000f;
	//Sprint data. Initially 20.
	public float sprintAmout = 20f;
	//Jump data. Initially 10.
	public float maxJump = 3.5f;
	//Movement speed data. Initially 10.
	public float movementSpeed;
	//Boots speed
	public float BootsSpeed = 10f;
	//Player PowerStones
	public string powerStone = "";
	public int powerStoneLevel = 0;
	//Player Magic resist outfit
	public float magicResist = 10;
	//Fury Ability variables
	public bool Fury = false;

	//How many tries does the player have.
	public int Revivetimes = 3;
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
	//Player level
	public float level = 1;
	//Player Gold
	public int gold = 0;
	//Player Iventory of items that can be used within the level
	public string[] Inventory = new string[6];
	//Inventory Index
	public int InvIndex = 0;

	//Player avilable magic styles. Initially only Thunder magic is true.
	public bool ThunderMagic = true;
	public bool FireMagic = false;
	public bool IceMagic = false;
	public bool BlackMagic = false;

	//Latest scene player arrived to.
	public int currentSceneIndex = 2;

	//Player UI state
	public float healthAmount = 1;
	public float energyAmount = 1;
	public float manaAmount = 1;

	public void ResetState ()
	{ // Resets the player state to the default state.
		health = 2000;
		maxHealth = 2000;
		mana = 1000;
		maxMana = 1000;
		sprintAmout = 20f;
		maxJump = 3.5f;
		BootsSpeed = 10f;
		powerStone = "";
		powerStoneLevel = 0;
		magicResist = 10;
		Fury = false;
		Revivetimes = 3;
		ThunderEXP = 3200;
		FireEXP = 0;
		IceEXP = 0;
		BlackMagicEXP = 0;
		ThunderWisdom = 5;
		FireWisdom = 1;
		IceWisdom = 1;
		BlackMagicWisdom = 1;
		EXP = 0;
		level = 1;
		gold = 0;
		InvIndex = 0;
		ThunderMagic = true;
		FireMagic = false;
		IceMagic = false;
		BlackMagic = false;
		currentSceneIndex = 2;
		healthAmount = 1;
		energyAmount = 1;
		manaAmount = 1;

		for (int i = 0; i < 6; i++) {
			Inventory [i] = "";
		}

	}
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
	//public INVData Inv;

	public DataContainer (PlayerState pl, SupportData cl, SupportData ad, SupportData et, SupportData la)
	{
		this.player = pl;
		this.Supports [0] = cl;
		this.Supports [1] = ad;
		this.Supports [2] = et;
		this.Supports [3] = la;
		this.Sm = new SMData (SceneManager.SM.activeXPosition, SceneManager.SM.activeYPosition, SceneManager.SM.activeZPosition, SceneManager.SM.Objectives_Strings, SceneManager.SM.objectives, SceneManager.SM.treasureNumber,
			SceneManager.SM.enemiesLevel, SceneManager.SM.TotalEnemies, SceneManager.SM.totalProgress, SceneManager.SM.sceneIndex);
		//this.Inv = new INVData (/*Inventory.INV.bag,*/ Inventory.INV.Ibag);
	}
}