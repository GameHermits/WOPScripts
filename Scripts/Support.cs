using UnityEngine;
using System.Collections;

public class Support : MonoBehaviour
{
	//Private:
	//Collection for holding supports that Nykean can have.
	private SupportData[] SC_NykeanSupp = { Clover, Adam, Ethan, Lauren };
	private int in_SuppIndex = 0;

	//Clover Assets
	// Refrence for health script of the player.
	private Health_General he_Heal;
	// Refrence for hpcontroller script of the player
	private HPController_General hpc_GameObjectRef;
	//heal amount that clover can do
	private float in_HealAmount;
	// clover object
	private SupportData Clover;

	//Adam Assets
	public GameObject Shield;
	private SupportData Adam;

	//Ethan
	private SupportData Ethan;

	//Lauren
	private SupportData Lauren;

	void Start ()
	{ //Initialaization
		//Player Componenets
		he_Heal = GameObject.FindGameObjectWithTag ("Player").GetComponent<Health_General> ();
		hpc_GameObjectRef = GameObject.FindGameObjectWithTag ("Player").GetComponent<HPController_General> ();

		//Clover Components
		Clover = new SupportData (1, true, true);
		in_HealAmount = 300 * Clover.in_Level;
		//Adam Components
		Adam = new SupportData (1, false, false);
		//Ethan Components
		Ethan = new SupportData (1, false, false);
		//Lauren Components
		Lauren = new SupportData (1, false, false);
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
					Clover.Use ();
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

class SupportData //Data container Object.
{
	// support level, can be adjust in training place
	public int in_Level;
	// did the player unlocked this support or not
	public bool isOpen;
	// limit of use for one level, can be adjust in shop
	private int in_UseTimes = 3;
	// did the support exceed the limit of uses in one level or not
	private bool canUse;

	//Constructor for Initilaization
	SupportData (int level, bool canUse, bool isOpen)
	{
		this.in_Level = level;
		this.isOpen = isOpen;
		this.canUse = canUse;
	}

	public bool CanUse ()
	{ // Check if support eceeded the limit of uses for one level and change canUse boolian to false
		if (in_UseTimes == 0)
			canUse = false;
		return canUse;
	}

	public void Use ()
	{ // decrease in_UseTimes by one whenever it's called, usually called when the support is used in SupportJob function
		in_UseTimes--;
	}
}
