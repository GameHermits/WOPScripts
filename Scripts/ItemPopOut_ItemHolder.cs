using UnityEngine;
using System.Collections;

public class ItemPopOut_ItemHolder : MonoBehaviour {
	
	public GameObject[] Items;

	private int limit;

	// Use this for initialization
	void Start () {
		limit = Random.Range (1, 5);
		
		PopOut ();
	}

	void PopOut(){ // when enemy dies make it drop random item
		for (int i = 0; i<=limit; i++) {
			GameObject newItem = new GameObject();
			newItem = Instantiate (Items[i], gameObject.transform.position, newItem.transform.rotation) as GameObject;
		}
	}


	
}
