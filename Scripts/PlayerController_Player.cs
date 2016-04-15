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
	// for controling jumping behavior.
	public bool isCanJump = true;
	// Max Sprint speed the player can run with.
	public float fl_SprintAmount;
	// Max jump the player can do.
	public float fl_MaxJump = 10.0f;
	// Refrecne for the animator component in the hands.
	public Animator handAnimator;

	//Private

	// Reference of the player canvas.
	private GameObject go_PlayerCanvas;
	// player canvas that include health and mana bar
	// jump controling variables
	bool JumpLimit = false;
	float temp = 0;
	//character controller object
	private CharacterController cc_PlayerController;
	// Refrence of an Image effect component in the main camera, for sprint.
	private MotionBlur mainCameraEffect;

	// Use this for initialization

	void Start ()
	{
		//Get the character controller component to the object
		cc_PlayerController = gameObject.GetComponent<CharacterController> ();
		go_PlayerCanvas = GameObject.FindGameObjectWithTag ("PlayerCanvas");
		mainCameraEffect = GameObject.FindWithTag ("MainCamera").GetComponent<MotionBlur> ();
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
				// Jumping behavior
				vec3_Movement.y = fl_MaxJump;
				temp++;
				if (temp > fl_MaxJump) { // if the player reached the maxjump value, diable jumping and Exit jumping animation.
					temp = 0;
					JumpLimit = true;
					handAnimator.SetBool ("isJumping", false);
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
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) { 
			fl_MoveSpeed = fl_SprintAmount;
			mainCameraEffect.enabled = true;
			handAnimator.SetBool ("isRunning", true);
		} else {
			fl_MoveSpeed = 8;
			mainCameraEffect.enabled = false;
			handAnimator.SetBool ("isRunning", false);
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

}
