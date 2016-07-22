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
	// Update is called once per frame
	void Start ()
	{
		if (gameObject.tag == "Player") {
			im_Healthbar = GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent <IconAnimationToggle_PlayerCanvas> ().img_Health;
			im_Manabar = GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent <IconAnimationToggle_PlayerCanvas> ().img_Mana;
			im_Energybar = GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent <IconAnimationToggle_PlayerCanvas> ().img_Energy;
		}
	}

	void Update ()
	{
		if (gameObject.tag == "Player") {

			if (GameManager.GM.Player.healthAmount > 1) { //make the amount of healthbar image go no more than it's highest value (1)
				GameManager.GM.Player.healthAmount = 1f;
			} else if (GameManager.GM.Player.healthAmount < 0) { //make the amount of healthbar image go no more dowm than it's least value (0)
				GameManager.GM.Player.healthAmount = 0f;
			}

			if (GameManager.GM.Player.manaAmount > 1) { //make the amount of manabar image go no more than it's highest value (1)
				GameManager.GM.Player.manaAmount = 1f;
			} else if (GameManager.GM.Player.manaAmount < 0) { //make the amount of manabar image go no more dowm than it's least value (0)
				GameManager.GM.Player.manaAmount = 0f;
			}

			if (GameManager.GM.Player.energyAmount > 1) {
				GameManager.GM.Player.energyAmount = 1f;
			} else if (GameManager.GM.Player.energyAmount < 0) {
				GameManager.GM.Player.energyAmount = 0f;
			}

			im_Healthbar.fillAmount = GameManager.GM.Player.healthAmount;
			im_Manabar.fillAmount = GameManager.GM.Player.manaAmount; //give the variable value to the real amount of manabar image  after change 
			im_Energybar.fillAmount = GameManager.GM.Player.energyAmount;
		} else {

			if (fl_tmpHealthbar > 1) { //make the amount of healthbar image go no more than it's highest value (1)
				fl_tmpHealthbar = 1f;
			} else if (fl_tmpHealthbar < 0) { //make the amount of healthbar image go no more dowm than it's least value (0)
				fl_tmpHealthbar = 0f;
			}
			im_Healthbar.fillAmount = fl_tmpHealthbar; //give the variable value to the real amount of healthbar image  after change
		}
 
	}
}
