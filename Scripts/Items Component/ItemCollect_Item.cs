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
	void OnTriggerEnter (Collider col)
	{// when the item collision to the player distroy it and put it in the inventory

		if (col.gameObject.tag == "Player") {
			//adding this object to player inventory by it's name
			GameManager.GM.Player.Inventory [GameManager.GM.Player.InvIndex] = gameObject.name;
			GameManager.GM.Player.InvIndex++;
			Debug.Log (GameManager.GM.Player.Inventory [0]);
			Destroy (this.gameObject);//the item will be saved in the inventory and destroyed from the scene
		}
	}
}
