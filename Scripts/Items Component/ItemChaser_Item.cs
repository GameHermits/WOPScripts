/*Class: ItemChaser
 * Requires: none
 * Provides: moving items toward the player to collect them.
 * Definition: this calss is part of item component that is used to make the item move towards the player so the player won't need to go ot collect the item him/her self
*/
using UnityEngine;
using System.Collections;

public class ItemChaser_Item : MonoBehaviour
{

	private Transform Player;

	public float speed;
	// the speed of item moving to player
	void Start ()
	{
		Player = GameObject.FindWithTag ("Player").transform; // take the position of the player to the item
	}
	// Update is called once per frame
	void Update ()
	{
		transform.LookAt (Player); // face the player
		transform.Translate (Vector3.forward * speed * Time.deltaTime); // move the item to player in exact speed
	}
}
