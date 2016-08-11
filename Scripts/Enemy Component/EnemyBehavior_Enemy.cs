/* Class : EnemyBehavior
 * Requires : Enemy component ( EnemyIdelMove, EnemyShooter, Enemy), NavMesh enviroment, NavMeshagent component.
 * Provides : Full functionality of AI enemy agent that gaurd or move in an area whenever out of combat, and attack and chase whoever attackes it or enter it's combat range.
 * Definition : The class is attached to the Enemy object along with other classes that define a complete AI enemy component. The class toggles between different classes that do different functionailty based
 * on the situation. The class also uses Unity.NavMesh to detect obstecals and calculate the shortest path to the target.
*/
using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

public class EnemyBehavior_Enemy : MonoBehaviour
{
	//private:
	private bool isHit = false;
	//Public:
	//Range in which enemy will start shooting the target
	public float fl_ShootingRange = 20f;
	// ranger in which enemy will start chasing the target
	public float fl_CombatRange = 30f;
	//public Vector3 vec_Target;
	public Transform tr_Target;
	//Controls Enemy behavior wether to stand still or move towards the player
	public bool ChasePlayer = true;
	// compnenet for chaser class
	[HideInInspector] 
	public NavMeshAgent nma_NavComponentRef;
	//refrence for enemy movement
	[HideInInspector] 
	public EnemyShooter_Enemy es_EnemyShooterRef;
	//refrence enemy idle move behavior
	[HideInInspector] 
	public EnemyIdleMove_Enemy em_EnemyMoveRef;
	// Use this for initialization
	void Start ()
	{
		// if no target specified, assume the player
		if (tr_Target == null) {

			if (GameObject.FindWithTag ("Player") != null)
				tr_Target = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
		}

		nma_NavComponentRef = gameObject.GetComponent<NavMeshAgent> ();
		es_EnemyShooterRef = gameObject.GetComponent<EnemyShooter_Enemy> ();
		em_EnemyMoveRef = gameObject.GetComponent<EnemyIdleMove_Enemy> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (tr_Target == null)
			return;
		
		//get the distance between the chaser and the target
		float distance = Vector3.Distance (transform.position, tr_Target.position);

		//If the player is out of this enemy combat range, set this enemy to it's Idle state
		if (distance > fl_CombatRange) {
			em_EnemyMoveRef.enabled = true;
			es_EnemyShooterRef.enabled = false;
			nma_NavComponentRef.enabled = false;

		}// If the Player is within this enemy combat range, set this enemy to it's ready to fight state. 
		else if ((distance <= fl_CombatRange && distance > fl_ShootingRange) || isHit) { // add (on taking damage) in the condition.
			em_EnemyMoveRef.enabled = false;
			es_EnemyShooterRef.enabled = false;
			nma_NavComponentRef.enabled = true;
			Chaser ();
		}//If the Player is near enough for this enemy to shoot, activate this enemy's shooting behavior.
		else if (distance <= fl_ShootingRange) {
			es_EnemyShooterRef.enabled = true;
			nma_NavComponentRef.enabled = false;
		}
				
	}

	// Set the target of the chaser
	public void SetTarget (Transform tr_newTarget)
	{
		tr_Target = tr_newTarget;
	}

	public void Chaser ()
	{
		if (ChasePlayer == true) {
			nma_NavComponentRef.SetDestination (tr_Target.position);
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Projectile")
			isHit = true;
	}
}
