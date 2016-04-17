using UnityEngine;
using System.Collections;

public class HandsController : MonoBehaviour
{

	//Private:

	// Refrence for PlayerShooter class in the main camera.
	private PlayerShooter_MainCamera PS_PlayerShooterRef;

	// Use this for initialization
	void Start ()
	{
		PS_PlayerShooterRef = GameObject.FindWithTag ("MainCamera").GetComponent <PlayerShooter_MainCamera> ();
	
	}

	// Calling LaunchBullet() function at a certain frame in the animation.
	public void EnterSingleAttack ()
	{
		PS_PlayerShooterRef.LaunchBullet (PS_PlayerShooterRef.go_Lightningbullet, PS_PlayerShooterRef.fl_UsedMana_Lightning);
	}

	//Exit "HandsOnHit" animation.
	public void OnHitExit ()
	{
		gameObject.GetComponent<Animator> ().SetBool ("OnHit", false);
	}
}
