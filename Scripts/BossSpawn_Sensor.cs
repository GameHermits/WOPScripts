using UnityEngine;
using System.Collections;

public class BossSpawn_Sensor : MonoBehaviour {
		
	public GameObject Boss; //the boss to be spawn

	void OnTriggerEnter( Collider col){
		if (col.gameObject.tag == "Player") {
			Boss.SetActive (true);
			GameObject.Destroy(gameObject);
		}
	}
}
