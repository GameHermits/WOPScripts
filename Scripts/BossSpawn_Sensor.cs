/* Class : BossSpawn
 * Uses : detecting triggers to spawn an object, use it on a sensor.
 * Requires : none.
 * provides : spawnnign the object " Boss" .
 * Definition : Usually used for spawnning level's boss. it uses the Unity.Object.setActive function instead of instantiate for performance matters.
 * Best use is to attach it to a sensor, and the class will detect trigger with any object tagged " Player".
*/
using UnityEngine;
using System.Collections;

public class BossSpawn_Sensor : MonoBehaviour
{
		
	public GameObject Boss;
	//the boss to be spawn

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player") {
			Boss.SetActive (true);
			GameObject.Destroy (gameObject);
		}
	}
}
