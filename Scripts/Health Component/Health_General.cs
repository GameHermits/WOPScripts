/*Class: Health
 * Requires: HPcontroller
 * Provides: health amount points, applying damage of different kinds and calling upon HPController.
 * Definition: this is a part of Health component and the most generalized class that should be attached to any character object that should have health, it assaign health points for the character, apply damage when happens of different 
 * types of attacks and bullets. On top of that, it calls upon modifying the UI of the health bar by calling HPController class. it also destroy the game object when he reachs zero in health points.
*/
using UnityEngine;
using System.Collections;

public class Health_General : MonoBehaviour
{
	//public:
	//the main health variable
	public float fl_health;
	//the maxmum health value
	public float fl_maxhealth;
	//to hold any items the player gets like keys for gate
	public GameObject go_itemHolder;
	//private:
	//Refrence to gameobject hpcontroller script
	private HPController_General hpc_GameObjectRef;
	//Used to learn if gameobject is outofcombat or not.
	private float currentMaxHealth;
	//return value of IsOutOfIndex fucntion
	private bool shouldgenerat = true;
	//When equals to zero, means it's out of combat
	private float timer;

	void Start ()
	{

		hpc_GameObjectRef = gameObject.GetComponent<HPController_General> ();
		timer = 300;
		if (gameObject.tag == "Player") {
			fl_health = 0;
			fl_maxhealth = 0;
			go_itemHolder = null;
			currentMaxHealth = GameManager.GM.Player.maxHealth;
		}
		currentMaxHealth = fl_maxhealth;
	}

	void Update ()
	{
		shouldgenerat = IsOutOfCombat ();
		if (gameObject.tag == "Player") {
			if (GameManager.GM.Player.health <= 0) {
				if (GameManager.GM.Player.Revivetimes > 0) {
					GameManager.GM.Revive ();	
				} else if (GameManager.GM.Player.Revivetimes <= 0) {
					GameManager.GM.Dead ();
				}
			} else if (GameManager.GM.Player.health > GameManager.GM.Player.maxHealth) {
				GameManager.GM.Player.health = GameManager.GM.Player.maxHealth;
			} else if (GameManager.GM.Player.health <= GameManager.GM.Player.maxHealth * (1 / 4))
				GameManager.GM.Player.Fury = true;
			if (shouldgenerat == true) {
				Heal (0.1f * GameManager.GM.Player.level, 0.1f * GameManager.GM.Player.level);
			}

		} else {
			
			if (fl_health <= 0) {
			
				GameObject newGO = Instantiate (go_itemHolder, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
				GameObject.Destroy (gameObject);
			} else if (fl_health > fl_maxhealth)
				fl_health = fl_maxhealth;
			if (shouldgenerat == true) {
				Heal (0.1f * SceneManager.SM.enemiesLevel, 0.0f);
			}
		}
			
	}

	public void ApplayDamage (float fl_Damage) // Applying damage effect to the gameObject. the fucntion is called by the projectile collided with this gameObject
	{
		if (gameObject.tag == "Player") {
			GameManager.GM.Player.health -= fl_Damage - GameManager.GM.Player.magicResist;
		} else
			fl_health = fl_health - fl_Damage;
		DamageHealthBar (fl_Damage);
	}

	private void DamageHealthBar (float fl_Damage) // Modifying UI health bar acoording to Damage amount
	{
		if (gameObject.tag == "Player") {
			GameManager.GM.Player.healthAmount -= (fl_Damage / (GameManager.GM.Player.maxHealth));
		} else
			hpc_GameObjectRef.fl_tmpHealthbar = hpc_GameObjectRef.fl_tmpHealthbar - (fl_Damage / (fl_maxhealth));
	}

	public void Heal (float healthHealing, float manaHealing)
	{
		if (gameObject.tag == "Player") {
			GameManager.GM.Player.health += healthHealing;
			GameManager.GM.Player.mana += manaHealing;
		} else
			fl_health += healthHealing;
		HealHealthBar (healthHealing, manaHealing);
	}

	private void HealHealthBar (float healthHealing, float manaHealing) // Modifying UI health bar according to heal amount
	{
		if (gameObject.tag == "Player") {
			GameManager.GM.Player.healthAmount += (healthHealing / (GameManager.GM.Player.maxHealth));
			GameManager.GM.Player.manaAmount += (manaHealing / (GameManager.GM.Player.maxMana));
		} else
			hpc_GameObjectRef.fl_tmpHealthbar = hpc_GameObjectRef.fl_tmpHealthbar + (healthHealing / (fl_maxhealth));
	}

	IEnumerator Poison (float fl_Damage, float poisonTime) // Poison Behavior
	{

		if (poisonTime > 0) {
			yield return new WaitForSeconds (2f);
			DamageOverTime (fl_Damage, poisonTime);
		} else
			StopCoroutine ("Poison");
	}

	public void DamageOverTime (float fl_Damage, float poisonTime) // Apply Posion damage effect to the gameObject. the function is called by the projectile collided with this gameObject
	{
		if (gameObject.tag == "Player") {
			GameManager.GM.Player.health -= (fl_Damage);
		} else
			fl_health = fl_health - (fl_Damage);

		DamageHealthBar (fl_Damage);
		poisonTime--;
		fl_Damage -= 20;
		StartCoroutine (Poison (fl_Damage, poisonTime));
	}

	bool IsOutOfCombat ()
	{
		if (gameObject.tag == "Player") {
			if (GameManager.GM.Player.health < currentMaxHealth) {
				timer = 300;
				currentMaxHealth = GameManager.GM.Player.health;
			} else if (GameManager.GM.Player.health > currentMaxHealth) {
				currentMaxHealth = GameManager.GM.Player.health;
			} else if (GameManager.GM.Player.health == currentMaxHealth) {
				timer--;
			}
		} else {
			if (fl_health < currentMaxHealth) {
				timer = 300;
				currentMaxHealth = fl_health;
			} else if (fl_health > currentMaxHealth) {
				currentMaxHealth = fl_health;
			} else if (fl_health == currentMaxHealth) {
				timer--;
			}
		}
		if (timer <= 0) {
			return true;
		} else
			return false;
	}
}