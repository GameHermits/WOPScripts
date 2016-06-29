/*Class: TelchinesBehavior
 * Requires: TelchinesBehavior class to be attached to the same gameobjects.
 * Provides: Telchines movment behavior and attacking time.
 * Definition: this is a custom class for Telchines that descibe the overall behavoir of Telchines.
*/
using UnityEngine;
using System.Collections;

public class TelchinesBehavior : MonoBehaviour
{

	private int in_wayPointIdex = 0;
	//Used for indexing the array of waypoints the boss should move to
	public float fl_moveSpeed = 40f;
	// Boss movement speed
	public GameObject[] go_wayPoints;
	//Array of waypoints (empty game objects)
	public float fl_moveTime = 300f;
	// how much to stop at each waypoint. each 60 unit = 1 sec
	public TelchinesShooter ts_TelchinesShooterRef;
	//TelchinesShooter Composition

	void Movement ()
	{ // a function that move the boss
		
		// If reach the last element of array, start over.
		if (in_wayPointIdex >= go_wayPoints.Length) {
			in_wayPointIdex = 0;
		}
		transform.position = Vector3.MoveTowards (transform.position, go_wayPoints [in_wayPointIdex].transform.position, fl_moveSpeed * Time.deltaTime);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (fl_moveTime <= 0) {
			in_wayPointIdex++;
			fl_moveTime = 400f;
			ts_TelchinesShooterRef.enabled = true;
		} else {
			fl_moveTime--;
			Movement ();
		}
			
	}
}
