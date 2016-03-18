using UnityEngine;
using System.Collections;

public class TimedObjectDestructor : MonoBehaviour {

	public float fl_TimeOut = 1.0f; //number of seconds to invoke the function after
	public bool is_DetachChildren = false;//

	
	void Awake () {// invoke the DestroyNow funtion to run after timeOut seconds // Used for initialization
		Invoke ("DestroyNow", fl_TimeOut);
	}
	

	void DestroyNow ()//used to destroy the
	{
		if (!GameManager.GM.Paused) {//if the pause key is not pressed
			if (is_DetachChildren) { // detach the children before destroying if specified
				transform.DetachChildren ();
			}
            
			Destroy (gameObject);// destory the game Object
        }
	}
}
