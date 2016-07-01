using UnityEngine;
using System.Collections;

public class ManaReplenish_ManaCrystal : MonoBehaviour
{
	//public:
	// mana amount for each diamond
	public float fl_manaAmount;
	//Movement speed of each diamond
	public float fl_movementSpeed;
	//private:
	// Player hp controller refrecne
	private HPController_General HPC_GameObjectRef;
	//controlling diamond behavior
	private bool canChase = false;
	//Refrence for player transform
	private Vector3 vec_Player;

	void Start ()
	{
		HPC_GameObjectRef = GameObject.FindWithTag ("Player").GetComponent <HPController_General> ();
	}

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

		if (col.gameObject.tag == "Player") {// on collision with the player give more mana and modify mana power accordingly.

			if (GameManager.GM.Player.mana <= GameManager.GM.Player.maxMana) {
				GameManager.GM.Player.mana += fl_manaAmount;
				HPC_GameObjectRef.fl_tmpManabar += fl_manaAmount / GameManager.GM.Player.maxMana;
			} else if (GameManager.GM.Player.mana > GameManager.GM.Player.maxMana) {
				GameManager.GM.Player.mana = GameManager.GM.Player.maxMana;

			}
			GameObject.Destroy (gameObject);

		}

	}
}
