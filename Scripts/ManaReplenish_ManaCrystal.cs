using UnityEngine;
using System.Collections;

public class ManaReplenish_ManaCrystal : MonoBehaviour
{

	public float fl_manaAmount;
	private HPController_General HPC_GameObjectRef;

	void Start ()
	{
		HPC_GameObjectRef = GameObject.FindWithTag ("Player").GetComponent <HPController_General> ();
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Player") {
            
			if (Mana.mana <= Mana.maxMana) {
				Mana.mana += fl_manaAmount;
				HPC_GameObjectRef.fl_tmpManabar += fl_manaAmount / Mana.maxMana;
			} else if (Mana.mana > Mana.maxMana) {
				Mana.mana = Mana.maxMana;

			}
			GameObject.Destroy (gameObject);
            
		}
	}
}
