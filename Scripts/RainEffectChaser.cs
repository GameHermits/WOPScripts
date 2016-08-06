using UnityEngine;
using System.Collections;

public class RainEffectChaser : MonoBehaviour
{
	//public:

	// the max distance between the partice and the player that will make the particle reset it's position .
	public float maxDistance;
	// Indicating particle hight from the player when it's position is reseted.
	public float yPosition;
	// Indicating partice Z position from the player when it's position is reseted.
	public float zPosition;
	//Private:

	// Player position
	private Transform Player;
	private float currentDistance;
	//Indicating the value of adding particle Z position and player Z position to determine weather to move forward or backward
	private float zValue;

	// Use this for initialization
	void Awake ()
	{
		Player = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentDistance = Vector3.Distance (transform.position, Player.transform.position);
		zValue = transform.position.z + Player.transform.position.z;
		if (currentDistance > maxDistance) {
			if (zValue > 0) {
				gameObject.transform.position = Player.transform.position + new Vector3 (0, yPosition, -zPosition);	
			} else {
				gameObject.transform.position = Player.transform.position + new Vector3 (0, yPosition, zPosition);
			}
		}
	}
}
