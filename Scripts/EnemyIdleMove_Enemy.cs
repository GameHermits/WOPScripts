using UnityEngine;
using System.Collections;

public class EnemyIdleMove_Enemy : MonoBehaviour {

	public float fl_MoveSpeed = 5f;		// enemy move speed
	public GameObject[] go_MyWaypoints;	// all waypoints
	private int in_MyWaypointId = 0;		// used as index for My_Waypoints
	
	void EnemyMovement() {// Moving the target toward a spacific waypoints, and repeat it.
		
		if(go_MyWaypoints.Length != 0){// if there isn't anything in My_Waypoints

            if (Vector3.Distance(go_MyWaypoints[in_MyWaypointId].transform.position, transform.position) <= 0){// if the enemy is close enough to waypoint, make it's new target the next waypoint
                in_MyWaypointId++;
			}
			
			if(in_MyWaypointId >= go_MyWaypoints.Length){// If the index reached the last element of the array, reset it.
				in_MyWaypointId = 0;
			}
            transform.LookAt(go_MyWaypoints[in_MyWaypointId].transform);
			transform.position = Vector3.MoveTowards(transform.position, go_MyWaypoints[in_MyWaypointId].transform.position, fl_MoveSpeed * Time.deltaTime);// move towards waypoint
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.GM.Paused) {
			EnemyMovement ();
		}
	}
}