using UnityEngine;
using System.Collections;

public class EnemyShooter_Enemy : MonoBehaviour {
	
	public float fl_MovmentForce = 20.0f;
	public GameObject go_BulletPrefab;
	public float fl_FireRate = 2f;
	public GameObject go_ShootingPLace;

    private float fl_Timer = 10;
    private EnemyBehavior_Enemy eb_EnemyBehaviorRef;

    void Start()
    {
        eb_EnemyBehaviorRef = gameObject.GetComponent<EnemyBehavior_Enemy>();
    }
	// Update is called once per frame
	void Update () {
		if (!GameManager.GM.Paused){// If game isn't paused

            transform.LookAt(eb_EnemyBehaviorRef.tr_Target);
            if (fl_Timer <= 0)// after passing two seconds, assigne a new two seconds to timer.
                fl_Timer = 90f;
            fl_Timer--;
			Shoot ();
		}
	}

	void Shoot(){// Enable Enemy to shoot
		
		if (fl_Timer == fl_FireRate){// Controling fire rate by timer, every two seconds in timer Shoot

        GameObject newBullet = Instantiate(go_BulletPrefab, go_ShootingPLace.transform.position + go_ShootingPLace.transform.forward,gameObject.transform.rotation) as GameObject;
		newBullet.GetComponent<Rigidbody> ().AddForce (transform.forward * fl_MovmentForce, ForceMode.VelocityChange);
		}
	}
	
}
