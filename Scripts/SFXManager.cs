using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {


	public  GameObject Player;
	private float X2;
	private float Z2;
	private float fl_distance;
	private AudioSource Sound;

	// Use this for initialization
	void Start () {
		Sound = gameObject.GetComponent <AudioSource> ();
		Player= GameObject.FindGameObjectWithTag ("Player");
		Sound.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		X2 = Mathf.Pow (this.transform.position.x - Player.transform.position.x,2f);
		Z2 = Mathf.Pow (this.transform.position.z - Player.transform.position.z,2f);
		fl_distance = Mathf.Sqrt (X2 +Z2);

		if (fl_distance > Sound.maxDistance) {
			Sound.volume = 0;
		}
		else if (fl_distance <= Sound.minDistance) {
			
			Sound.volume = 1;
		} 
		else if (fl_distance <= Sound.maxDistance) {
			
			Sound.volume = 1 - (fl_distance / Sound.maxDistance);
		} 


	}
}
