/*Class: CharacterSound
 * Requires: none
 * Provides: general sound functions for different situations
 * Definition: this class can be attached to any character game object that need to speak. it have arrays of sounds for different situations, and have funtions that play random sound of the spacific array
 * of sounds in each function.
 * Note: The user must add sound clips and records manually to each array.
*/
using UnityEngine;
using System.Collections;

public class CharacterSound_General : MonoBehaviour
{
	//public:
	public AudioClip[] agony;
	public AudioClip[] death;
	public AudioClip[] attack;
	public AudioClip[] achieving;
	public AudioClip[] jump;
	public AudioClip[] move;
	//private:
	private AudioSource Sounds;

	// Use this for initialization
	void Start ()
	{
		Sounds = gameObject.GetComponent <AudioSource> ();
	}

	public void Attack ()
	{ // playing random clips when attack
		Sounds.clip = attack [Random.Range (0, attack.Length)];
	}

	public void Agony ()
	{// playing random clips when getting hit
		Sounds.clip = agony [Random.Range (0, agony.Length)];
	}

	public void Death ()
	{ // playing random clips when Die
		Sounds.clip = death [Random.Range (0, death.Length)];
	}

	public void Achieving ()
	{ // playing random clips when achieving a goal or unlocking something
		Sounds.clip = achieving [Random.Range (0, achieving.Length)];
	}

	public void Jump ()
	{ // playing random clips when jumping
		Sounds.clip = jump [Random.Range (0, jump.Length)];
	}

	public void Move ()
	{ // playing random clips when achieving a goal or unlocking something
		Sounds.clip = move [Random.Range (0, achieving.Length)];
	}
}
