/*Class: EnemyShooter
 * Requires: EnemyBehavior object, target object
 * Provides: Shooting magic in different forms depend on the the enemy tag the class attached to.
 * Definition: This class is one of Enemy component where it define the behavior of shooting for different types of enemies based on their tags.
 * Recommended : set the class enable checkbox in the inspector to false for better performance and error-free.
*/
using UnityEngine;
using System.Collections;

public class EnemyShooter_Enemy : MonoBehaviour
{
	
	public float fl_MovmentForce = 20.0f;
	public GameObject go_BulletPrefab;
	public float fl_FireRate = 2f;
	public GameObject go_ShootingPLace;

	private EnemyBehavior_Enemy eb_EnemyBehaviorRef;
	private int in_randomInteger;
	private float fl_CoolDown = 0f;

	void Start ()
	{
		eb_EnemyBehaviorRef = gameObject.GetComponent<EnemyBehavior_Enemy> ();
	}
	// Update is called once per frame
	void Update ()
	{

		transform.LookAt (eb_EnemyBehaviorRef.tr_Target);
		if (Time.time >= fl_CoolDown) {// after passing spacific seconds, assigne a new value to timer.
			fl_CoolDown = Time.time + fl_FireRate;
			if (gameObject.tag == "NEnemy") {
				NEnemyShoot ();

			} else if (gameObject.tag == "LEnemy")
				LEnemyShoot ();
		}
	}

	void NEnemyShoot ()
	{// Enable Enemy to shoot
		
		GameObject newBullet = Instantiate (go_BulletPrefab, go_ShootingPLace.transform.position + go_ShootingPLace.transform.forward, go_ShootingPLace.transform.rotation) as GameObject;
        
		in_randomInteger = Random.Range (1, 3);
        
		switch (in_randomInteger) {
		case 1:
			fl_FireRate = 1f;
			break;
		case 2:
			fl_FireRate = 2f;
			break;
		}
		//AOE
		Damage_Projectile dp = newBullet.GetComponentInChildren (typeof(Damage_Projectile))as Damage_Projectile;
		if (dp.projectile.ToString () == "AoeInstantDmg") {
			dp.summonerTag = "NEnemy";
		} else if (dp.projectile.ToString () == "Freezedmg") {
			dp.freezeTag = "NEnemy";
		}
	}

	void LEnemyShoot ()
	{// Enable Enemy to shoot

        
		{// Controling fire rate by timer, every two seconds in timer Shoot

			GameObject newBullet = Instantiate (go_BulletPrefab, go_ShootingPLace.transform.position + go_ShootingPLace.transform.forward, go_ShootingPLace.transform.rotation) as GameObject;
			//newBullet.GetComponent<Rigidbody> ().AddForce (transform.forward * fl_MovmentForce, ForceMode.VelocityChange);

			in_randomInteger = Random.Range (1, 3);
			switch (in_randomInteger) {
			case 1:
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x + 10,
					gameObject.transform.position.y, gameObject.transform.position.z);                    
				break;
			case 2:
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x - 10,
					gameObject.transform.position.y, gameObject.transform.position.z);
				break;
			}
			//AOE
			Damage_Projectile dp = newBullet.GetComponentInChildren (typeof(Damage_Projectile))as Damage_Projectile;
			if (dp.projectile.ToString () == "AoeInstantDmg") {
				dp.summonerTag = "LEnemy";
			} else if (dp.projectile.ToString () == "Freezedmg") {
				dp.freezeTag = "LEnemy";
			}
		}
	}

}
