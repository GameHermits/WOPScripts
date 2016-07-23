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
	// Max walk speed the player can walk with.
	public float fl_MoveSpeed = 0.5f;
	// for applying gravity to the player.
	public float fl_Gravity = 9.81f;
	// Max Sprint speed the player can run with.
	public float fl_SprintAmount;
	// Max jump the player can do.
	public float fl_MaxJump = 10.0f;
	// Refrecne for the animator component in the hands.
	public Animator handAnimator;
	//Audio clips needed for player movements
	public AudioClip sprint;

	//Private
	// for controling jumping behavior.
	private bool isCanJump = true;
	//flag for ability to sprint or not
	private bool isCanSprint = true;
	//flag for ability to fill energy bar or not
	private bool isCanFill = false;
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
	//Audio sources needed for player
	//private AudioSource Sprint;

	void Start ()
	{
		//Get the character controller component to the object
		cc_PlayerController = gameObject.GetComponent<CharacterController> ();
		go_PlayerCanvas = GameObject.FindGameObjectWithTag ("PlayerCanvas");
		mainCameraEffect = GameObject.FindWithTag ("MainCamera").GetComponent<MotionBlur> ();
		hpc_GameObjectRef = gameObject.GetComponent <HPController_General> ();
		playerSounds = gameObject.GetComponent <CharacterSound_General > ();
		//Set player data
		fl_SprintAmount = GameManager.GM.Player.sprintAmout;
		fl_MaxJump = GameManager.GM.Player.maxJump;
		fl_MoveSpeed = GameManager.GM.Player.movementSpeed;
	}

	void Jump (ref Vector3 vec3_Movement) // jump behavior and animations
	{
		if (JumpLimit) { // if reached maximum jump apply gravity
			vec3_Movement.y -= fl_Gravity * Time.deltaTime;
			if (cc_PlayerController.isGrounded) { // if the player touched the ground, enable jumping again.
				JumpLimit = false;

			}
		} else if (isCanJump) {// is the player is on a suitable ground to jump
			if (Input.GetKey (KeyCode.Space)) {
				// Enter "HandsJump" animation and Exit "HandsWalk" animation
				handAnimator.SetBool ("isJumping", true);
				handAnimator.SetBool ("isWalking", false);
				if (true) {
					
				
					// Jumping behavior
					vec3_Movement.y = fl_MaxJump;
					temp++;
					if (temp > fl_MaxJump) { // if the player reached the maxjump value, diable jumping and Exit jumping animation.
						temp = 0;
						JumpLimit = true;
						handAnimator.SetBool ("isJumping", false);
					}
				}
			}
		} else {
			// Aplaying gravity if not standing on something
			vec3_Movement.y -= fl_Gravity * Time.deltaTime;
		}

	}

	void Sprint () // Sprint behavior and animations
	{
		// if the player sprinting, apply sprint speed, activate sprint image effect, and Enter "HandsRunning" animation. Else, back to normal state.
		if ((Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) && isCanSprint == true) { 
			fl_MoveSpeed = fl_SprintAmount;
			mainCameraEffect.enabled = true;
			handAnimator.SetBool ("isRunning", true);
			//if energy bar is more than zero 
			if (GameManager.GM.Player.energyAmount > 0) {
				GameManager.GM.Player.energyAmount -= (0.01f / 2.0f);
				isCanFill = false;
			}//if the energy bar is lower or equal zero he will not be able to sprint
			else if (GameManager.GM.Player.energyAmount <= 0) {
				isCanSprint = false;
			}
				
		} else {
			isCanFill = true;
			fl_MoveSpeed = 8;
			mainCameraEffect.enabled = false;
			handAnimator.SetBool ("isRunning", false);

			if (isCanFill) {
				//fill the energy bar when isCanFill equal true
				GameManager.GM.Player.energyAmount += (0.01f / 2.0f);
			}
			//if the energy bar is higher than 0.3 after reaching zero he could use it again
			if (GameManager.GM.Player.energyAmount > 0.3f) {
				isCanSprint = true;
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{

		if (GameManager.GM.isDead != true && GameManager.GM.ispaused != true) {
			
			// movement in Z direction
			Vector3 vec3_MovementZ = Input.GetAxis ("Vertical") * Vector3.forward * fl_MoveSpeed * Time.deltaTime;
			// movement in X direction
			Vector3 vec3_MovementX = Input.GetAxis ("Horizontal") * Vector3.right * fl_MoveSpeed * Time.deltaTime;
			// Movement variable
			Vector3 vec3_Movement = transform.TransformDirection (vec3_MovementX + vec3_MovementZ);

			if ((Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) || (vec3_MovementX == Vector3.right * 0 && vec3_MovementZ == Vector3.forward * 0)) {

				handAnimator.SetBool ("isWalking", false);

			} else if ((Input.GetKey (KeyCode.LeftShift) != true || Input.GetKey (KeyCode.RightShift) != true)) {
				handAnimator.SetBool ("isWalking", true);
			}
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
			isCanJump = false;
		} else {
			// the player is on a suitable ground to jump
			isCanJump = true;
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Projectile") {// if the player gets hit, Enter "HandsOnHit" Animation.
			handAnimator.SetBool ("OnHit", true);// The animation calls it's Exit function when it finishs.
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
