using UnityEngine;
using System.Collections;

public class TelchinesShooter : MonoBehaviour {

	public TelchinesShooter ts_TelchinesShooterRef; // This class object to disable it self after finish

	public GameObject go_FireMetero; // Fire Metero Prefab Refrence
	public GameObject go_NatureRaor; //NatureRoar Prefab Refrence
	public GameObject go_WayHolderOne; //Projectile Luncher right
	public GameObject go_WayHoldertow; // Projectile Luncher middle
	public GameObject go_WayHolderThree; // Projectile Luncher left
	public GameObject go_Minion; // Enemy Prefab
	public GameObject go_Spawner1; // Enemy Spawner one
	public GameObject go_Spawner2; // Enemy spawner two
	public float fl_MovementForce = 50f; // movement force that move the projectile

    private enum enum_Attacks { NatureRaor, Firemeteor, CallToArms }; // Type of Attacks
    private enum_Attacks at_attacks = enum_Attacks.NatureRaor; // Type of Attack
 
    void Update () {
			switch (at_attacks) 
            {
			case enum_Attacks.Firemeteor:
				GameObject go_newBullet = Instantiate (go_FireMetero, //...
		      	go_WayHoldertow.transform.position+go_WayHoldertow.transform.forward,go_WayHoldertow.transform.rotation) as GameObject;
				
				if (!go_newBullet.GetComponent<Rigidbody>()){
                        go_newBullet.AddComponent<Rigidbody>();
				}
                    go_newBullet.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * fl_MovementForce, ForceMode.VelocityChange);
                   
                    at_attacks = enum_Attacks.CallToArms;
				break;
				
			case enum_Attacks.NatureRaor:
				GameObject go_newBullet1 = Instantiate (go_NatureRaor, //...
                go_WayHolderOne.transform.position+ go_WayHolderOne.transform.forward, go_WayHolderOne.transform.rotation) as GameObject;
				GameObject go_newBullet2 = Instantiate (go_NatureRaor, //...
                go_WayHoldertow.transform.position+ go_WayHoldertow.transform.forward, go_WayHoldertow.transform.rotation) as GameObject;				
				GameObject go_newBullet3 = Instantiate (go_NatureRaor, //...
                go_WayHolderThree.transform.position+ go_WayHolderThree.transform.forward, go_WayHolderThree.transform.rotation) as GameObject;
				
				if (!go_newBullet1.GetComponent<Rigidbody>()){
                        go_newBullet1.AddComponent<Rigidbody>();
				}
                    go_newBullet1.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * fl_MovementForce, ForceMode.VelocityChange);
				
				if (!go_newBullet2.GetComponent<Rigidbody>()){
                        go_newBullet2.AddComponent<Rigidbody>();
				}
                    go_newBullet2.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * fl_MovementForce, ForceMode.VelocityChange);
				
				if (!go_newBullet3.GetComponent<Rigidbody>()){
                        go_newBullet3.AddComponent<Rigidbody>();
				}
                    go_newBullet3.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * fl_MovementForce, ForceMode.VelocityChange);
                
                    at_attacks = enum_Attacks.Firemeteor;
				break;
				
			case enum_Attacks.CallToArms:
				GameObject go_newEnemy = Instantiate (go_Minion,//... 
                go_Spawner1.transform.position, go_Spawner1.transform.rotation) as GameObject;
				GameObject go_newEnemy1 = Instantiate (go_Minion,//...
                go_Spawner2.transform.position, go_Spawner2.transform.rotation) as GameObject;
                   
                    at_attacks = enum_Attacks.NatureRaor;
				break;
				
			}
            ts_TelchinesShooterRef.enabled = false;
	}
}
