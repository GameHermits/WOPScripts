using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerController_Player : MonoBehaviour
{
	//public variables for player behavior
	public float fl_MoveSpeed = 0.5f;
	public float fl_Gravity = 9.81f;
	public bool isCanJump = true;
	//flag for ability to sprint or not
	public bool isCanSprint = true;
	//flag for ability to fill energy bar or not
	public bool isCanFill = true;
	public float fl_SprintAmount;
	public float fl_MaxJump = 10.0f;
	private GameObject go_PlayerCanvas;
	// player canvas that include health and mana bar
	// jump controling variables
	bool JumpLimit = false;
	float temp = 0;
	//character controller object
	private CharacterController cc_PlayerController;
	private MotionBlur mainCameraEffect;
	// Use this for initialization

	private HPController_General hpc_GameObjectRef;

	void Start ()
	{
		//Get the character controller component to the object
		cc_PlayerController = gameObject.GetComponent<CharacterController> ();
		go_PlayerCanvas = GameObject.FindGameObjectWithTag ("PlayerCanvas");
		mainCameraEffect = GameObject.FindWithTag ("MainCamera").GetComponent<MotionBlur> ();
		hpc_GameObjectRef = gameObject.GetComponent<HPController_General> ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (GameManager.GM.isDead != true && GameManager.GM.ispaused != true) {

			if (Input.GetKey (KeyCode.LeftShift) && isCanSprint) {
				fl_MoveSpeed = fl_SprintAmount;
				mainCameraEffect.enabled = true;
				//if energy bar is more than zero 
				if (hpc_GameObjectRef.fl_tmpEnergybar > 0) {
					hpc_GameObjectRef.fl_tmpEnergybar = hpc_GameObjectRef.fl_tmpEnergybar - (0.01f / 2.0f);
					isCanFill = false;
				}//if the energy bar is lower or equal zero he will not be able to sprint
				else if (hpc_GameObjectRef.fl_tmpEnergybar <= 0) {
					isCanSprint = false;
				}
			} else {
				fl_MoveSpeed = 8;
				mainCameraEffect.enabled = false;
				//if the energy bar is higher than 0.3 after reaching zero he could use it again
				if (hpc_GameObjectRef.fl_tmpEnergybar > 0.3f) {
					isCanSprint = true;
				}
			}
			if (isCanFill) {
				//fill the energy bar when isCanFill equal true
				hpc_GameObjectRef.fl_tmpEnergybar = hpc_GameObjectRef.fl_tmpEnergybar + (0.01f / 2.0f);
			}
			//only canFill when left the shift key up dude XD
			if (Input.GetKeyUp (KeyCode.LeftShift)) {
				isCanFill = true;
			}
			// movement in Z direction
			Vector3 vec3_MovementZ = Input.GetAxis ("Vertical") * Vector3.forward * fl_MoveSpeed * Time.deltaTime;
			// movement in X direction
			Vector3 vec3_MovementX = Input.GetAxis ("Horizontal") * Vector3.right * fl_MoveSpeed * Time.deltaTime;
			// Movement variable
			Vector3 vec3_Movement = transform.TransformDirection (vec3_MovementX + vec3_MovementZ);


			if (JumpLimit) {
				vec3_Movement.y -= fl_Gravity * Time.deltaTime;
				if (cc_PlayerController.isGrounded) {
					JumpLimit = false;
				}
			} else if (isCanJump) {
				if (Input.GetKey (KeyCode.Space)) {
					vec3_Movement.y = fl_MaxJump;
					temp++;
					if (temp > fl_MaxJump) {
						temp = 0;
						Debug.Log ("Enter");
						JumpLimit = true;
					}
				}
			} else {
				// Aplaying gravity if not standing on something
				vec3_Movement.y -= fl_Gravity * Time.deltaTime;
			}
            
			//actual movement
			cc_PlayerController.Move (vec3_Movement);
		}
	}


	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if (hit.normal.y <= 0.7) {
			//Debug.Log("no ");
			isCanJump = false;
		} else {
			//Debug.Log("yes");
			isCanJump = true;
		}
	}

}
