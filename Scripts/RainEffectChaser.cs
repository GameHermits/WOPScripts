using UnityEngine;
using System.Collections;

public class RainEffectChaser : MonoBehaviour
{
	//Private:
	private Transform Player;
	//Public:
	public float chaseSpeed = 5.0f;
	// Use this for initialization
	void Awake ()
	{
		Player = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gameObject.transform.position != Player.position) {
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, Player.position + new Vector3 (0, 6, 0), chaseSpeed * Time.deltaTime);
		}
	}
}
