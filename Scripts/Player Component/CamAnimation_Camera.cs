/*Class : Cameraanimation
 * Uses : Attach to main camera.
 * Requires : Camera Animation component attached.
 * Provides : toggle between animations in the camera animations.
 * Definition : This class is customized to toggle between Animation "Left" and animation "right" that simulate the head bob move for FPS chracters.
*/
using UnityEngine;
using System.Collections;

public class CamAnimation_Camera : MonoBehaviour
{

	public CharacterController cc_PlayerController;
	//Empty GameObject's animation component
	public Animation an_CameraAnimation;
	[HideInInspector]
	//for checking if the player is hit
	public bool isHit;
	//for checking if the player is moving.
	private bool isMoving;
	//To play left animation clip
	private bool isMovingLeft;
	//To play right animation clip
	private bool isMovingRight;
	//To play onHit animation clip
	private bool onHit;

	void CameraAnimations ()
	{
		if (cc_PlayerController.isGrounded == true) {
			if (isMoving == true) {
				if (isMovingLeft == true) {
					if (!an_CameraAnimation.isPlaying) {//Waits until no animation is playing to play the next
						an_CameraAnimation.Play ("walkLeft");
						isMovingLeft = false;
						isMovingRight = true;
					}
				}
				if (isMovingRight == true) {
					if (!an_CameraAnimation.isPlaying) {
						an_CameraAnimation.Play ("walkRight");
						isMovingRight = false;
						isMovingLeft = true;
					}
				}
			}                      
		}
		if (isHit == true) {
			an_CameraAnimation.Play ("OnHit");
			isHit = false;
		}
	}

	
	void Start ()
	{ //First step in a new scene/life/etc. will be "walkLeft"
		isMovingLeft = true;
		isMovingRight = false;
		onHit = false;
	}

	
	void Update ()
	{
      
		float inputX = Input.GetAxis ("Horizontal"); //Keyboard input to determine if player is moving
		float inputY = Input.GetAxis ("Vertical");
		
		if (inputX != 0 || inputY != 0) {
			isMoving = true;       
		} else if (inputX == 0 && inputY == 0) {
			isMoving = false;      
		}
		
		CameraAnimations ();
	}
}
