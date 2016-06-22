using UnityEngine;
using System.Collections;

public class Support : MonoBehaviour
{
	//Private:
	//Collection for holding supports that Nykean can have.
	private SupportData[] SC_NykeanSupp;
	private int in_SuppIndex = 0;

	//Clover Assets
	// Refrence for health script of the player.
	private Health_General he_Heal;
	// Refrence for hpcontroller script of the player
	private HPController_General hpc_GameObjectRef;
	//heal amount that clover can do
	private float in_HealAmount;

	//Adam Assets
	//The shield that adam summons
	public GameObject Shield;

	void Start ()
	{ //Initilaization
		//Player Componenets
		he_Heal = GameObject.FindGameObjectWithTag ("Player").GetComponent<Health_General> ();
		hpc_GameObjectRef = GameObject.FindGameObjectWithTag ("Player").GetComponent<HPController_General> ();
		//Support Collection Initilaization
		SC_NykeanSupp = new SupportData[4];
		SC_NykeanSupp [0] = GameManager.GM.Clover;
		SC_NykeanSupp [1] = GameManager.GM.Adam;
		SC_NykeanSupp [2] = GameManager.GM.Ethan;
		SC_NykeanSupp [3] = GameManager.GM.Lauren;
		//Clover Heal amount Initilaization
		in_HealAmount = 300 * GameManager.GM.Clover.in_Level;
	}

	void SupportJob ()
	{
		//If this support is unlocked
		if (SC_NykeanSupp [in_SuppIndex].isOpen == true) {
			//If Support can be used and didn't exceed the limited times
			if (SC_NykeanSupp [in_SuppIndex].CanUse () == true) {

				switch (in_SuppIndex) {
				//Clover
				case 0:
					he_Heal.fl_health += in_HealAmount;
					he_Heal.HealHealthBar (in_HealAmount);
					Mana.mana += in_HealAmount;
					hpc_GameObjectRef.fl_tmpManabar += (in_HealAmount / (Mana.maxMana * 2));
					GameManager.GM.Clover.Use ();
					break;
				//Adam
				case 1:
					break;
				//Ethan
				case 2:
					break;
				//Lauren
				case 3:
					break;
				
				}
			}
		}
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.Tab)) { // Player can navigate throw different supports by pressing tab. 
			if (in_SuppIndex == 3)
				in_SuppIndex = 0;
			else
				in_SuppIndex++;
		}
		if (Input.GetKey (KeyCode.R)) {
			SupportJob (); //Call the support upon clicking R
		}
	}
}
