/*Class: SpawnGameObjects
 * Requires: none
 * Provides: a mechanism to spawn game objects in go_SpawnPrefab.
 * Definition: the class is attached to an empty game object that serve as the location of the spawn of certain prefabs specified fro mthe user.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnGameObjects_Spawner : MonoBehaviour
{

	public GameObject go_SpawnPrefab;
	public int int_EnemyCounter = 5;

	//minimum time between every spawning in seconds..
	public float fl_MinSpawnInterval_Sec = 3.0f;
	//maximum time between every spawning in seconds..
	public float fl_MaxSpawnInterval_Sec = 6.0f;

	//The target in which the spawned objects will should chase.
	public Transform trans_ChasedTarget;
	//the sensor gameobject of the current combat area.
	public Sense_Sensor Sensor;

	//Private:
	private float fl_SavedTime;
	//time between every spawning in seconds..
	private float SpawnInterval_Sec;
	
	// Use this for initialization
	void Start ()
	{
		fl_SavedTime = Time.time;
		SpawnInterval_Sec = Random.Range (fl_MinSpawnInterval_Sec, fl_MaxSpawnInterval_Sec);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time - fl_SavedTime >= SpawnInterval_Sec) { // is it time to spawn again?
			MakeThingToSpawn ();
			fl_SavedTime = Time.time; // store for next spawn
			SpawnInterval_Sec = Random.Range (fl_MinSpawnInterval_Sec, fl_MaxSpawnInterval_Sec);
		}
		if (int_EnemyCounter == 0) {
			GameObject.Destroy (this.gameObject);
		}
	}

	void MakeThingToSpawn ()
	{
		if (int_EnemyCounter != 0) {
			// create a new gameObject
			GameObject clone = Instantiate (go_SpawnPrefab, transform.position, transform.rotation) as GameObject;
			Sensor.Spawned [Sensor.globalIterator] = clone; // Adding every spawned enemy to the spawned list in the sensor.
			Sensor.globalIterator++;
			Sensor.startCheck = true; //Indicating to start checking the spawned list.
			int_EnemyCounter--;
			// set chaseTarget if specified
			if ((trans_ChasedTarget != null) && (clone.gameObject.GetComponent<EnemyBehavior_Enemy> () != null)) {
				clone.gameObject.GetComponent<EnemyBehavior_Enemy> ().SetTarget (trans_ChasedTarget);
			}
		} 

	}
}
