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
	//public:
	//Refrence to the projectile
	public GameObject go_BulletPrefab;
	//refrence to fire rate
	public float fl_FireRate = 2f;
	//refrence to the place where the projectiles are lucnhed from.
	public GameObject go_ShootingPLace;
	//Contains all possible behaviors for shooting.
	public enum ShootingBehavior
	{
		DoubleAttack,
		InstanteTransportation,
		Stealth}

	;
	//Used to set the behavior of each indivisual enemy in the inspector.
	public ShootingBehavior shootingBehavior = ShootingBehavior.DoubleAttack;
	//private:
	//Refrence to EnemyBehavior script attached to the gameObject
	private EnemyBehavior_Enemy eb_EnemyBehaviorRef;
	//Used for Randomization behavior.
	private int in_randomInteger;
	//Used for shooting cooldown.
	private float fl_CoolDown = 0f;

	void Shoot ()
	{
		
	}

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
			switch (shootingBehavior) {
			case ShootingBehavior.DoubleAttack:
				DoubleAttack ();
				break;
			case ShootingBehavior.InstanteTransportation:
				InstantTransportation ();
				break;
			case ShootingBehavior.Stealth:
				Stealth ();
				break;
			}
		}
	}

	void DoubleAttack ()
	{// Implements behavior that let the enemy double attack randomly.
		
		GameObject newBullet = Instantiate (go_BulletPrefab, go_ShootingPLace.transform.position + go_ShootingPLace.transform.forward, go_ShootingPLace.transform.rotation) as GameObject;
		if (!newBullet.GetComponent <Rigidbody> ()) {
			newBullet.AddComponent <Rigidbody> ();
		}
		newBullet.GetComponent <Rigidbody> ().AddForce (go_ShootingPLace.transform.forward * 30, ForceMode.Impulse);

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
			dp.summonerTag = gameObject.tag;
		} else if (dp.projectile.ToString () == "Freezedmg") {
			dp.freezeTag = gameObject.tag;
		}
	}

	void InstantTransportation ()
	{// Implements behavior that let the enemy transport to different near location once he/she shoot.

		GameObject newBullet = Instantiate (go_BulletPrefab, go_ShootingPLace.transform.position + go_ShootingPLace.transform.forward, go_ShootingPLace.transform.rotation) as GameObject;
		if (!newBullet.GetComponent <Rigidbody> ()) {
			newBullet.AddComponent <Rigidbody> ();
		}
		newBullet.GetComponent <Rigidbody> ().AddForce (go_ShootingPLace.transform.forward * 30, ForceMode.Impulse);

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
			dp.summonerTag = gameObject.tag;
		} else if (dp.projectile.ToString () == "Freezedmg") {
			dp.freezeTag = gameObject.tag;
		}
	}

	void Stealth ()
	{//Implements behavior that let the enemy stealth as long as he/she doesn't attack.
		
	}

}
