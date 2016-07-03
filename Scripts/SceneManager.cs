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
	private PlayerState PlayerInfo = GameManager.GM.Player;
	private GameObject Player = GameObject.FindGameObjectWithTag("Player");
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
	//This Index CheckPoints array.. and is modified by checkpoint objects, that is, whenever a player reach the next checkpoint, this index is increased by one.
	public int checkpointIndex = 0;
	//This is Index that loops in the array's CheckPoints elements to detect which is the last CheckPoint of them.
	public int VIndexer = 0;
	//Private:

	//this to count all the enemys in the level
	public int TotalEnemys=0;
	/*This int indecate the total progress the player did in the level, it's calculated as the following Formula
	(100 - TreasureNumber - LossEnemies.Length - Spawners.Lenght - ObjectivesIndex)*/
	private int totalProgress = 0;
	// Use this for initialization
	void Start ()
	{
		MapObjectivesStrings (Objectives_Strings);

		Player.transform.position = CheckPoints[0].gameObject.transform.position;
		AllEnemysNumber ();
	}
	// Update is called once per frame
	void FixedUpdate ()
	{	
		ResetSecneState ();
	}
	public string CurrentObjective ()
	{ //Return the current Objective in the Objectives array
		return "null";
	}
	public void ResetSecneState ()
	{ //Reset Scene state according to checkpoints Defeintion.
		if (PlayerInfo.health <= 0 && PlayerInfo.lives > 0)
		{
			PlayerInfo.lives--;
			Player.transform.position = CheckPoints [checkpointIndex].gameObject.transform.position;
		}
	}
	public int TotalProgress ()
	{ //Calulate the totalprogress and return totalProgress.
		
		return (100 - treasureNumber - TotalEnemys - objectives.Length);
	}
	void OnGUI()
	{	
		if (Input.GetKey(KeyCode.O)) 
		{
			GUI.contentColor = Color.yellow;
			GUI.skin.label.fontSize = 20;
			GUI.Box (new Rect (1000, 15, 400, Objectives_Strings.Length*50),"");
			for (int i = 0; i < Objectives_Strings.Length; i++) {
				GUI.Label (new Rect (1000, 15*(i+1), 200, 30), Objectives_Strings[i]);
			}		
		}

	}
	public void MapObjectivesStrings(string[] objectivesStrings)
	{
		
		for (int i = 0; i < objectivesStrings.Length; i++) {
			if (i==0) 
			{
				objectives [i] = new ObjectiveState (objectivesStrings[i],false,true);
			} 
			else 
			{
				objectives [i] = new ObjectiveState (objectivesStrings[i],false,false);
			}
		}
	}

	public void SetActivePoints ()
	{
		for (int i = 0; i < CheckPoints.Length; i++)
		{
			CheckPoints [i].gameObject.GetComponent<CheckPointInfo> ().isActive = false;
		}
	}

	public void AllEnemysNumber ()
	{
		for (int X = 0; X < CheckPoints.Length; X++) 
		{
			TotalEnemys += CheckPoints[X].EmenyAroundCP.Length + 1;
		}
	}

}
public class ObjectiveState
{
	public string objective;
	public bool isComplete = false;
	public bool isMain=false;
	public ObjectiveState(string objective,bool isComplete,bool isMain)
	{
		this.objective = objective;
		this.isComplete = isComplete;
		this.isMain = isMain;
	}
}