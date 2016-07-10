/*Class:
 * Requires:
 * Provides:
 * Definition:
*/
using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;

public class ItemCollect_Item : MonoBehaviour
{

	public Sprite icon;
	//this sprite variable to hold image of the item and show it in the inventory

	void OnTriggerEnter (Collider col)
	{// when the item collision to the player distroy it and put it in the inventory

		if (col.gameObject.tag == "Player") {
			Inventory.INV.AddItem (icon);//when the player catch the item it will show its image in the inventory and save it there
			Destroy (this.gameObject);//the item will be saved in the inventory and destroyed from the scene
		}
	}
}
