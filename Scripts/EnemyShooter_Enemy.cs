using UnityEngine;
using System.Collections;

public class EnemyShooter_Enemy : MonoBehaviour {
	
	public float fl_MovmentForce = 20.0f;
	public GameObject go_BulletPrefab;
	public float fl_FireRate = 2f;
	public GameObject go_ShootingPLace;

    private float fl_Timer = 10;
    private EnemyBehavior_Enemy eb_EnemyBehaviorRef;
    private GameObject Player;
    private int in_randomInteger;
    private float fl_nextDamage = 0f;
    void Start()
    {
        eb_EnemyBehaviorRef = gameObject.GetComponent<EnemyBehavior_Enemy>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	// Update is called once per frame
	void Update () {
		if (!GameManager.GM.Paused){// If game isn't paused

            transform.LookAt(eb_EnemyBehaviorRef.tr_Target);
            if (Time.time >= fl_nextDamage)// after passing spacific seconds, assigne a new value to timer.
            {
                fl_nextDamage = Time.time + fl_FireRate;
                if (gameObject.tag == "NEnemy")
                {
                    NEnemyShoot();

                }
                else if (gameObject.tag == "LEnemy")
                    LEnemyShoot();
            }
            
        }
	}

	void NEnemyShoot(){// Enable Enemy to shoot
		
        GameObject newBullet = Instantiate(go_BulletPrefab, go_ShootingPLace.transform.position + go_ShootingPLace.transform.forward,gameObject.transform.rotation) as GameObject;
		newBullet.GetComponent<Rigidbody> ().AddForce (transform.forward * fl_MovmentForce, ForceMode.VelocityChange);
        
        in_randomInteger = Random.Range(1, 3);
        Debug.Log(in_randomInteger);
        switch (in_randomInteger)
        {
            case 1:
                fl_FireRate = 1f;
                break;
            case 2:
                fl_FireRate = 2f;break;
        }
    }

    void LEnemyShoot()
    {// Enable Enemy to shoot

        if (fl_Timer == fl_FireRate)
        {// Controling fire rate by timer, every two seconds in timer Shoot

            GameObject newBullet = Instantiate(go_BulletPrefab, go_ShootingPLace.transform.position + go_ShootingPLace.transform.forward, gameObject.transform.rotation) as GameObject;
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * fl_MovmentForce, ForceMode.VelocityChange);

            in_randomInteger = Random.Range(1, 4);
            switch (in_randomInteger)
            {
                case 1 : gameObject.transform.position = new Vector3(Player.transform.position.x + eb_EnemyBehaviorRef.fl_ShootingRange,
                            gameObject.transform.position.y, Player.transform.position.z);                    
                    break;
                case 2: gameObject.transform.position = new Vector3(Player.transform.position.x - eb_EnemyBehaviorRef.fl_ShootingRange,
                            gameObject.transform.position.y, Player.transform.position.z);
                    break;
                case 3: gameObject.transform.position = new Vector3(Player.transform.position.x,
                            gameObject.transform.position.y, Player.transform.position.z + eb_EnemyBehaviorRef.fl_ShootingRange);
                    break;
                case 4: gameObject.transform.position = new Vector3(Player.transform.position.x,
                            gameObject.transform.position.y, Player.transform.position.z - eb_EnemyBehaviorRef.fl_ShootingRange);
                    break;
            }
        }
    }

}
