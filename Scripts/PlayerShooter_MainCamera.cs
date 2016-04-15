using UnityEngine;
using System.Collections;


public class PlayerShooter_MainCamera : MonoBehaviour
{

	// the place where projectiles are instantiated
	public GameObject go_ShootingPLace;
	//Bullets Model prefab
	public GameObject go_Lightningbullet;
	public GameObject go_Firebullet;
	public GameObject go_Icebullet;
	public GameObject go_BlackMagicbullet;
	//The amount of mana every element use.
	public float fl_UsedMana_Lightning;
	public float fl_UsedMana_Fire;
	public float fl_UsedMana_Ice;
	public float fl_UsedMana_BlackMagic;
	//Movement power
	public float fl_MovementForce = 10.0f;
	// Player UI controller
	public HPController_General hpc_GameObjectRef;
	public Animator handAnimator;

	//the four elements.
	private enum enum_Elements
	{
		Fire,
		Ice,
		Lightning,
		BlackMagic}
	;

	//Default element
	private enum_Elements currentElement = enum_Elements.Lightning;

	//Launch Bullet
	public void LaunchBullet (GameObject go_BulletType, float fl_UsedManaType)
	{
		if (GameManager.GM.isDead != true && GameManager.GM.ispaused != true) {

			GameObject go_NewBullet = Instantiate (go_BulletType, //...
				                          go_ShootingPLace.transform.position + go_ShootingPLace.transform.forward, transform.rotation) as GameObject;
		
			if (!go_NewBullet.GetComponent<Rigidbody> ()) {
				go_NewBullet.AddComponent<Rigidbody> ();
			}
			go_NewBullet.GetComponent<Rigidbody> ().AddForce (gameObject.transform.forward * fl_MovementForce, ForceMode.VelocityChange);

			Mana.mana -= fl_UsedManaType;
			hpc_GameObjectRef.fl_tmpManabar -= fl_UsedManaType / Mana.maxMana;
			handAnimator.SetBool ("isAttackingS", false); // setting the animation bool to false to exit the attack animation.
		}

	}
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.GM.isDead != true && GameManager.GM.ispaused != true) {

			// these conditions to check the input (1,2,3,4) and change element type
			if (Input.GetKey (KeyCode.Alpha1)) {
				currentElement = enum_Elements.Lightning;
			} else if (Input.GetKey (KeyCode.Alpha2)) {
				currentElement = enum_Elements.Fire;
			} else if (Input.GetKey (KeyCode.Alpha3)) {
				currentElement = enum_Elements.Ice;
			} else if (Input.GetKey (KeyCode.Alpha4)) {
				currentElement = enum_Elements.BlackMagic;
			}
			//this condition checks on the element type and fire the right prefab attached.
			switch (currentElement) {
			//when the player choose '1' or 'lightning'
			case enum_Elements.Lightning:
				if (Input.GetKeyUp (KeyCode.Mouse0)) {
					//If there is enough mana
					if (Mana.mana >= fl_UsedMana_Lightning) {
						if (go_Lightningbullet) {
							// setting the animation bool to true to enter the animation attack. the animation contains an event that calls lunch bullet in a certain frame.
							handAnimator.SetBool ("isAttackingS", true); 
						}
					}
				}
				break;

			case enum_Elements.Fire:

				if (GameManager.GM.isDead != true && GameManager.GM.ispaused != true) {
			
					if (Input.GetKeyUp (KeyCode.Mouse0)) {
						//If there is enough mana
						if (Mana.mana >= fl_UsedMana_Fire) {
							if (go_Firebullet) {
								LaunchBullet (go_Firebullet, fl_UsedMana_Fire);
							}
						}
					}
				}
				break;

			case enum_Elements.Ice:
				if (Input.GetKeyUp (KeyCode.Mouse0)) {
					//If there is enough mana
					if (Mana.mana >= fl_UsedMana_Ice) {
						if (go_Icebullet) {
							LaunchBullet (go_Icebullet, fl_UsedMana_Ice);
						}
					}
				}
				break;

			case enum_Elements.BlackMagic:
				if (Input.GetKeyUp (KeyCode.Mouse0)) {
					//If there is enough mana
					if (Mana.mana >= fl_UsedMana_BlackMagic) {
						if (go_BlackMagicbullet) {
							LaunchBullet (go_BlackMagicbullet, fl_UsedMana_BlackMagic);
						}
					}
				}
				break;
			}

		}
	}
}
	
