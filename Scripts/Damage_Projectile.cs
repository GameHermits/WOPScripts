/* Class : DamageProjectile
 * Uses : Attach to particle systems only, other objects that uses physics will not work with this class.
 * Requires : PlayerShooter and Player controller classes.
 * Provides : Different damage types ( freez, instant damage, poison, Area of effect).
 * Definition : This class is attached to customize different type of projectile and how will they act in the game.
 * The class calls functions form the object it hits, these functions are located in the folllowing scripts ( Health, Unity.RigidBody).
*/
using UnityEngine;
using System.Collections;

public class Damage_Projectile : MonoBehaviour
{

	// Instant Damage amount for every Projectile.
	public float fl_dmgAmount;
	// Freeze Damage amount for every projectile
	public float fl_FreezeDmgAmount;
	// how much should the freez effect lasts.
	public float fl_FreezeTime;
	// how much should the poison effect lasts.
	public float PoisonTime;
	public float fl_Radius;
	//radius for aoe
	// Types of Projectiles
	public enum ProjectileType
	{
		Freezedmg,
		InstantDmg,
		OverTimeDmg,
		AoeInstantDmg,
		AoeOverTimeDmg,
		AoeFreezeDmg}

	;
	// add a fucking aoe damage type for each of the above atttack. Make it as general as possible.

	public ProjectileType projectile = ProjectileType.AoeInstantDmg;

	//Private:
	private PlayerController_Player PlC_Ref;
	//A refrence object for the player controller compnent
	private PlayerShooter_MainCamera PS_Ref;
	//A refrence object for the player Shooter compnent
	//private EnemyBehavior_Enemy EB_Ref; // A refrnce to enemies main behavior component
	//private EnemyBehavior_Enemy EB_Ref; // A refrnce to enemies main behavior component
	IEnumerator Destroy (GameObject go)
	{
		yield return new WaitForSeconds (1.5f);
		GameObject.Destroy (go);
	}

	void Start ()
	{
		PlC_Ref = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController_Player> ();
		PS_Ref = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<PlayerShooter_MainCamera> ();


	}

	public void InstantDamage (GameObject col) // Called when Instant Damage Type of projectile is selected
	{
		if (col.gameObject.tag == "NEnemy" || col.gameObject.tag == "LEnemy") {// If hit an enemy, calls damage handling functions in it's health component
			col.gameObject.GetComponent<Health_General> ().ApplayDamage (fl_dmgAmount);
			col.gameObject.GetComponent<Health_General> ().DamageHealthBar (fl_dmgAmount);
			GameObject.Destroy (this.gameObject);

		} else if (col.gameObject.tag == "Player") {// If hit a Player, calls damage handling functions in it's health component
			col.gameObject.GetComponent<Health_General> ().ApplayDamage (fl_dmgAmount);
			col.gameObject.GetComponent<Health_General> ().DamageHealthBar (fl_dmgAmount);
			StartCoroutine (Destroy (gameObject));
		}
	}

	// Freezing effect
	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (fl_FreezeTime);
		PS_Ref.enabled = true;
		PlC_Ref.enabled = true;
	}

	public void FreezeDamage (GameObject col) // Called when Freeze Damage Type of projectile is selected
	{
		gameObject.GetComponent<TimedObjectDestructor> ().fl_TimeOut = fl_FreezeTime;
		gameObject.GetComponent <Rigidbody> ().velocity = Vector3.zero;
		gameObject.GetComponent <Rigidbody> ().angularVelocity = Vector3.zero;
		gameObject.GetComponent <Rigidbody> ().Sleep ();
		if (col.gameObject.tag == "Player") {
			
			PS_Ref.enabled = false;
			col.gameObject.GetComponent<Health_General> ().ApplayDamage (fl_FreezeDmgAmount);
			col.gameObject.GetComponent<Health_General> ().DamageHealthBar (fl_FreezeDmgAmount);
			StartCoroutine ("Wait");
		}  
	}

	public void PoisonDamage (GameObject col)
	{
		gameObject.GetComponent <TimedObjectDestructor> ().enabled = false;
		col.gameObject.GetComponent<Health_General> ().DamageOverTime (fl_dmgAmount, PoisonTime);
	}

	//make the sphere and get all colliders in that sphere then call the proper method to make the damage
	public void AoeDmg (Vector3 position, float radius)
	{
		var objectsInRange = Physics.OverlapSphere (position, radius);
		foreach (Collider col in objectsInRange) {
			if (col.gameObject != null) {// it should not make any nulls but ..... if any null came here must re check the over lab sphere totally from the begaining
				if (projectile == ProjectileType.AoeInstantDmg) {
					InstantDamage (col.gameObject);//send the game object of the collider to make the proper damage
				} else if (projectile == ProjectileType.AoeOverTimeDmg) {
					PoisonDamage (col.gameObject);//send the game object of the collider to make the proper damage
				} else if (projectile == ProjectileType.AoeFreezeDmg) {
					FreezeDamage (col.gameObject);//send the game object of the collider to make the proper damage
				}
			}
		}
	}

	void OnParticleCollision (GameObject col)
	{
		
		if (col.gameObject.tag == "Terrain") { // If the projectile hit the envoiroment
			GameObject.Destroy (this.gameObject);
		} else if (projectile == ProjectileType.InstantDmg)
			InstantDamage (col);
		else if (projectile == ProjectileType.Freezedmg)
			FreezeDamage (col);
		else if (projectile == ProjectileType.OverTimeDmg) {
			PoisonDamage (col);
		} else if (projectile == ProjectileType.AoeInstantDmg) {
			
			AoeDmg (col.gameObject.transform.position, fl_Radius);
		}
	}
}