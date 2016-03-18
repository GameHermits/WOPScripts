using UnityEngine;
using System.Collections;

public class Health_General : MonoBehaviour {
	

	public float fl_health = 2000; //the main health variable
	 
	public float fl_maxhealth = 2000; //the maxmum health value

	public GameObject go_itemHolder; //to hold any items the player gets like keys for gate

    private HPController_General hpc_GameObjectRef;

    void Start()
    {
        /*if (gameObject.tag == "Player")
            go_itemHolder = null;*/
        hpc_GameObjectRef = gameObject.GetComponent<HPController_General>();
    }

	void Update(){
		if (fl_health <= 0) {
			//if ( gameObject.name != "Player"){
			GameObject newGO = Instantiate (go_itemHolder, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			GameObject.Destroy (gameObject);
			//}
		}
		else if (fl_health > fl_maxhealth)
			fl_health = fl_maxhealth;


	}

    public void ApplayDamage (float fl_Damage)
    {
        fl_health = fl_health - (fl_Damage/2);
    }

    public void DamageHealthBar (float fl_Damage)
    {
        
        hpc_GameObjectRef.fl_tmpHealthbar = hpc_GameObjectRef.fl_tmpHealthbar - (fl_Damage/(fl_maxhealth*2));
    }

    public void HealHealthBar (float fl_heal)
    {
        hpc_GameObjectRef.fl_tmpHealthbar = hpc_GameObjectRef.fl_tmpHealthbar + (fl_heal / (fl_maxhealth * 2));
    }

}