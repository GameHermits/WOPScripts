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
	//Contain all Spawners empty gameObject in the current Scene.
	public GameObject[] Spawners;
	//Contain all LossEnemies in the Scene.
	public GameObject[] LossEnemies;
	//Contain all Checkpoints location in the Scene. Initially empty gameObject if there is no model avaliable. zero index in any level is always the starter location of the player
	public Transform[] CheckPoints;
	//Contain all objectives in the current Scene.
	public Objective[] Objectives;
	//Indecate how many treasures are there in the Scene, each time the player opens one, this number is decreased
	public int TreasureNumber;
	//Indecates the over all enemy levels in the current Scene.
	public int EnemiesLevel;

	//Private:
	// The previous checkpoint index to Spawners array.
	private int preCheckPoint_Spawners = 0;
	//The current checkpoint index to Spawners array.
	private int currentCheckPoint_Spawners = 0;
	// The previous checkpoint index to LossEnemies array.
	private int preCheckPoint_LossEnemies = 0;
	//The current checkpoint index to LossEnemies array.
	private int currentCheckPoint_LossEnemies = 0;
	//The current checkpoint index to CheckPoints array.
	private int currentCheckPoint_CheckPoints = 0;
	/*Checkpoints Definition : Each array got two indexes, one that points for the latest checkpoint, and one that points to the current state. currectCheckPoint will increase by one everyTime a lossEnemy die
	 * or a Spawner is destroied, so that it alwasy points to the player progress through the Scene in terms of spawners and loss enemies. the preCheckpoint will equals the currentCheckPoint whenever a player
	 * saved the game in some checkpoint area in the Scene, so whenever the player dies, currentCheckpoint will equals preCheckpoint, and a function that spawn all object from the currentCheckPoint to the end
	 * of the arraies will be called to reset the state of the Scene, while player location will teleport back to the checkpoint location.
	 * */

	//ObjectiveIndex should decrease everytime an objective is completed
	private int ObjectiveIndex;
	//This int indecate the total progress the player did in the level, it's calculated as the following Formula
	//(100 - TreasureNumber - LossEnemies.Length - Spawners.Lenght - ObjectivesIndex)
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

public class Objective
{
	public string objective;
	public Transform objectiveLocation;
	public bool isComplete = false;
}