using UnityEngine;
using System.Collections;

public class Sense_Sensor : MonoBehaviour {

	public GameObject go_Spawners;// will have game object of spawners
    public GameObject go_RocksPrefab; // refrence to rocks prefab that will block the way
    public float fl_BlockroadTimer; //when will the rocks fall after the player trigger the sensor; 

	void OnTriggerEnter (Collider col){// when the player collide with sensor
        if (col.gameObject.tag == "Player")
        { // check this is the player
            go_Spawners.SetActive(true); // activate the spawners
            Invoke("BlockRoad", 4);//activate rocks gameobject
                                            //anim_RocksAnimationRef.Play("Rock Animation");      
        }
     }
    void BlockRoad()
    {
        go_RocksPrefab.SetActive(true);
    }
}
