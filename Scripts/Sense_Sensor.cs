/*Class: Sense
 * Requires: none
 * Provides: detection of player entering some area, and blocking road he entered from aftre a certain amount of time.
 * Definition: This class is used to detect player object that enter a spacific area and block the road behind him after a certain amount of time. the class can be generlized to serve more functionality.
 * Recommended: attach the class to an empty game object that have a rigidbody and collider set to trigger, while render set to disale
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sense_Sensor : MonoBehaviour
{
	//Spawners gameobjects that will be active. Note: Usually empty gameobjects.
	public GameObject go_Spawners;
	//the particle system that should block the road.
	public GameObject blockParticle;
	//array of all enemies spawned by spawners
	[HideInInspector]
	public GameObject[] Spawned;
	//Indicating if the spawners started spawning or not.
	[HideInInspector]
	public bool startCheck = false;
	// integer indicating array size, it also indicate how many spawned enemies player should defeat to open the way out of combat area
	public int clearingNumber;
	//Indicates where is the array indexer to spawners to access
	[HideInInspector]
	public int globalIterator;

	public bool blockinplace = true;
	public GameObject blockingTarget;
	//Private:

	//Iterates on spawned array in Update loop
	private int localIterator = 0;
	//Used to check if all gameobjects within spawned is null (Destroied)
	private int Indicator = 0;

	void Awake ()
	{
		Spawned = new GameObject[clearingNumber];
		globalIterator = 0;
	}

	void OnTriggerEnter (Collider col)
	{// when the player collide with sensor
		if (col.gameObject.tag == "Player") { // check this is the player
			go_Spawners.SetActive (true); // activate the spawners
		}
	}

	//when the player pass throw the place it will be blocked
	void OnTriggerExit (Collider block)
	{
		if (block.gameObject.tag == "Player") {
			this.GetComponent<Collider> ().isTrigger = false;
			blockParticle.SetActive (true);
			if (blockinplace == false) {
				transform.position = blockingTarget.transform.position;
				transform.rotation = blockingTarget.transform.rotation;
			}
		}
	}

	//idont think we need this function any more

	void Update ()
	{
		if (startCheck == true) {
			
			if (Indicator == clearingNumber)
				GameObject.Destroy (this.gameObject);
			else if (localIterator <= Spawned.Length) {

				if (Spawned [localIterator] == null) {
					if (Indicator != Spawned.Length) {
						Indicator++;
						localIterator++;
					} 
				}
			}
		}

	}
}