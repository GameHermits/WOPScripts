using UnityEngine;
using System.Collections;

public class SpawnGameObjects_Spawner : MonoBehaviour {

	public GameObject go_SpawnPrefab;
	public int int_EnemyCounter = 5;

	public float fl_MinSpawnInterval_Sec = 3.0f;//minimum time between every spawning in seconds..
	public float fl_MaxSpawnInterval_Sec = 6.0f;//maximum time between every spawning in seconds..
	
	public Transform trans_ChasedTarget;
	
	private float fl_SavedTime;
	private float SpawnInterval_Sec;//time between every spawning in seconds..
	
	// Use this for initialization
	void Start () {
		fl_SavedTime = Time.time;
		SpawnInterval_Sec = Random.Range (fl_MinSpawnInterval_Sec, fl_MaxSpawnInterval_Sec);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - fl_SavedTime >= SpawnInterval_Sec) // is it time to spawn again?
		{
			MakeThingToSpawn();
			fl_SavedTime = Time.time; // store for next spawn
			SpawnInterval_Sec = Random.Range (fl_MinSpawnInterval_Sec, fl_MaxSpawnInterval_Sec);
		}	
	}

	void MakeThingToSpawn()
	{
		if (int_EnemyCounter != 0) {
			// create a new gameObject
			GameObject clone = Instantiate (go_SpawnPrefab, transform.position, transform.rotation) as GameObject;
			int_EnemyCounter--;
			// set chaseTarget if specified
			if ((trans_ChasedTarget != null) && (clone.gameObject.GetComponent<EnemyBehavior_Enemy> () != null)) {
				clone.gameObject.GetComponent<EnemyBehavior_Enemy> ().SetTarget (trans_ChasedTarget);
			}
		} else
			Destroy (this.gameObject);
	}
}
