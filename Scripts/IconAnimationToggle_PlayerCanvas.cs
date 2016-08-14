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
	public Image img_BloodyScreen;
	public Image img_FreezeScreen;
	//private:
	private int elementIndex = 0;

	void Start ()
	{
		img_Lightning.enabled = true;
		img_Fire.enabled = false;
		img_Ice.enabled = false;
		img_BlackMagic.enabled = false;
	}

	void IconToggel ()
	{

		if (elementIndex == 0) {
			img_Lightning.enabled = true;
			img_Fire.enabled = false;
			img_Ice.enabled = false;
			img_BlackMagic.enabled = false;
		} else if (elementIndex == 1 && GameManager.GM.Player.FireMagic == true) {
			img_Lightning.enabled = false;
			img_Fire.enabled = true;
			img_Ice.enabled = false;
			img_BlackMagic.enabled = false;
		} else if (elementIndex == 2 && GameManager.GM.Player.IceMagic == true) {
			img_Lightning.enabled = false;
			img_Fire.enabled = false;
			img_Ice.enabled = true;
			img_BlackMagic.enabled = false;
		} else if (elementIndex == 3 && GameManager.GM.Player.BlackMagic == true) {
			img_Lightning.enabled = false;
			img_Fire.enabled = false;
			img_Ice.enabled = false;
			img_BlackMagic.enabled = true;
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonUp (1)) {
			if (elementIndex == 3) {
				elementIndex = 0;
				IconToggel ();
			} else {
				elementIndex++;
			}
			IconToggel ();
		}
	}
}
