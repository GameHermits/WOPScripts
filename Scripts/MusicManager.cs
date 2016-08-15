/*This Script is used to manage the play of the music in the level. 
 * This Script should be attached to the SceneManager empty gameobject in the level.
*/
using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
	//public:
	//Background Music of the level.
	public AudioClip[] Music;
	//Background Environmental sounds.
	public AudioClip[] BackGroundSFX;
	//Combat Music
	public AudioClip[] CombatMusic;
	//Boss Music
	public AudioClip BossMusic;
	//How much time should the next Music wait before playing.
	public float quietTime;
	//how much time should the Environment Sounds wait before starting.
	public float delayTime;
	//bool to control fading out music
	[HideInInspector]
	public bool FadeOut = false;
	//private:
	//Audio Source created in runtime to play the music
	private AudioSource MusicSource;
	//bool to control deeping in music
	private bool FadeIn = false;

	public void StopCombatMusic ()
	{//called by the sensor script before getting destroied so that it stops the combat music
		MusicSource.Stop ();
		MusicSource.loop = false;
	}

	public void StartCombatMusic (AudioClip clip)
	{//called by the sensor script OnTriggerEnter function so that it stops the idle music and plays the combat music
		MusicSource.Stop ();
		MusicSource.clip = clip;
		MusicSource.loop = true;
		MusicSource.Play ();
		FadeOut = false;
		FadeIn = true;
	}

	void Start ()
	{//creating audio source for each environmental sound and play it after the delay time.
		if (BackGroundSFX.Length > 0) {
			for (int i = 0; i < BackGroundSFX.Length; i++) {
				AudioSource newSource = gameObject.AddComponent <AudioSource> ();
				newSource.clip = BackGroundSFX [i];
				newSource.loop = true;
				newSource.priority = 256;
				newSource.volume = 0.5f;
				newSource.PlayDelayed (delayTime);
			}
		}
		//creating Music audio source and play the element music in Music array
		if (Music.Length > 0) {
			MusicSource = gameObject.AddComponent <AudioSource> ();
			MusicSource.clip = Music [0];
			MusicSource.loop = false;
			MusicSource.priority = 0;
			MusicSource.volume = 1;
			MusicSource.Play ();
		}
	}
	// Update is called once per frame
	void Update ()
	{
		//Whenever the music clip in MusicSource stopped, assign another music clip from Music array and play it after quiettime delay.
		if (MusicSource.isPlaying == false && MusicSource.loop == false) {
			MusicSource.Stop ();
			MusicSource.clip = Music [Random.Range (0, Music.Length)];
			MusicSource.PlayDelayed (quietTime);
		}

		if (FadeOut == true) {
			MusicSource.volume -= 0.01f;
			if (MusicSource.volume <= 0) {
				StartCombatMusic (CombatMusic [Random.Range (0, CombatMusic.Length)]);
			}
		} else if (FadeIn == true) {
			MusicSource.volume += 0.01f;
			if (MusicSource.volume >= 1) {
				FadeIn = false;
			}
		}
	}
}
