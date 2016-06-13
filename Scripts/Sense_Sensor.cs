﻿/*Class: Sense
 * Requires: none
 * Provides: detection of player entering some area, and blocking road he entered from aftre a certain amount of time.
 * Definition: This class is used to detect player object that enter a spacific area and block the road behind him after a certain amount of time. the class can be generlized to serve more functionality.
 * Recommended: attach the class to an empty game object that have a rigidbody and collider set to trigger, while render set to disale
*/
using UnityEngine;
using System.Collections;

public class Sense_Sensor : MonoBehaviour
{
	// will have game object of spawners
	public GameObject go_Spawners;
	public GameObject blockParticle;

	void OnTriggerEnter (Collider col)
	{// when the player collide with sensor
		if (col.gameObject.tag == "Player") { // check this is the player
			go_Spawners.SetActive (true); // activate the spawners
			Invoke ("BlockRoad", 4);//activate rocks gameobject
			//anim_RocksAnimationRef.Play("Rock Animation");      
		}
	}

	//when the player pass throw the place it will be blocked
	void OnTriggerExit (Collider block)
	{
		if (block.gameObject.tag == "Player") {
			this.GetComponent<Collider> ().isTrigger = false;
			blockParticle.SetActive (true);
		}
	}

	//idont think we need this function any more

}