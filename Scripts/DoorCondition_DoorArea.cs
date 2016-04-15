using UnityEngine;
using System.Collections;

public class DoorCondition_DoorArea : MonoBehaviour
{

	public GameObject go_DoorCollider;
	public string str_Key;
	public Animation anim_Door;

	private bool isOpen = false;

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player" && ItemCollect_Item.isUseable == true) {
			if (isOpen == false) { 
				for (int i = 0; i < 6; i++) {
					if (Inventory.bag [i].tag == str_Key) {
						GameObject.Destroy (go_DoorCollider);
						ItemCollect_Item.isUseable = false;
						//Inventory.Ibag--;
						anim_Door.Play ("DoorOpen");
						isOpen = true;
						break;
					}
				}
			}
		}
	}
}
