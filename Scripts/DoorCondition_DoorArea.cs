
using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;

public class DoorCondition_DoorArea : MonoBehaviour
{
	//public:

	//Key needed to open this door
	public string key;
	//Door collider refernce
	public GameObject doorCollider;
	//Indecating weather you want one side of the gate to open or both sides
	public bool OpenBoth = false;
	//GameObject refernce to right side of the gate
	public GameObject rightDoor;
	//GameObject refernce to left side of the gate
	public GameObject leftDoor;

	//private:
	//Controlling opening and closing the gate.
	private bool isOpen = false;
	//Indicating how much time should update run the code
	private int times = 180;

	void ShifttingInventory (int i)
	{
		//This fucntion used to reorgnaize inventory slots.
		//Indicating the next element
		int k = 1;
		GameManager.GM.Player.Inventory [i] = "";
		//loops through the inventory
		while (i + k < GameManager.GM.Player.InvIndex) {
			if (GameManager.GM.Player.Inventory [i + k] != "") {
				GameManager.GM.Player.Inventory [i] = GameManager.GM.Player.Inventory [i + k];
				GameManager.GM.Player.Inventory [i + k] = "";
				i++;
			} else if (GameManager.GM.Player.Inventory [i + k] == "") {
				k++;
			}
		}
		GameManager.GM.Player.InvIndex--;
	}

	void Update ()
	{
		if (isOpen == true) {
			if (times > 0) {
				if (OpenBoth == true) {
					//opens both doors
					rightDoor.transform.Rotate (0f, 0.5f, 0f);
					leftDoor.transform.Rotate (0f, 0.5f, 0f);
					times--;
				} else {
					//opens one door
					if (rightDoor.transform.rotation.y < 90) {
						rightDoor.transform.Rotate (0f, 0.5f, 0f);
						times--;
					}
				}
			}
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player") {
			for (int i = 0; i < 6; i++) {
				if (key == GameManager.GM.Player.Inventory [i]) {
					//opens the door
					isOpen = true;
					GameObject.Destroy (doorCollider);
					ShifttingInventory (i);
				}
				Debug.Log (GameManager.GM.Player.Inventory [i]);
			}

		}
	}
}
