using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;
public class ItemCollect_Item : MonoBehaviour {

	//public static bool isUseable = false; // to check if the player can collect items
	public Sprite icon;
	void OnTriggerEnter ( Collider col){ // when the item collition to the player but it in the inventory

		if (col.gameObject.tag == "Player") {
			Inventory.IN.AddItem (icon);
			Destroy (this.gameObject);
			//isUseable = true;
			//gameObject.SetActive(false);
		}
	}
}
