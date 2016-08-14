/*This Script is used to manage the play of the music in the level. 
 * This Script should be attached to the SceneManager empty gameobject in the level.
*/
using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

	public AudioClip[] Music;
	public AudioClip[] BackGroundSFX;
	public float quietTime;
	public float delayTime;

	private AudioSource MusicSource;

	public void StopCombatMusic ()
	{
		MusicSource.Stop ();
		MusicSource.loop = false;
	}

	public void StartCombatMusic (AudioClip clip)
	{
		MusicSource.Stop ();
		MusicSource.clip = clip;
		MusicSource.loop = true;
		MusicSource.Play ();
	}

	void Start ()
	{
		if (BackGroundSFX.Length > 0) {
			for (int i = 0; i < BackGroundSFX.Length; i++) {
				AudioSource newSource = gameObject.AddComponent <AudioSource> ();
				newSource.clip = BackGroundSFX [i];
				newSource.loop = true;
				newSource.priority = 256;
				newSource.PlayDelayed (delayTime);
			}
		}

		if (Music.Length > 0) {
			MusicSource = gameObject.AddComponent <AudioSource> ();
			MusicSource.clip = Music [0];
			MusicSource.loop = false;
			MusicSource.priority = 0;
			MusicSource.volume = 1;
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if (MusicSource.isPlaying == false && MusicSource.loop == false) {
			MusicSource.Stop ();
			MusicSource.clip = Music [Random.Range (0, Music.Length)];
			MusicSource.PlayDelayed (quietTime);
		}
	}
}
