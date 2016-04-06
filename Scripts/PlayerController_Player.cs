using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerController_Player : MonoBehaviour {
	//public variables for player behavior
	public float fl_MoveSpeed = 0.5f;
	public float fl_Gravity = 9.81f;
    public bool isCanJump = true;
    public float fl_SprintAmount;
    public float fl_MaxJump = 10.0f;
    private GameObject go_PlayerCanvas; // player canvas that include health and mana bar
    // jump controling variables
    bool JumpLimit = false;
    float temp = 0;
	//character controller object
	private CharacterController cc_PlayerController;
    private MotionBlur mainCameraEffect;
	// Use this for initialization

	void Start () {
		//Get the character controller component to the object
		cc_PlayerController= gameObject.GetComponent<CharacterController> ();
        go_PlayerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas");
        mainCameraEffect = GameObject.FindWithTag("MainCamera").GetComponent<MotionBlur>();
	}
	
	// Update is called once per frame
	void Update () {

		if (GameManager.GM.isDead !=true && GameManager.GM.ispaused !=true) {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                fl_MoveSpeed = fl_SprintAmount;
                mainCameraEffect.enabled = true;
            }
            else {
                fl_MoveSpeed = 8;
                mainCameraEffect.enabled = false;
            }

            if (Input.GetKey (KeyCode.LeftShift))
			fl_MoveSpeed = fl_SprintAmount;
		else
			fl_MoveSpeed = 8;


		// movement in Z direction
		Vector3 vec3_MovementZ = Input.GetAxis ("Vertical") * Vector3.forward * fl_MoveSpeed * Time.deltaTime;
		// movement in X direction
		Vector3 vec3_MovementX = Input.GetAxis ("Horizontal") * Vector3.right * fl_MoveSpeed * Time.deltaTime;
		// Movement variable
		Vector3 vec3_Movement = transform.TransformDirection (vec3_MovementX + vec3_MovementZ);


            if (JumpLimit)
            {
                vec3_Movement.y -= fl_Gravity * Time.deltaTime;
                if (cc_PlayerController.isGrounded)
                {
                    JumpLimit = false;
                }
            }
            else if (isCanJump)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    vec3_Movement.y = fl_MaxJump;
                    temp++;
                    if (temp > fl_MaxJump)
                    {
                        temp = 0;
                        Debug.Log("Enter");
                        JumpLimit = true;
                    }
                }
            }
            else
            {
                // Aplaying gravity if not standing on something
                vec3_Movement.y -= fl_Gravity * Time.deltaTime;
            }
            
		//actual movement
		cc_PlayerController.Move (vec3_Movement);
	    }
	}


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal.y<=0.7)
        {
            //Debug.Log("no ");
            isCanJump = false;
        }
        else
        {
            //Debug.Log("yes");
            isCanJump = true;
        }
    }

}
