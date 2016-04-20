using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;
public class DoorCondition_DoorArea : MonoBehaviour
{

	public GameObject go_DoorCollider;
	//public string str_Key;
	public Sprite key;
	private bool isUseable;
	public Animation anim_Door;
	public GameObject doorCollider;
	private bool isOpen = false;

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player" ) {
			isUseable = Inventory.IN.CanUseItem (key);
			if (isUseable == true) {
				GameObject.Destroy (doorCollider);
			} else {
			}
		}
	}
}
