using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{

	public Transform target;

	// Update is called once per frame
	void Update ()
	{
		if (gameObject.name == "ShootingPLace" && target == null) {
			transform.LookAt (GameObject.FindWithTag ("Player").transform);
		} else
			transform.LookAt (target);
	}
}
