using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

public class EnemyBehavior_Enemy : MonoBehaviour {
	
	public float fl_MovementSpeed = 25.0f;
	//public float fl_minDist = 0f;
	public float fl_ShootingRange = 20f;
	public float fl_CombatRange = 30f;
	public Transform tr_Target;
    //public Vector3 vec_Target;
    public NavMeshAgent nma_NavComponentRef;
    public EnemyShooter_Enemy es_EnemyShooterRef;
    public EnemyIdleMove_Enemy em_EnemyMoveRef;
    public EnemyChaser_Enemy ec_EnemyChaserRef;
    // Use this for initialization
    void Start () 
	{
		// if no target specified, assume the player
		if (tr_Target == null) {

			if (GameObject.FindWithTag ("Player")!=null)
				tr_Target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
		}

        nma_NavComponentRef = gameObject.GetComponent<NavMeshAgent>();
        es_EnemyShooterRef = gameObject.GetComponent<EnemyShooter_Enemy>();
        em_EnemyMoveRef = gameObject.GetComponent<EnemyIdleMove_Enemy>();
        ec_EnemyChaserRef = gameObject.GetComponent<EnemyChaser_Enemy>();
    }

    // Update is called once per frame
    void Update () 
	{
		if (!GameManager.GM.Paused) {
			if (tr_Target == null)
				return;

            // face the target
            //transform.LookAt (tr_Target);

            //get the distance between the chaser and the target

            float distance = Vector3.Distance (transform.position, tr_Target.position);

			//so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
			if (distance > fl_CombatRange) {
				em_EnemyMoveRef.enabled = true;
				es_EnemyShooterRef.enabled = false;
                ec_EnemyChaserRef.enabled = false;
                nma_NavComponentRef.enabled = false;
			}
            else if (distance <= fl_CombatRange && distance > fl_ShootingRange) { // add (on taking damage) in the condition.
				em_EnemyMoveRef.enabled = false;
                ec_EnemyChaserRef.enabled = true;
                es_EnemyShooterRef.enabled = false;
                nma_NavComponentRef.enabled = true;
			}
            else if (distance <= fl_ShootingRange) {
                ec_EnemyChaserRef.enabled = false;
                es_EnemyShooterRef.enabled = true;
                nma_NavComponentRef.enabled = false;
            }
				
		}
	}

	// Set the target of the chaser
	public void SetTarget(Transform tr_newTarget)
	{
		tr_Target = tr_newTarget;
	}
	

}
