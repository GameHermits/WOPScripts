﻿using UnityEngine;
using System.Collections;

public class RavenBehavior : MonoBehaviour
{

	//Private:
	private GameObject go_PlayerLocationRef;
	private float fl_DangerRange;
	private Animator RavenAnimator;
	private int WayPointsIndex = 0;
	//Public:
	public GameObject[] WayPoints;
	// Use this for initialization
	void Awake ()
	{
		RavenAnimator = gameObject.GetComponent <Animator> ();
		go_PlayerLocationRef = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update ()
	{
		fl_DangerRange = Vector3.Distance (gameObject.transform.position, go_PlayerLocationRef.transform.position);

		if (fl_DangerRange <= 4) {
			if (WayPoints != null) {
				transform.LookAt (WayPoints [WayPointsIndex].transform);
				RavenAnimator.SetBool ("ShouldFly", true);
				transform.position = Vector3.MoveTowards (gameObject.transform.position, WayPoints [WayPointsIndex].transform.position, 4 * Time.deltaTime);
			}
		}
		if (Vector3.Distance (transform.position, WayPoints [WayPointsIndex].transform.position) <= 0) {
			if (fl_DangerRange > 4)
				RavenAnimator.SetBool ("ShouldFly", false);
			
			if (WayPointsIndex != WayPoints.Length - 1)
				WayPointsIndex++;
			else
				WayPointsIndex = 0;
		}
	}
}