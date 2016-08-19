/*Class: TelchinesBehavior
 * Requires: TelchinesBehavior class to be attached to the same gameobjects.
 * Provides: Telchines movment behavior and attacking time.
 * Definition: this is a custom class for Telchines that descibe the overall behavoir of Telchines.
*/
using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour
{
	//Range in which boss is seeing the player. (boss battel field range)
	public float CombatRange;
	//Boss MovementSpeed
	public float fl_moveSpeed = 40f;
	//Array of waypoints (empty game objects)
	public GameObject[] go_wayPoints;
	// how much seconds to stop at each waypoint before moving to the other one.
	public float fl_moveTime = 5;
	[HideInInspector]
	//Controling Shooting Behavior of the boss
	public bool didShoot = false;
	//BossShooter Component
	private BossShooter BSRef;
	//Used for indexing the array of waypoints the boss should move to
	private int in_wayPointIdex = 0;
	//CoolDown of the movement at each waypoint
	private float MoveCD;
	//Player position
	private Transform Player;
	//Boss Health component
	private Health_General HP;
	//Storing the distance between boss and the player
	private float distance;

	void Start ()
	{
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
		BSRef = gameObject.GetComponent <BossShooter> ();
		HP = gameObject.GetComponent <Health_General> ();
	}

	void Shoot ()
	{
		//didShoot is asigned as true in the BossShooter after firing.
		if (didShoot == false) {
			BSRef.enabled = true;
		}
	}

	void Movement ()
	{
		// If reach the last element of array, make the next destination is the first element again.
		if (in_wayPointIdex >= go_wayPoints.Length) {
			in_wayPointIdex = 0;
		}
		//If reached the destinationed waypoint, reset cooldown and call shooting
		if (transform.position == go_wayPoints [in_wayPointIdex].transform.position) {
			MoveCD = Time.time + fl_moveTime;
			in_wayPointIdex++;
			Shoot ();
		} //If didn't reach destinationed waypoint, keep moving towards it. 
		else {
			transform.LookAt (go_wayPoints [in_wayPointIdex].transform);
			transform.position = Vector3.MoveTowards (transform.position, go_wayPoints [in_wayPointIdex].transform.position, fl_moveSpeed * Time.deltaTime);
			didShoot = false;
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		distance = Vector3.Distance (transform.position, Player.position);

		//If player is within combat range, start boss behavior
		if (distance <= CombatRange) {
			if (Time.time > MoveCD) {
				Movement ();
			} else {
				transform.LookAt (Player);
			}
		}//Heals the boss if player moved outside combat range 
		else if (HP.fl_health < HP.fl_maxhealth) {
			BSRef.enabled = false;
			HP.Heal (HP.fl_maxhealth, 0);
			transform.position = Vector3.MoveTowards (transform.position, go_wayPoints [0].transform.position, fl_moveSpeed * Time.deltaTime);
		}
			
	}
}
