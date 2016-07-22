using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;

public class IconAnimationToggle_PlayerCanvas : MonoBehaviour
{

	public Image img_Lightning;
	public Image img_Fire;
	public Image img_Ice;
	public Image img_BlackMagic;
	//Bar Images for the player.
	public Image img_Health;
	public Image img_Mana;
	public Image img_Energy;

	void Start ()
	{
		img_Lightning.enabled = true;
		img_Fire.enabled = false;
		img_Ice.enabled = false;
		img_BlackMagic.enabled = false;
	}
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			img_Lightning.enabled = true;
			img_Fire.enabled = false;
			img_Ice.enabled = false;
			img_BlackMagic.enabled = false;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2) && GameManager.GM.Player.FireMagic == true) {
			img_Lightning.enabled = false;
			img_Fire.enabled = true;
			img_Ice.enabled = false;
			img_BlackMagic.enabled = false;
		}
		if (Input.GetKeyDown (KeyCode.Alpha3) && GameManager.GM.Player.IceMagic == true) {
			img_Lightning.enabled = false;
			img_Fire.enabled = false;
			img_Ice.enabled = true;
			img_BlackMagic.enabled = false;
		}
		if (Input.GetKeyDown (KeyCode.Alpha4) && GameManager.GM.Player.BlackMagic == true) {
			img_Lightning.enabled = false;
			img_Fire.enabled = false;
			img_Ice.enabled = false;
			img_BlackMagic.enabled = true;
		}
	}
}
