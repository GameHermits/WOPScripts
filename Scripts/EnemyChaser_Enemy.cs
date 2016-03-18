using UnityEngine;
using System.Collections;

public class EnemyChaser_Enemy : MonoBehaviour {

    private EnemyBehavior_Enemy eb_EnemyBehaviorRef;

	// Use this for initialization
	void Start () {
        eb_EnemyBehaviorRef = gameObject.GetComponent<EnemyBehavior_Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
        eb_EnemyBehaviorRef.nma_NavComponentRef.SetDestination(eb_EnemyBehaviorRef.tr_Target.position);
	}
}
