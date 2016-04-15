﻿using UnityEngine;
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
	// Types of Projectiles
	public enum ProjectileType
	{
		Freezedmg,
		InstantDmg,
		OverTimeDmg}

	;

	public ProjectileType projectile = ProjectileType.InstantDmg;


	//Private:
	private PlayerController_Player PlC_Ref;
	//A refrence object for the player controller compnent
	private PlayerShooter_MainCamera PS_Ref;
	//A refrence object for the player Shooter compnent
	private MouseLooker ML_Ref;
	//A refrence object for the player Mouse looker compnent
    
	//private EnemyBehavior_Enemy EB_Ref; // A refrnce to enemies main behavior component

	void Start ()
	{
		PlC_Ref = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController_Player> ();
		PS_Ref = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<PlayerShooter_MainCamera> ();
		ML_Ref = GameObject.FindGameObjectWithTag ("Player").GetComponent<MouseLooker> ();
	}

	IEnumerator Wait () // Freezing effect
	{
		yield return new WaitForSeconds (2.0f);
		PlC_Ref.enabled = true;
		PS_Ref.enabled = true;
		GameObject.Destroy (this.gameObject);
	}

	public void InstantDamage (Collider col) // Called when Instant Damage Type of projectile is selected
	{
		if (col.gameObject.tag == "NEnemy" || col.gameObject.tag == "LEnemy") {// If hit an enemy, calls damage handling functions in it's health component
			col.gameObject.GetComponent<Health_General> ().ApplayDamage (fl_dmgAmount);
			col.gameObject.GetComponent<Health_General> ().DamageHealthBar (fl_dmgAmount);
			GameObject.Destroy (this.gameObject);

		} else if (col.gameObject.tag == "Player") {// If hit a Player, calls damage handling functions in it's health component
			col.gameObject.GetComponent<Health_General> ().ApplayDamage (fl_dmgAmount);
			col.gameObject.GetComponent<Health_General> ().DamageHealthBar (fl_dmgAmount);
			GameObject.Destroy (this.gameObject);
		}
	}

	public void FreezeDamage (Collider col) // Called when Freeze Damage Type of projectile is selected
	{
		gameObject.GetComponent<TimedObjectDestructor> ().enabled = false;
		if (col.gameObject.tag == "Player") {
			PlC_Ref.enabled = false;
			PS_Ref.enabled = false;
			//          ML_Ref.enabled = false;
			col.gameObject.GetComponent<Health_General> ().ApplayDamage (fl_FreezeDmgAmount);
			col.gameObject.GetComponent<Health_General> ().DamageHealthBar (fl_FreezeDmgAmount);
			//animation.Play(animationName);
			StartCoroutine (Wait ());
		}  
	}

	public void DamageoverTimr (Collider col)
	{
		gameObject.GetComponent<TimedObjectDestructor> ().enabled = false;
		if (col.gameObject.tag == "Player") {
			while (PoisonTime > 0) {
				col.gameObject.GetComponent<Health_General> ().ApplayDamage (fl_FreezeDmgAmount);
				col.gameObject.GetComponent<Health_General> ().DamageHealthBar (fl_FreezeDmgAmount);
			}
			GameObject.Destroy (gameObject);
		}
	}

	void OnTriggerEnter (Collider col)
	{

		if (col.gameObject.tag == "Terrain") { // If the projectile hit the envoiroment
			//animation.Play(animationName);
			GameObject.Destroy (this.gameObject);
		} else if (projectile == ProjectileType.InstantDmg)
			InstantDamage (col);
		else if (projectile == ProjectileType.Freezedmg)
			FreezeDamage (col);
		else if (projectile == ProjectileType.OverTimeDmg)
			DamageoverTimr (col);
	}
}
