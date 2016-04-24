using UnityEngine;
using System.Collections;

public class CharacterSound_General : MonoBehaviour
{
	//public:
	public AudioClip[] Agony;
	public AudioClip[] Death;
	public AudioClip[] Attack;

	//private:
	private AudioSource Sounds;
	// Use this for initialization
	void Start ()
	{
		Sounds = gameObject.GetComponent <AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
