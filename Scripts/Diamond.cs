using UnityEngine;
using System.Collections;

public class Diamond : MonoBehaviour
{
	
	//public:
	// mana amount for each diamond
	public float healingAmount;
	//Movement speed of each diamond
	public float fl_movementSpeed;
	//Decide wether this diamond going to heal health or mana
	public enum Type
	{
		Health,
		Mana

	}

	public Type diamondType = Type.Health;
	//private:
	//controlling diamond behavior
	private bool canChase = false;
	//Refrence for player transform
	private Vector3 vec_Player;

	void Update ()
	{
		
		if (canChase == true) { // Traslate the diamond to the player
			vec_Player = GameObject.FindWithTag ("Player").transform.position;
			transform.position = Vector3.MoveTowards (transform.position, vec_Player, fl_movementSpeed * Time.deltaTime);
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Terrain") {// if the diamond hits a terrain, enable it's chasing behavior
			canChase = true;
		}

		if (col.gameObject.tag == "Player") {// on collision with the player give more health or mana and modify thier UI accordingly.
			switch (diamondType) {
			case Type.Mana:
				if (GameManager.GM.Player.mana <= GameManager.GM.Player.maxMana) {
					GameManager.GM.Player.mana += healingAmount;
					GameManager.GM.Player.manaAmount += healingAmount / GameManager.GM.Player.maxMana;
				} else if (GameManager.GM.Player.mana > GameManager.GM.Player.maxMana) {
					GameManager.GM.Player.mana = GameManager.GM.Player.maxMana;
				}
				break;
			case Type.Health:
				if (GameManager.GM.Player.health <= GameManager.GM.Player.maxHealth) {
					GameManager.GM.Player.health += healingAmount;
					GameManager.GM.Player.healthAmount += healingAmount / GameManager.GM.Player.maxHealth;
				} else if (GameManager.GM.Player.health > GameManager.GM.Player.maxHealth) {
					GameManager.GM.Player.health = GameManager.GM.Player.maxHealth;
				}
				break;
			}
			GameObject.Destroy (gameObject);

		}

	}
}
