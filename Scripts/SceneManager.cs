/* Class : Level Manager
 * Requires : Level assets
 * Provides : Level state, player progress, level Map, complete percentage of the level.
 * Definition : This class is attached to an empty gameObject set to the origin of the level. it differ from one level to another depending on the input data set manually by level designer.
 * Level manager is responsible for managing level state, providing checkpoints by calling save function located in GameMangaer. Also, provide overall enemies level and boss level, while maintaining the range
 * EXP and gold each enemy can give and how many treasures are there in the level.
 */
using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
	//The one and only copy of the SceneManager located in the current Scene. all access to SceneManager class should be through this object only.
	public static SceneManager SM;

	//Public:
	//Contain all Checkpoints location in the Scene. Initially empty gameObject if there is no model avaliable. zero index in any level is always the starter location of the player
	public CheckPointInfo[] CheckPoints;
	//contain all inputed string objectives.
	public string[] Objectives_Stirngs;
	//Indecate how many treasures are there in the Scene, each time the player opens one, this number is decreased
	public int treasureNumber;
	//Indecates the over all enemy levels in the current Scene.
	public int enemiesLevel;
	//This Index CheckPoints array.. and is modified by checkpoint objects, that is, whenever a player reach the next checkpoint, this index is increased by one.
	public int checkpointIndex = 0;
	//Private:

	//ObjectiveIndex should decrease everytime an objective is completed
	private int ObjectiveIndex;
	/*This int indecate the total progress the player did in the level, it's calculated as the following Formula
	(100 - TreasureNumber - LossEnemies.Length - Spawners.Lenght - ObjectivesIndex)*/
	private int totalProgress = 0;


	// Use this for initialization
	void Start ()
	{
		ObjectiveIndex = Objectives.Length; 
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public string CurrentObjective ()
	{ //Return the current Objective in the Objectives array
		return "null";
	}

	public void ResetSecneState ()
	{ //Reset Scene state according to checkpoints Defeintion.
		
	}

	public int TotalProgress ()
	{ //Calulate the totalprogress and return totalProgress.
		return 0;
	}
}

public class ObjectiveState
{
	public string objective;
	public Transform objectiveLocation;
	public bool isComplete = false;
}