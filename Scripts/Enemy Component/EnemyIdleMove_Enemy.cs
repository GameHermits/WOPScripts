/*Class: EnemyIdleMove
 * Requires: none
 * Provides: cycle of walking in a straight line through a finit waypoints provided from the user.
 * Definition: This class is one of Enemy component where it provides enemy's idle satate either of standing, or walking in a cycle through a set of waypoints declared in go_MyWAypoints
 * Note: go_MyWaypoints is an array of empty game objects. can be attached to spawners game objects too to change spawn locations.
 * Recommended : set the class enable checkbox in the inspector to false for better performance and error-free.
*/
using UnityEngine;
using System.Collections;

public class EnemyIdleMove_Enemy : MonoBehaviour
{

	public float fl_MoveSpeed = 5f;
	// enemy move speed
	public GameObject[] go_MyWaypoints;
	// all waypoints
	private int in_MyWaypointId = 0;
	// used as index for My_Waypoints
	
	void EnemyMovement ()
	{// Moving the target toward a spacific waypoints, and repeat it.
		
		if (go_MyWaypoints.Length != 0) {// if there isn't anything in My_Waypoints

			if (Vector3.Distance (go_MyWaypoints [in_MyWaypointId].transform.position, transform.position) <= 0) {// if the enemy is close enough to waypoint, make it's new target the next waypoint
				in_MyWaypointId++;
			}
			
			if (in_MyWaypointId >= go_MyWaypoints.Length) {// If the index reached the last element of the array, reset it.
				in_MyWaypointId = 0;
			}
			transform.LookAt (go_MyWaypoints [in_MyWaypointId].transform);
			transform.position = Vector3.MoveTowards (transform.position, go_MyWaypoints [in_MyWaypointId].transform.position, fl_MoveSpeed * Time.deltaTime);// move towards waypoint
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		EnemyMovement ();
	}
}