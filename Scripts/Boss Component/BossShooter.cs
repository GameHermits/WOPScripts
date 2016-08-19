/*Class: TelchinesShooter
 * Requires: none
 * Provides: Telchines shooting behavior
 * Definition: This class is a custom class for Telchines shooting behavoir and abilities.
*/
using UnityEngine;
using System.Collections;

public class BossShooter : MonoBehaviour
{
	
	//Shooting place/s for each Boss
	public GameObject[] ShootingPlaces;
	//Light Attack magic spell prefab
	public GameObject lightAttack;
	//Heavy Attack magic spell prefab
	public GameObject HeavyAttack;
	//Special skill for each boss
	public GameObject SpecialSkill;
	//Minions prefab, leave it null if it's of no use
	public GameObject go_Minion;
	//minions' spawner's array of positions
	public GameObject[] Spawners;
	// movement force that move the projectile
	public float spellsMovementSpeed = 60f;
	//Options that let you choose which boss behavior to run.
	public enum Boss
	{
		Telchiens,
		Argus,
		Syphan,
		Martha,
		Merlin}
	;

	public Boss boss = Boss.Telchiens;
	//Player Position
	private Transform Player;
	//Reference to BossBehavior Component
	private BossBehavior BBRef;

	void Start ()
	{
		BBRef = gameObject.GetComponent <BossBehavior> ();
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void TelchiensShooter ()
	{//Implements Telchiens Shooting behavior
		//Local vars
		int randomAttack = Random.Range (0, 10);
		int isLightAttack = 0;
		//If randomAttack is an even number
		if (randomAttack % 2 == 0) {
			isLightAttack = 1;
		}
		switch (isLightAttack) {
		case 1://If boss should light attack
			GameObject newLightSpell = Instantiate (lightAttack, ShootingPlaces [1].transform.position + ShootingPlaces [1].transform.transform.forward, ShootingPlaces [1].transform.rotation) as GameObject;
			GameObject newLightSpell1 = Instantiate (lightAttack, ShootingPlaces [2].transform.position + ShootingPlaces [2].transform.transform.forward, ShootingPlaces [2].transform.rotation) as GameObject;
			GameObject newLightSpell2 = Instantiate (lightAttack, ShootingPlaces [3].transform.position + ShootingPlaces [3].transform.transform.forward, ShootingPlaces [3].transform.rotation) as GameObject;
			newLightSpell.GetComponent <Rigidbody> ().AddForce (ShootingPlaces [1].transform.forward * spellsMovementSpeed, ForceMode.Impulse);
			newLightSpell1.GetComponent <Rigidbody> ().AddForce (ShootingPlaces [2].transform.forward * spellsMovementSpeed, ForceMode.Impulse);
			newLightSpell2.GetComponent <Rigidbody> ().AddForce (ShootingPlaces [3].transform.forward * spellsMovementSpeed, ForceMode.Impulse);
			break;
		case 0://If boss shouldn't light attack
			if (randomAttack == 3 || randomAttack == 9) {
				GameObject newSpecial = Instantiate (SpecialSkill, transform.position, transform.rotation) as GameObject;
			} else if (randomAttack == 5 || randomAttack == 7) {
				GameObject newSpell = Instantiate (HeavyAttack, ShootingPlaces [0].transform.position + ShootingPlaces [0].transform.forward, ShootingPlaces [0].transform.rotation) as GameObject;
				newSpell.GetComponent <Rigidbody> ().AddForce (ShootingPlaces [0].transform.forward * spellsMovementSpeed, ForceMode.Impulse);
			}
			break;
		}
		BBRef.didShoot = true;
		this.enabled = false;
	}

	void ArgusShooter ()
	{//Implements Argus Shooting behavior
		
	}

	void SyphanShooter ()
	{//Implements Syphan Shooting behavior
		
	}

	void MarthaShooter ()
	{//Implements Martha Shooting behavior
		
	}

	void MerlinShooter ()
	{////Implements Merlin Shooting behavior
		
	}

	void Update ()
	{
		switch (boss) {
		case Boss.Telchiens:
			TelchiensShooter ();
			break;
		case Boss.Argus:
			ArgusShooter ();
			break;
		case Boss.Syphan:
			SyphanShooter ();
			break;
		case Boss.Martha:
			MarthaShooter ();
			break;
		case Boss.Merlin:
			MerlinShooter ();
			break;

		}
	}
}
