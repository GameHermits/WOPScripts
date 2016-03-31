using UnityEngine;
using System.Collections;

public class Damage_Projectile : MonoBehaviour {

	public float fl_dmgAmount; // Instant Damage amount for every Projectile.
    public float fl_FreezeDmgAmount; // Freeze Damage amount for every projectile
    public float fl_FreezeTime; // set to zero if the projectile isn't a freeze type.
	//public Animation animation; // The animator of this object of being destroied.
	//public string animationName; // the animation Name the will be played upon hitting something.
    public enum ProjectileType { Freezedmg, InstantDmg, OverTimeDmg};
    public ProjectileType projectile = ProjectileType.InstantDmg;
    public float PoisonTime;

    //Private:
    private PlayerController_Player PlC_Ref; //A refrence object for the player controller compnent
    private PlayerShooter_MainCamera PS_Ref; //A refrence object for the player Shooter compnent
    private MouseLooker ML_Ref; //A refrence object for the player Mouse looker compnent
    
    //private EnemyBehavior_Enemy EB_Ref; // A refrnce to enemies main behavior component

    void Start()
    {
        PlC_Ref = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController_Player>();
        PS_Ref = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerShooter_MainCamera>();
        ML_Ref = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLooker>();
    }

    IEnumerator Wait()
    {
        Debug.Log("freezeWait");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("Freeze unlocked");
        PlC_Ref.enabled = true;
        PS_Ref.enabled = true;
//        ML_Ref.enabled = true;
        GameObject.Destroy(this.gameObject);
    }

    public void InstantDamage( Collider col) // Called when Instant Damage Type of projectile is selected
    {
        if (col.gameObject.tag == "NEnemy" || col.gameObject.tag == "LEnemy")
        {// If hit a character
            col.gameObject.GetComponent<Health_General>().ApplayDamage(fl_dmgAmount);
            col.gameObject.GetComponent<Health_General>().DamageHealthBar(fl_dmgAmount);
            //animation.Play(animationName);	
            GameObject.Destroy(this.gameObject);
            Debug.Log("trigger Work");
        }
        else if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Health_General>().ApplayDamage(fl_dmgAmount);
            col.gameObject.GetComponent<Health_General>().DamageHealthBar(fl_dmgAmount);
            //animation.Play(animationName);	
            GameObject.Destroy(this.gameObject);
        }
    }
    
    public void FreezeDamage (Collider col) // Called when Freeze Damage Type of projectile is selected
    {
        gameObject.GetComponent<TimedObjectDestructor>().enabled = false;
        if (col.gameObject.tag == "Player")
        {
            PlC_Ref.enabled = false;
            PS_Ref.enabled = false;
 //          ML_Ref.enabled = false;
            col.gameObject.GetComponent<Health_General>().ApplayDamage(fl_FreezeDmgAmount);
            col.gameObject.GetComponent<Health_General>().DamageHealthBar(fl_FreezeDmgAmount);
            //animation.Play(animationName);
            StartCoroutine(Wait());
        }  
    }

    public void DamageoverTimr(Collider col)
    {
        gameObject.GetComponent<TimedObjectDestructor>().enabled = false;
        if (col.gameObject.tag=="Player")
        {
            while(PoisonTime > 0)
            {
                col.gameObject.GetComponent<Health_General>().ApplayDamage(fl_FreezeDmgAmount);
                col.gameObject.GetComponent<Health_General>().DamageHealthBar(fl_FreezeDmgAmount);
            }
            GameObject.Destroy(gameObject);
        }
    }
	void OnTriggerEnter (Collider col){

        if (col.gameObject.tag == "Terrain")
        { // If the projectile hit the envoiroment
          //animation.Play(animationName);
            GameObject.Destroy(this.gameObject);
        }
        else if (projectile == ProjectileType.InstantDmg)
            InstantDamage(col);
        else if (projectile == ProjectileType.Freezedmg)
            FreezeDamage(col);
        else if (projectile == ProjectileType.OverTimeDmg)
            DamageoverTimr(col);
	}
}
