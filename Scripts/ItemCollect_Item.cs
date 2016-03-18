using UnityEngine;
using System.Collections;

public class ItemCollect_Item : MonoBehaviour {

	public static bool isUseable = false; // to check if the player can collect items

	void OnTriggerEnter ( Collider col){ // when the item collition to the player but it in the inventory

		if (col.gameObject.tag == "Player") {
			Inventory.bag[Inventory.Ibag] = gameObject;
			Inventory.Ibag++;
			isUseable = true;
			gameObject.SetActive(false);
		}
	}
}
