using UnityEngine;
using System.Collections;

public class ItemChaser_Item : MonoBehaviour {

	private Transform Player;

	public float speed; // the speed of item moving to player
	void Start(){
		Player = GameObject.FindWithTag ("Player").transform; // take the position of the player to the item
	}
	// Update is called once per frame
	void Update () {
		transform.LookAt (Player); // face the player
		transform.Translate (Vector3.forward * speed * Time.deltaTime); // move the item to player in exact speed
	}
}
