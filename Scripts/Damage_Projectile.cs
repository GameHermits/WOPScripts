using UnityEngine;
using System.Collections;

public class Damage_Projectile : MonoBehaviour {

	public float fl_dmgAmount; // Damage amount of every Projectile.
	//public Animation animation; // The animator of this object of being destroied.
	//public string animationName; // the animation Name the will be played upon hitting something.

	void OnTriggerEnter (Collider col){

		if (col.gameObject.tag == "Terrain") { // If the projectile hit the envoiroment
			//animation.Play(animationName);
			GameObject.Destroy (this.gameObject);
		} 
		else if (col.gameObject.tag == "NEnemy" || col.gameObject.tag == "LEnemy") {// If hit a character
            col.gameObject.GetComponent<Health_General>().ApplayDamage(fl_dmgAmount);
            col.gameObject.GetComponent<Health_General>().DamageHealthBar(fl_dmgAmount);
            //animation.Play(animationName);	
            GameObject.Destroy (this.gameObject);
		}
        else if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Health_General>().ApplayDamage(fl_dmgAmount);
            col.gameObject.GetComponent<Health_General>().DamageHealthBar(fl_dmgAmount);
            //animation.Play(animationName);	
            GameObject.Destroy(this.gameObject);
        }
		
		
	}
}
