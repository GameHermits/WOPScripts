/*Class: ItemPopOut
 * Requires: none
 * Provides: spawning items specified in the Items array by the user, also, make a popout explosion looking of items from dead enemy characters and different objects liek chests.
 * Definition: this class is always attached to the game object holding the array of items to be poped out. the class uses physics to emmit an explosion force from gameObject in a circular range
 * pushing all items that spawned away based on the force.
*/
using UnityEngine;
using System.Collections;

public class ItemPopOut_ItemHolder : MonoBehaviour
{
	
	public GameObject[] Items;
	// list of objects that gonna spawn
	public float fl_ExplosionForce;
	public ForceMode forceMode;
	public float fl_ExplosionRadius;
	public float fl_ExplosionUpForce;

	private int in_limit;
	// random data for iteration
	private float fl_distance;
	// random data for instantiation
	private int iterator = 0;
	// Use this for initialization
	void Start ()
	{
		in_limit = Random.Range (1, Items.Length);	
	}

	void FixedUpdate ()
	{
		foreach (Collider col in Physics.OverlapSphere(transform.position, fl_ExplosionRadius)) {
			if (col.GetComponent<Rigidbody> () != null) {
                
				col.GetComponent<Rigidbody> ().AddExplosionForce (fl_ExplosionForce, transform.position - new Vector3 (0, 0, 1), fl_ExplosionRadius, fl_ExplosionUpForce, forceMode);
			}
		}
	}

	void Update ()
	{

		if (iterator != in_limit) {
			fl_distance = Random.Range (-2, 2);
			GameObject newItem = new GameObject ();
			newItem = Instantiate (Items [iterator], new Vector3 (gameObject.transform.position.x + fl_distance, transform.position.y, gameObject.transform.position.z + fl_distance),
				newItem.transform.rotation) as GameObject;
			iterator++;
		} else
			Invoke ("Destroy", 2);
	}

	void Destroy ()
	{
		GameObject.Destroy (this.gameObject);
	}
}
