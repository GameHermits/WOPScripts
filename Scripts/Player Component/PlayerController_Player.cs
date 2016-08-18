/* Class : PlayerController
 * Uses : any object with character controller component.
 * Requires : character controller, canvas, hpcontoller, main camera blur effect, playerCnavas, player sound, character hand animator
 * Provides : Character movement, sprint, jump, gravity and physics, camera effect control, hand animation for walking, running and jumping, mana modification.
 * Definition : This class can be applied on any character controller as long as it have the requires componenets and classes on the attached to the same object. It enables movement through (W, A, S, D)
 * or Arrow. Sprint with holding right/left shift and jump using space. It also detect edges that have hard ridges. The class also modify energy when use sprint and detect hits from enemies and trigger Onhit
 * animaiton for charactrer's hand model.
*/
using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerController_Player : MonoBehaviour
{
	//public
	// for applying gravity to the player.
	public float fl_Gravity = 9.81f;
	// Refrecne for the animator component in the hands.
	//public Animator handAnimator;
	//Audiosource for jump sound
	public AudioSource jump;
	//Private
	// for controling jumping behavior.
	private bool CanJump = true;
	//flag for ability to sprint or not
	private bool CanSprint = true;
	//flag for ability to fill energy bar or not
	private bool CanFill = false;
	//refrence for the player canvas
	private GameObject go_PlayerCanvas;
	// jump controling variables
	bool JumpLimit = false;
	float temp = 0;
	//character controller object
	private CharacterController cc_PlayerController;
	// Refrence of an Image effect component in the main camera, for sprint.
	private MotionBlur mainCameraEffect;
	// for controlling player UI
	private HPController_General hpc_GameObjectRef;
	//Object to play character sounds.
	private CharacterSound_General playerSounds;

	void Start ()
	{
		//If the game is loaded from previous playing, set player position to the last checkpoint position
		if (GameManager.GM.isLoadedGame == true) {
			gameObject.transform.position = new Vector3 (SceneManager.SM.activeXPosition, SceneManager.SM.activeYPosition, SceneManager.SM.activeZPosition);
			GameManager.GM.isLoadedGame = false;
		}
		//Set this as the GameManager Playergameobject refrence 
		GameManager.GM.PlayerGameObject = this.gameObject;
		//Get the character controller component to the object for applying movement
		cc_PlayerController = gameObject.GetComponent<CharacterController> ();
		//Get the player canvas refrence for applying UI cahnges
		go_PlayerCanvas = GameObject.FindGameObjectWithTag ("PlayerCanvas");
		//Get Main Camera Image effect (Motion blur) for activating when sprint
		mainCameraEffect = GameObject.FindWithTag ("MainCamera").GetComponent<MotionBlur> ();
		//Get HPController component for UI changes
		hpc_GameObjectRef = gameObject.GetComponent <HPController_General> ();
		//Get Character Sound component for dialog and other player sound.
		playerSounds = gameObject.GetComponent <CharacterSound_General > ();
		GameManager.GM.Player.movementSpeed = GameManager.GM.Player.BootsSpeed;
	}

	void Jump (ref Vector3 vec3_Movement) // jump behavior and animations
	{
		if (JumpLimit) { // if reached maximum jump apply gravity
			vec3_Movement.y -= fl_Gravity * Time.deltaTime;
			if (cc_PlayerController.isGrounded) { // if the player touched the ground, enable jumping again.
				JumpLimit = false;
			}
		} else if (CanJump) {// is the player is on a suitable ground to jump
			if (Input.GetKeyDown (KeyCode.Space)) {
				jump.Play ();
			}
			if (Input.GetKey (KeyCode.Space)) {
				// Jumping behavior
				vec3_Movement.y = GameManager.GM.Player.maxJump;
				temp++;
				if (temp > GameManager.GM.Player.maxJump) { // if the player reached the maxjump value, diable jumping and Exit jumping animation.
					temp = 0;
					JumpLimit = true;
					CanJump = false;
				}
			}
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			// Aplaying gravity if not standing on something
			vec3_Movement.y -= fl_Gravity * Time.deltaTime;
			CanJump = false;
		}

	}

	void Sprint () // Sprint behavior and animations
	{
		// if the player sprinting, apply sprint speed, activate sprint image effect, and Enter "HandsRunning" animation. Else, back to normal state.
		if ((Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) && CanSprint == true) { 
			GameManager.GM.Player.movementSpeed = GameManager.GM.Player.sprintAmout;
			mainCameraEffect.enabled = true;
			//handAnimator.SetBool ("isRunning", true);
			//if energy bar is more than zero 
			if (GameManager.GM.Player.energyAmount > 0) {
				GameManager.GM.Player.energyAmount -= (0.01f / 2.0f);
				CanFill = false;
			}//if the energy bar is lower or equal zero he will not be able to sprint
			else if (GameManager.GM.Player.energyAmount <= 0) {
				CanSprint = false;
			}
				
		} else {
			CanFill = true;
			GameManager.GM.Player.movementSpeed = GameManager.GM.Player.BootsSpeed;
			mainCameraEffect.enabled = false;
			//handAnimator.SetBool ("isRunning", false);

			if (CanFill) {
				//fill the energy bar when isCanFill equal true
				GameManager.GM.Player.energyAmount += (0.01f / 2.0f);
			}
			//if the energy bar is higher than 0.3 after reaching zero he could use it again
			if (GameManager.GM.Player.energyAmount > 0.3f) {
				CanSprint = true;
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{

		if (GameManager.GM.isDead != true && GameManager.GM.ispaused != true) {
			
			// movement in Z direction
			Vector3 vec3_MovementZ = Input.GetAxis ("Vertical") * Vector3.forward * GameManager.GM.Player.movementSpeed * Time.deltaTime;
			// movement in X direction
			Vector3 vec3_MovementX = Input.GetAxis ("Horizontal") * Vector3.right * GameManager.GM.Player.movementSpeed * Time.deltaTime;
			// Movement variable
			Vector3 vec3_Movement = transform.TransformDirection (vec3_MovementX + vec3_MovementZ);

			/*if ((Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) || (vec3_MovementX == Vector3.right * 0 && vec3_MovementZ == Vector3.forward * 0)) {

				//handAnimator.SetBool ("isWalking", false);

			} else if ((Input.GetKey (KeyCode.LeftShift) != true || Input.GetKey (KeyCode.RightShift) != true)) {
				//handAnimator.SetBool ("isWalking", true);
			}*/
			vec3_Movement.y -= fl_Gravity / 2 * Time.deltaTime;//pull him down
			Jump (ref vec3_Movement);
			Sprint ();
			//actual movement
			cc_PlayerController.Move (vec3_Movement);
		}

	}

	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if (hit.normal.y <= 0.7) {
			//the player isn't on a suitable ground to jump
			CanJump = false;
		} else {
			// the player is on a suitable ground to jump
			CanJump = true;
		}
	}

	AudioSource AddAudioComponent (AudioClip ac, bool loop, bool playOnAwake, float volume)
	{
		AudioSource newSource = new AudioSource ();
		newSource.clip = ac;
		newSource.loop = loop;
		newSource.playOnAwake = playOnAwake;
		newSource.volume = volume;
		return newSource;
	}
}
