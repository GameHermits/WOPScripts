using UnityEngine;
using System.Collections;

public class RainEffectChaser : MonoBehaviour
{
	//Private:
	private Transform Player;
	//Public:
	public float chaseSpeed = 5.0f;
	public float xPosition = 0;
	public float yPosition = 0;
	public float zPosition = 0;
	public bool follow = false;
	// Use this for initialization
	void Awake ()
	{
		Player = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gameObject.transform.position != Player.position && follow == false) {
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, Player.position + new Vector3 (xPosition, yPosition, zPosition), chaseSpeed * Time.deltaTime);
		} else if (gameObject.transform.position != Player.position && follow == true) {
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, Player.position + new Vector3 (0, yPosition, 0) - new Vector3 (xPosition, 0, zPosition), chaseSpeed * Time.deltaTime);
		}
	}
}
