using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;

public class IconAnimationToggle_PlayerCanvas : MonoBehaviour {

	public Image img_Lightning;
	public Image img_Fire;
	public Image img_Ice;
	public Image img_BlackMagic;

	void Start(){
		img_Lightning.enabled = true;
		img_Fire.enabled = false;
		img_Ice.enabled = false;
		img_BlackMagic.enabled = false;
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			img_Lightning.enabled = true;
			img_Fire.enabled = false;
			img_Ice.enabled = false;
			img_BlackMagic.enabled = false;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			img_Lightning.enabled = false;
			img_Fire.enabled = true;
			img_Ice.enabled = false;
			img_BlackMagic.enabled = false;
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			img_Lightning.enabled = false;
			img_Fire.enabled = false;
			img_Ice.enabled = true;
			img_BlackMagic.enabled = false;
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			img_Lightning.enabled = false;
			img_Fire.enabled = false;
			img_Ice.enabled = false;
			img_BlackMagic.enabled = true;
		}
	}
}
