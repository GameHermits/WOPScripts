using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour
{


	private  GameObject Player;
	private float fl_distance;
	private AudioSource Sound;

	// Use this for initialization
	void Start ()
	{
		Sound = gameObject.GetComponent <AudioSource> ();
		Player = GameObject.FindGameObjectWithTag ("Player");
		Sound.Play ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		fl_distance = Vector3.Distance (gameObject.transform.position, Player.transform.position);

		if (fl_distance > Sound.maxDistance) {
			Sound.volume = 0;
		} else if (fl_distance <= Sound.minDistance) {
			Sound.volume = 1;
		} else if (fl_distance <= Sound.maxDistance) {
			Sound.volume = 1 - (fl_distance / Sound.maxDistance);
		} 


	}
}
