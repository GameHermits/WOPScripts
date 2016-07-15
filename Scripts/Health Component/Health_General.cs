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
	

	public float fl_health;
	//the main health variable
	 
	public float fl_maxhealth;
	//the maxmum health value

	public GameObject go_itemHolder;
	//to hold any items the player gets like keys for gate

	private GameObject test;
	//this is for testing
	private HPController_General hpc_GameObjectRef;

	void Start ()
	{

		hpc_GameObjectRef = gameObject.GetComponent<HPController_General> ();

		if (gameObject.tag == "Player") {
			fl_health = GameManager.GM.Player.health;
			Debug.Log (GameManager.GM.Player.health);
			fl_maxhealth = GameManager.GM.Player.maxHealth;
		}
	}

	void Update ()
	{
		if (fl_health <= 0) {
			if (gameObject.name != "Player") {
				GameObject newGO = Instantiate (go_itemHolder, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
				GameObject.Destroy (gameObject);
			} else if (gameObject.name == "Player") {
				GameManager.GM.isDead = true;
			}
		} else if (fl_health > fl_maxhealth)
			fl_health = fl_maxhealth;


	}

	public void ApplayDamage (float fl_Damage) // Applying damage effect to the gameObject. the fucntion is called by the projectile collided with this gameObject
	{
		fl_health = fl_health - (fl_Damage / 2);
	}

	public void DamageHealthBar (float fl_Damage) // Modifying UI health bar acoording to Damage amount
	{
        
		hpc_GameObjectRef.fl_tmpHealthbar = hpc_GameObjectRef.fl_tmpHealthbar - (fl_Damage / (fl_maxhealth * 2));
	}

	public void HealHealthBar (float fl_heal) // Modifying UI health bar according to heal amount
	{
		hpc_GameObjectRef.fl_tmpHealthbar = hpc_GameObjectRef.fl_tmpHealthbar + (fl_heal / (fl_maxhealth * 2));
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
		fl_health = fl_health - (fl_Damage);
		DamageHealthBar (fl_Damage);
		poisonTime--;
		fl_Damage -= 20;
		StartCoroutine (Poison (fl_Damage, poisonTime));
	}

	
}