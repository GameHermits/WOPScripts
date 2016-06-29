/*Class: HPController
 * Requires: none
 * Provides: control of UI health bars and mana bars if found.
 * Definition: this class is a part of health component of any character, it provides control to the UI that each character has. it's called by Health class.
*/
using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;

// very important to impelement the UI image as variable

public class HPController_General : MonoBehaviour
{


	public Image im_Healthbar;
	// make anew variable of shown image
	public Image im_Manabar;
	// make anew variable of shown image
	public Image im_Energybar;

	public float fl_tmpHealthbar = 1f;
	//this is the total amount of the Health bar and it can be between 1and 0 (fill and empty)
	public float fl_tmpManabar = 1f;
	//this is the total amount of the Mana bar and it can be between 1and 0 (fill and empty)
	public float fl_tmpEnergybar = 1f;
	// Update is called once per frame
	void Update ()
	{

		if (fl_tmpHealthbar > 1) { //make the amount of healthbar image go no more than it's highest value (1)
			fl_tmpHealthbar = 1f;
		} else if (fl_tmpHealthbar < 0) { //make the amount of healthbar image go no more dowm than it's least value (0)
			fl_tmpHealthbar = 0f;
		}
		

		if (fl_tmpManabar > 1) { //make the amount of manabar image go no more than it's highest value (1)
			fl_tmpManabar = 1f;
		} else if (fl_tmpManabar < 0) { //make the amount of manabar image go no more dowm than it's least value (0)
			fl_tmpManabar = 0f;
		}

		if (fl_tmpEnergybar > 1) {
			fl_tmpEnergybar = 1f;
		} else if (fl_tmpEnergybar < 0) {
			fl_tmpEnergybar = 0f;
		}

		im_Healthbar.fillAmount = fl_tmpHealthbar; //give the variable value to the real amount of healthbar image  after change 

		if (gameObject.tag == "Player") {
			im_Manabar.fillAmount = fl_tmpManabar; //give the variable value to the real amount of manabar image  after change 
			im_Energybar.fillAmount = fl_tmpEnergybar;
		}
	}
}
