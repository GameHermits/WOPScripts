using UnityEngine;
using System.Collections;

public class Support : MonoBehaviour
{
	//Private:
	private SupportCharacter[] SC_NykeanSupp = new SupportCharacter[4];
	private int in_SuppIndex = 0;
	//Clover Assets
	private Health_General he_Heal;
	private HPController_General hpc_GameObjectRef;
	private float in_HealAmount;
	//Adam Assets

	void Start ()
	{ //Initialaization
		//Player Componenets
		he_Heal = GameObject.FindGameObjectWithTag ("Player").GetComponent<Health_General> ();
		hpc_GameObjectRef = GameObject.FindGameObjectWithTag ("Player").GetComponent<HPController_General> ();
		//Clover Components
		Debug.Log (SC_NykeanSupp [0].fl_SuppLevel);
		//in_HealAmount = 300 * SC_NykeanSupp [0].fl_SuppLevel;
	}

	void SupportJob ()
	{
		//If Support can be used
		if (SC_NykeanSupp [in_SuppIndex].CanUse () == true) {

			switch (in_SuppIndex) {
			//Clover
			case 0:
				he_Heal.fl_health += in_HealAmount;
				he_Heal.HealHealthBar (in_HealAmount);
				Mana.mana += in_HealAmount;
				hpc_GameObjectRef.fl_tmpManabar += (in_HealAmount / (Mana.maxMana * 2));
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

	void Update ()
	{
		if (Input.GetKey (KeyCode.Tab)) {
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
