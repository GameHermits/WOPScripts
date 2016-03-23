using UnityEngine;
using System.Collections;

public class ItemPopOut_ItemHolder : MonoBehaviour {
	
	public GameObject[] Items; // list of objects that gonna spawn
    public float fl_ExplosionForce;
    public ForceMode forceMode;
    public float fl_ExplosionRadius;
    public float fl_ExplosionUpForce;
    private int in_limit; // random data for iteration
    private float fl_distance; // random data for instantiation  
    
	// Use this for initialization
	void Start () {
        in_limit = Random.Range (1, 5);
		PopOut ();
	}

	void PopOut(){ // when enemy dies make it drop random item

        for (int i = 0; i<=5; i++) {

            fl_distance = Random.Range(-2, 2);
            GameObject newItem = new GameObject();
            newItem = Instantiate(Items[i], new Vector3(gameObject.transform.position.x +fl_distance, transform.position.y, gameObject.transform.position.z +fl_distance),
                newItem.transform.rotation) as GameObject;
            Debug.Log("OK");
        }
        //GameObject.Destroy(this.gameObject);
	}

    void FixedUpdate()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, fl_ExplosionRadius))
        {
            if (col.GetComponent<Rigidbody>() != null)
            {
                
                col.GetComponent<Rigidbody>().AddExplosionForce(fl_ExplosionForce, transform.position, fl_ExplosionRadius, fl_ExplosionUpForce, forceMode);
            }
        }
    }
	
}
