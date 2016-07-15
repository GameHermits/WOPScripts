/* Class : PlayerShooter
 * Uses : FPS character shooter.
 * Requires : Attach to main camera
 * Provides : Different types of projectile, Mana modification, character hand animation.
 * Definition : This class is cutomized on Nykean the main character of the game, however, the function Lunchbullet can be easly generlized.
 * The class goal is to provide the ability to enter attacking hand animations that contain events that call lunchbullet to lunch attacks as particle systems.
 * That means it doesn't deal with objects that use physics.
*/
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
	//Object to play character sounds.
	private CharacterSound_General playersounds;
	//boolean to control UI funcion
	private bool lockedStyle = false;

	void Start ()
	{
		playersounds = gameObject.GetComponent <CharacterSound_General> ();
	}
	//Launch Bullet
	public void LaunchBullet (GameObject go_BulletType, float fl_UsedManaType)
	{
		if (GameManager.GM.isDead != true && GameManager.GM.ispaused != true) {

			GameObject go_NewBullet = Instantiate (go_BulletType, //...
				                          go_ShootingPLace.transform.position + go_ShootingPLace.transform.forward, transform.rotation) as GameObject;
		
			/*if (!go_NewBullet.GetComponent<Rigidbody> ()) {
				go_NewBullet.AddComponent<Rigidbody> ();
			}
			go_NewBullet.GetComponent<Rigidbody> ().AddForce (gameObject.transform.forward * fl_MovementForce, ForceMode.VelocityChange);*/	
			go_NewBullet.GetComponent <Damage_Projectile> ().fl_dmgAmount = 100 * GameManager.GM.Player.ThunderWisdom;
			GameManager.GM.Player.mana -= fl_UsedManaType;
			hpc_GameObjectRef.fl_tmpManabar -= fl_UsedManaType / GameManager.GM.Player.maxMana;
			handAnimator.SetBool ("isAttackingS", false); // setting the animation bool to false to exit the attack animation.
			//playersounds.Attack ();
		}

	}
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.GM.isDead != true && GameManager.GM.ispaused != true) {

			// these conditions to check the input (1,2,3,4) and change element type
			if (Input.GetKey (KeyCode.Alpha1) && GameManager.GM.Player.ThunderMagic == true) {
				currentElement = enum_Elements.Lightning;
			} else if (Input.GetKey (KeyCode.Alpha2) && GameManager.GM.Player.FireMagic == true) {
				currentElement = enum_Elements.Fire;
			} else if (Input.GetKey (KeyCode.Alpha3) && GameManager.GM.Player.IceMagic == true) {
				currentElement = enum_Elements.Ice;
			} else if (Input.GetKey (KeyCode.Alpha4) && GameManager.GM.Player.BlackMagic == true) {
				currentElement = enum_Elements.BlackMagic;
			} else {
				lockedStyle = true;
			}

			//this condition checks on the element type and fire the right prefab attached.
			switch (currentElement) {
			//when the player choose '1' or 'lightning'
			case enum_Elements.Lightning:
				if (Input.GetKeyUp (KeyCode.Mouse0)) {
					//If there is enough mana
					if (GameManager.GM.Player.mana >= fl_UsedMana_Lightning) {
						if (go_Lightningbullet) {
							// setting the animation bool to true to enter the animation attack. the animation contains an event that calls lunch bullet in a certain frame.
							handAnimator.SetBool ("isAttackingS", true); 
						}
					}
				}
				break;

			case enum_Elements.Fire:
				if (Input.GetKeyUp (KeyCode.Mouse0)) {
					//If there is enough mana
					if (GameManager.GM.Player.mana >= fl_UsedMana_Fire) {
						if (go_Firebullet) {
							LaunchBullet (go_Firebullet, fl_UsedMana_Fire);
						}
					}
				}
				break;

			case enum_Elements.Ice:
				if (Input.GetKeyUp (KeyCode.Mouse0)) {
					//If there is enough mana
					if (GameManager.GM.Player.mana >= fl_UsedMana_Ice) {
						if (go_Icebullet) {
							LaunchBullet (go_Icebullet, fl_UsedMana_Ice);
						}
					}
				}
				break;

			case enum_Elements.BlackMagic:
				if (Input.GetKeyUp (KeyCode.Mouse0)) {
					//If there is enough mana
					if (GameManager.GM.Player.mana >= fl_UsedMana_BlackMagic) {
						if (go_BlackMagicbullet) {
							LaunchBullet (go_BlackMagicbullet, fl_UsedMana_BlackMagic);
						}
					}
				}
				break;
			}

		}
  
	
	}

	/*IEnumerator LockedStyletrigger ()
	{
		yield return new WaitForSeconds (4);
		lockedStyle = false;
	}

	void onGUI ()
	{
		if (lockedStyle == true) {
			GUI.TextArea (Rect (100, 200, 100, 200), "You did not learn this style of magic yet");
			LockedStyletrigger ();
		}

	}*/
}
	
