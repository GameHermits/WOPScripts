
using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;

public class DoorCondition_DoorArea : MonoBehaviour
{
	public string key;
	public Animation anim_Door;
	public GameObject doorCollider;

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player") {
			Debug.Log ("Found Player");
			for (int i = 0; i < 6; i++) {
				if (key == GameManager.GM.Player.Inventory [i]) {
					//opens the door
					Debug.Log ("found Item");
					GameObject.Destroy (doorCollider);
					anim_Door.Play ();
					GameManager.GM.Player.Inventory [i] = "";
					GameManager.GM.Player.InvIndex--;
				}
			}

		}
	}
}
