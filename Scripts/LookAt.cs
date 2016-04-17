using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{

	public Transform target;

	void Start ()
	{
		if (gameObject.name == "ShootingPLace")
			target = GameObject.FindWithTag ("Player").transform;
	}
	// Update is called once per frame
	void Update ()
	{
		transform.LookAt (target);
	}
}
