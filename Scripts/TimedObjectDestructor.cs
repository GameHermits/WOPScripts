/*Class: TimeObjectDestrucor
 * Requires: none
 * Provides: destroy game object that have the script after a certain amount of time set by the user.
 * Definition: This is used for bullets, and time based objects.
*/
using UnityEngine;
using System.Collections;

public class TimedObjectDestructor : MonoBehaviour
{

	public float fl_TimeOut = 1.0f;
	//number of seconds to invoke the function after
	public bool is_DetachChildren = false;
//

	
	void Awake ()
	{// invoke the DestroyNow funtion to run after timeOut seconds // Used for initialization
		Invoke ("DestroyNow", fl_TimeOut);
	}


	void DestroyNow ()//used to destroy the
	{
		if (is_DetachChildren) { // detach the children before destroying if specified
			transform.DetachChildren ();
		}
            
		Destroy (gameObject);// destory the game Object
	}
}
