using UnityEngine;
using System.Collections;

public class OnTriggerSound : MonoBehaviour
{
	private AudioSource SFX;
	public GameObject target;
	// Use this for initialization
	void Start ()
	{
		SFX = gameObject.GetComponent <AudioSource> ();

		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player");
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject == target) {
			SFX.Play ();
		}
	}
}
