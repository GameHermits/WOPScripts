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
	//indecating the name that should be recorded in the player inventory
	public string name;

	void OnTriggerEnter (Collider col)
	{// when the item collision to the player distroy it and put it in the inventory

		if (col.gameObject.tag == "Player") {
			//adding this object to player inventory by it's name
			GameManager.GM.Player.Inventory [GameManager.GM.Player.InvIndex] = name;
			GameManager.GM.Player.InvIndex++;
		}
	}
}
