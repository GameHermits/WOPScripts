using UnityEngine;
using System.Collections;

public class CamAnimation_Camera : MonoBehaviour {

	public CharacterController cc_PlayerController;
	public Animation an_CameraAnimation; //Empty GameObject's animation component
	private bool isMoving;
	private bool isMovingLeft;
	private bool isMovingRight;
	
	void CameraAnimations(){
		if(cc_PlayerController.isGrounded == true){
			if(isMoving == true){
				if(isMovingLeft == true){
					if(!an_CameraAnimation.isPlaying){//Waits until no animation is playing to play the next
						an_CameraAnimation.Play("walkLeft");
						isMovingLeft = false;
						isMovingRight = true;
					}
				}
				if(isMovingRight == true){
					if(!an_CameraAnimation.isPlaying){
						an_CameraAnimation.Play("walkRight");
						isMovingRight = false;
						isMovingLeft = true;
					}
				}
			}                      
		}
	}
	
	
	void Start () { //First step in a new scene/life/etc. will be "walkLeft"
		isMovingLeft = true;
		isMovingRight = false;
	}
	
	
	void Update () {
      
            float inputX = Input.GetAxis("Horizontal"); //Keyboard input to determine if player is moving
		float inputY = Input.GetAxis("Vertical");
		
		if(inputX  != 0 || inputY != 0){
			isMoving = true;       
		}
		else if(inputX == 0 && inputY == 0){
			isMoving = false;      
		}
		
		CameraAnimations();
    }
}
