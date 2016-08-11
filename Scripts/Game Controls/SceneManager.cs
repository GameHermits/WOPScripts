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

	//The one and only copy of the SceneManager located in the current Scene. all access to SceneManager class should be through this object only.
	public static SceneManager SM;

	//Public:
	//contain all inputed string objectives.
	public string[] Objectives_Strings;
	//contains ObjectiveState objects
	public ObjectiveState[] objectives;
	//Indecate how many treasures are there in the Scene, each time the player opens one, this number is decreased
	public int treasureNumber;
	//Indecates the over all enemy levels in the current Scene.
	public float enemiesLevel;
	//Holds the scene name. It must be assigned manually in the inspector
	public int sceneIndex;
	//this to count all the enemies in the level
	public int TotalEnemies = 0;

	/*This int indecate the total progress the player did in the level, it's calculated as the following Formula
	(100 - TreasureNumber - LossEnemies.Length - Spawners.Lenght - ObjectivesIndex)*/
	[HideInInspector]
	public int totalProgress = 0;
	[HideInInspector]
	public bool isComplete = false;
	//index the active vector3 place of checkpoint
	[HideInInspector]
	public float activeXPosition;
	[HideInInspector]
	public float activeYPosition;
	[HideInInspector]
	public float activeZPosition;
	//Index the temporary place to revive player in if needed.
	[HideInInspector]
	public Vector3 tempRevivePosition;
	//Control weather to revive player in the same place or in different place.
	[HideInInspector]
	public bool ReviveInPlace;
	//Private:
	private GameObject Player;

	void Awake ()
	{
		if (SM == null) {
			DontDestroyOnLoad (gameObject);
			SM = this;
		} else if (SM != this) {
			Destroy (gameObject);
		}

		//objectives = new ObjectiveState[Objectives_Strings.Length];
		//MapObjectivesStrings (Objectives_Strings);
		activeXPosition = -330.8f;
		activeYPosition = 282.2f;
		activeZPosition = 291.9f;
		ReviveInPlace = true;
	}
	// Update is called once per frame
	void Update ()
	{	
	}

	public void Revive ()
	{
		if (ReviveInPlace == false) {
			Player = GameObject.FindWithTag ("Player");
			Player.transform.position = tempRevivePosition;
		}

		GameManager.GM.ReviveCanvas.SetActive (false);
		GameManager.GM.Player.Revivetimes--;
		GameManager.GM.PlayerGameObject.GetComponent <Health_General> ().Heal ((GameManager.GM.Player.maxHealth / 2), (GameManager.GM.Player.maxMana / 2));
		GameManager.GM.PlayerGameObject.GetComponent <MouseLooker> ().LockCursor (true);
		ReviveInPlace = true;
		Time.timeScale = 1;
		GameManager.GM.PlayerGameObject.GetComponent <MouseLooker> ().enabled = true;
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

	//contain all inputed string objectives.
	public string[] Objectives_Strings = new string[SceneManager.SM.Objectives_Strings.Length];
	//contains ObjectiveState objects
	public ObjectiveState[] objectives = new ObjectiveState[SceneManager.SM.objectives.Length];
	//Indecate how many treasures are there in the Scene, each time the player opens one, this number is decreased
	public int treasureNumber = 0;
	//Indecates the over all enemy levels in the current Scene.
	public float enemiesLevel = 0;
	//this to count all the enemys in the level
	public int TotalEnemies = 0;
	/*This int indecate the total progress the player did in the level, it's calculated as the following Formula
	(100 - TreasureNumber - LossEnemies.Length - Spawners.Lenght - ObjectivesIndex)*/
	public int totalProgress = 0;
	//active posistion of checkpoint
	public float x;
	public float y;
	public float z;

	public int sceneIndex;

	public SMData (float x, float y, float z, string[] ObjSt, ObjectiveState[] ObjS, int treasureNumber, float enemiesLevel, int TotalEnemies, int totalProgress, int sceneIndex)
	{
		for (int i = 0; i < ObjSt.Length; i++) {
			this.Objectives_Strings [i] = ObjSt [i];
		}

		for (int i = 0; i < ObjS.Length; i++) {
			this.objectives [i] = ObjS [i];
		}

		this.treasureNumber = treasureNumber;
		this.enemiesLevel = enemiesLevel;
		this.TotalEnemies = TotalEnemies;
		this.totalProgress = totalProgress;
		this.x = x;
		this.y = y;
		this.z = z;
		this.sceneIndex = sceneIndex;
	}
}