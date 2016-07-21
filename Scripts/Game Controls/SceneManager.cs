/* Class : Level Manager
 * Requires : Level assets
 * Provides : Level state, player progress, level Map, complete percentage of the level.
 * Definition : This class is attached to an empty gameObject set to the origin of the level. it differ from one level to another depending on the input data set manually by level designer.
 * Level manager is responsible for managing level state, providing checkpoints by calling save function located in GameMangaer. Also, provide overall enemies level and boss level, while maintaining the range
 * EXP and gold each enemy can give and how many treasures are there in the level.
 */
using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SceneManager : MonoBehaviour
{
	
	//if Unity couldn't find the Player GameObject with it's tag
	//put it manually by attaching it to this variable
	public GameObject Player;

	//The one and only copy of the SceneManager located in the current Scene. all access to SceneManager class should be through this object only.
	public static SceneManager SM;

	//Public:

	//Contain all Checkpoints location in the Scene. Initially empty gameObject if there is no model avaliable. zero index in any level is always the starter location of the player
	public CheckPointInfo[] CheckPoints;
	//contain all inputed string objectives.
	public string[] Objectives_Strings;
	//contains ObjectiveState objects
	public ObjectiveState[] objectives;
	//Indecate how many treasures are there in the Scene, each time the player opens one, this number is decreased
	public int treasureNumber;
	//Indecates the over all enemy levels in the current Scene.
	public int enemiesLevel;
	//Holds the scene name. It must be assigned manually in the inspector
	public string sceneName;
	//this to count all the enemies in the level
	public int TotalEnemies = 0;

	/*This int indecate the total progress the player did in the level, it's calculated as the following Formula
	(100 - TreasureNumber - LossEnemies.Length - Spawners.Lenght - ObjectivesIndex)*/
	[HideInInspector]
	public int totalProgress = 0;
	[HideInInspector]
	public bool isComplete = false;
	//index the active checkpoint in the checkpointinfo array
	[HideInInspector]
	public int activePoint = 0;
	//Contain int indecating if checkpoints was passed, where 0 = not passed, and 1 = passed
	[HideInInspector]
	public int[] PassedCPs;
	// Use this for initialization
	void Start ()
	{
		if (SM == null) {
			SM = this;
		}
		PassedCPs = new int[CheckPoints.Length];
		objectives = new ObjectiveState[Objectives_Strings.Length];
		MapObjectivesStrings (Objectives_Strings);
		for (int i = 0; i < PassedCPs.Length; i++) {
			if (PassedCPs [i] == null) {
				PassedCPs [i] = 0;
			}
		}
	}
	// Update is called once per frame
	void Update ()
	{	
		if (isComplete == true) {
			Inventory.INV.RemoveAllItem ();
			//GameManager.GM.ResetPlayerHP
			//show Victory canvas
		}
	}

	public void ResetSecneState ()
	{ //Reset Scene state according to checkpoints Defeintion.
		Time.timeScale = 1;
		GameManager.GM.DieCanvas.SetActive (false);
		GameManager.GM.isDead = false;
		Player.transform.position = CheckPoints [activePoint].gameObject.transform.position; //reset player to the last checkpoint he arrived to.
		for (int i = 0; i < CheckPoints.Length; i++) { //destroying all gameobjects in all passed checkpoints but the active one.
			if (i != activePoint && PassedCPs [i] == 1) {
				for (int j = 0; j < CheckPoints [i].EmenyAroundCP.Length; j++) {
					GameObject.Destroy (CheckPoints [i].EmenyAroundCP [j]);
				}
			}
		}
	}

	public int TotalProgress ()
	{ //Calulate the totalprogress and return totalProgress.
		
		return (100 - treasureNumber - TotalEnemies - objectives.Length);
	}

	void OnGUI ()
	{	
		if (Input.GetKey (KeyCode.O)) {
			GUI.contentColor = Color.yellow;
			GUI.skin.label.fontSize = 20;
			GUI.Box (new Rect (1000, 15, 400, Objectives_Strings.Length * 50), "");
			for (int i = 0; i < Objectives_Strings.Length; i++) {
				GUI.Label (new Rect (1000, 15 * (i + 1), 200, 30), Objectives_Strings [i]);
			}		
		}

	}

	public void MapObjectivesStrings (string[] objectivesStrings)
	{
		
		for (int i = 0; i < objectivesStrings.Length; i++) {
			if (i == 0) {
				SM.objectives [i] = new ObjectiveState (objectivesStrings [i], false, true);
			} else {
				SM.objectives [i] = new ObjectiveState (objectivesStrings [i], false, false);
			}
		}
	}
		
}

[Serializable]
public class ObjectiveState
{
	public string objective;
	public bool isComplete = false;
	public bool isMain = false;

	public ObjectiveState (string objective, bool isComplete, bool isMain)
	{
		this.objective = objective;
		this.isComplete = isComplete;
		this.isMain = isMain;
	}
}

[Serializable]
public class SMData
{
	//Contains all current Scene Manager data.
	//Public:
	//Contain all Checkpoints location in the Scene. Initially empty gameObject if there is no model avaliable. zero index in any level is always the starter location of the player
	public int[] PassedCPs = new int [SceneManager.SM.CheckPoints.Length];
	//contain all inputed string objectives.
	public string[] Objectives_Strings = new string[SceneManager.SM.Objectives_Strings.Length];
	//contains ObjectiveState objects
	public ObjectiveState[] objectives = new ObjectiveState[SceneManager.SM.objectives.Length];
	//Indecate how many treasures are there in the Scene, each time the player opens one, this number is decreased
	public int treasureNumber = 0;
	//Indecates the over all enemy levels in the current Scene.
	public int enemiesLevel = 0;
	//This Index CheckPoints array.. and is modified by checkpoint objects, that is, whenever a player reach the next checkpoint, this index is increased by one.
	public int checkpointIndex = 0;
	//this to count all the enemys in the level
	public int TotalEnemies = 0;
	/*This int indecate the total progress the player did in the level, it's calculated as the following Formula
	(100 - TreasureNumber - LossEnemies.Length - Spawners.Lenght - ObjectivesIndex)*/
	public int totalProgress = 0;

	public SMData (int[] PCP, string[] ObjSt, ObjectiveState[] ObjS, int treasureNumber, int enemiesLevel, int checkpointIndex, int TotalEnemies, int totalProgress)
	{
		// Assigning 
		for (int i = 0; i < PCP.Length; i++) {
			this.PassedCPs [i] = PCP [i];
		}

		for (int i = 0; i < ObjSt.Length; i++) {
			this.Objectives_Strings [i] = ObjSt [i];
		}

		for (int i = 0; i < ObjS.Length; i++) {
			this.objectives [i] = ObjS [i];
		}

		this.treasureNumber = treasureNumber;
		this.enemiesLevel = enemiesLevel;
		this.checkpointIndex = checkpointIndex;
		this.TotalEnemies = TotalEnemies;
		this.totalProgress = totalProgress;
	}
}