
using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;

public class DoorCondition_DoorArea : MonoBehaviour
{

	public Sprite key;
	// to hold the image of needed key
	private bool isUseable;
	public Animation anim_Door;
	public GameObject doorCollider;
	private bool isOpen = false;

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player" && isOpen == false) { // when the door collision to the player open it and remove the item from the inventory
			isUseable = Inventory.IN.CanUseItem (key);//to check if you have the key in the inventory and use it
			if (isUseable == true) {
				isOpen = true;
				//anim_Door.Play ();
				GameObject.Destroy (doorCollider);//remove the collider and open the door
			} else {
			}
		}
	}
}
