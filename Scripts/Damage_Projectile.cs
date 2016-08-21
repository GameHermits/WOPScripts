﻿/* Class : DamageProjectile
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
	// how much should the Silence effect lasts.
	public float SilenceTime;
	//radius for aoe
	public float fl_Radius;
	//Explosion prefab
	public GameObject explosionShuriken;

	// Types of Projectiles
	public enum ProjectileType
	{
		Freezedmg,
		InstantDmg,
		OverTimeDmg,
		SilenceDmg,
		AoeInstantDmg,
		AoeOverTimeDmg,
		AoeFreezeDmg}

	;

	public enum Enemies
	{
		NEnemy,
		LEnemy}

	;

	public enum Aliaies
	{
		Player}

	;
	// add a fucking aoe damage type for each of the above atttack. Make it as general as possible.

	public ProjectileType projectile = ProjectileType.AoeInstantDmg;
	[HideInInspector]
	//for aoe instant dmg only
	public string summonerTag;
	[HideInInspector]
	//for freezedmg only
	public string freezeTag;
	[HideInInspector]
	//for freezed one refrence
	public GameObject freezedEnmey;

	//Private:
	private PlayerController_Player PlC_Ref;
	//A refrence object for the player controller compnent
	private PlayerShooter_MainCamera PS_Ref;

	void Start ()
	{
		//Player Refrences
		PlC_Ref = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController_Player> ();
		PS_Ref = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<PlayerShooter_MainCamera> ();
	}

	void Explosion ()
	{
		//Starting the Explosion after a collsion happend
		GameObject newExplosion = Instantiate (explosionShuriken, transform.position, transform.rotation) as GameObject;
		Destroy (gameObject);
	}

	public void InstantDamage (GameObject col) // Called when Instant Damage Type of projectile is selected
	{
		if (col.gameObject.tag == "NEnemy" || col.gameObject.tag == "LEnemy") {// If hit an enemy, calls damage handling functions in it's health component
			col.gameObject.GetComponent<Health_General> ().ApplayDamage (fl_dmgAmount);
			Explosion ();

		} else if (col.gameObject.tag == "Player") {// If hit a Player, calls damage handling functions in it's health component
			col.gameObject.GetComponent<Health_General> ().ApplayDamage (fl_dmgAmount);
			Explosion ();
		}
	}

	// Freezing effect
	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (fl_FreezeTime);
		PS_Ref.enabled = true;
		PlC_Ref.enabled = true;
		if (Enemies.IsDefined (typeof(Enemies), freezedEnmey.tag)) {
			freezedEnmey.gameObject.GetComponent<EnemyShooter_Enemy> ().enabled = true;
			//freezedEnmey.gameObject.GetComponent<EnemyIdleMove_Enemy> ().enabled = true;
			freezedEnmey.gameObject.GetComponent<EnemyBehavior_Enemy> ().enabled = true;
		}
	}

	public void FreezeDamage (GameObject col, string summontag) // Called when Freeze Damage Type of projectile is selected
	{
		//if the one to be hit is aliaies and the summoner is enemy
		if (Aliaies.IsDefined (typeof(Aliaies), col.gameObject.tag) && Enemies.IsDefined (typeof(Enemies), summontag)) {
			gameObject.GetComponent<TimedObjectDestructor> ().fl_TimeOut = fl_FreezeTime;
			gameObject.GetComponent <Rigidbody> ().velocity = Vector3.zero;
			gameObject.GetComponent <Rigidbody> ().angularVelocity = Vector3.zero;
			gameObject.GetComponent <Rigidbody> ().Sleep ();
			//here the only ally that coud be hit is player so I just stopped him
			PS_Ref.enabled = false;
			PlC_Ref.enabled = false;
			col.gameObject.GetComponent<Health_General> ().ApplayDamage (fl_FreezeDmgAmount);
			StartCoroutine ("Wait");
		} else if (Enemies.IsDefined (typeof(Enemies), col.gameObject.tag) && Aliaies.IsDefined (typeof(Aliaies), summontag)) {
			freezedEnmey = col.gameObject;
			col.gameObject.GetComponent<EnemyShooter_Enemy> ().enabled = false;
			//col.gameObject.GetComponent<EnemyIdleMove_Enemy> ().enabled = false;
			col.gameObject.GetComponent<EnemyBehavior_Enemy> ().enabled = false;
			col.gameObject.GetComponent<Health_General> ().ApplayDamage (fl_FreezeDmgAmount);
			StartCoroutine ("Wait");
		}
	}

	public void PoisonDamage (GameObject col)
	{
		gameObject.GetComponent <TimedObjectDestructor> ().enabled = false;
		col.gameObject.GetComponent<Health_General> ().DamageOverTime (fl_dmgAmount, PoisonTime);
	}

	public void SilenceDamage (GameObject col)
	{
		
	}

	//make the sphere and get all colliders in that sphere then call the proper method to make the damage
	public void AoeDmg (Vector3 position, float radius, string summoner)
	{
		bool ally = (Aliaies.IsDefined (typeof(Aliaies), summoner) ? true : false);
		var objectsInRange = Physics.OverlapSphere (position, radius);
		foreach (Collider col in objectsInRange) {
			if (col.gameObject != null) {// it should not make any nulls but ..... if any null came here must re check the over lab sphere totally from the begaining
				if (Aliaies.IsDefined (typeof(Aliaies), col.gameObject.tag) && ally == true) {//if summoner's tag is what we have do nothing as from logic ther will be no species will hit its species
					continue;
				} else if (Enemies.IsDefined (typeof(Enemies), col.gameObject.tag) && ally == false) {
					continue;
				} else if (projectile == ProjectileType.AoeInstantDmg) {
					InstantDamage (col.gameObject);//send the game object of the collider to make the proper damage
				} else if (projectile == ProjectileType.AoeOverTimeDmg) {
					PoisonDamage (col.gameObject);//send the game object of the collider to make the proper damage
				} else if (projectile == ProjectileType.AoeFreezeDmg)
					FreezeDamage (col.gameObject, summoner);//send the game object of the collider to make the proper damage
			}
		}
	}

	void OnParticleCollision (GameObject col)
	{
		
		if (col.gameObject.tag == "Terrain") { // If the projectile hit the envoiroment
			Debug.Log ("Fucking Terrain");
			Explosion ();
		} else if (projectile == ProjectileType.InstantDmg)
			InstantDamage (col);
		else if (projectile == ProjectileType.Freezedmg)
			FreezeDamage (col, freezeTag);
		else if (projectile == ProjectileType.OverTimeDmg) {
			PoisonDamage (col);
		} else if (projectile == ProjectileType.SilenceDmg) {
			SilenceDamage (col);
		} else if (projectile == ProjectileType.AoeInstantDmg) {
			AoeDmg (col.transform.position, fl_Radius, summonerTag);
			summonerTag = " ";
		}
	}

}