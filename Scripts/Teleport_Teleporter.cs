using UnityEngine;
using System.Collections;

public class Teleport_Teleporter : MonoBehaviour {
    public GameObject go_TeleMark;
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
            col.gameObject.GetComponent<Transform>().position = go_TeleMark.transform.position;
    }
}
