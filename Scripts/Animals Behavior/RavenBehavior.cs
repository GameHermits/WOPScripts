using UnityEngine;
using System.Collections;

public class RavenBehavior : MonoBehaviour
{

	//Private:
	private GameObject go_PlayerLocationRef;
	// Refrence to player gameobject
	private float fl_DangerRange;
	//distance in which raven start flying away
	private Animator RavenAnimator;
	//animator controller attached to gameObject
	private int WayPointsIndex = 0;
	//WayPoints Index
	private bool canMove = false;
	//Control the behavior of the raven
	//Public:
	public GameObject[] WayPoints;
	// contain all the way points as empty gameobjects. It should be set manually from Inspector.
	// Use this for initialization
	void Awake ()
	{
		//Initialaization
		RavenAnimator = gameObject.GetComponent <Animator> ();
		go_PlayerLocationRef = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update ()
	{
		//If Player is too close
		fl_DangerRange = Vector3.Distance (gameObject.transform.position, go_PlayerLocationRef.transform.position);

		if (fl_DangerRange <= 4) {
			canMove = true;
		}
		Move ();
	}

	IEnumerator WaitforSeconds ()
	{
		yield return new WaitForSeconds (1f);
		transform.position = Vector3.MoveTowards (gameObject.transform.position, WayPoints [WayPointsIndex].transform.position, 4 * Time.deltaTime);
	}

	private void Move ()
	{
		//Test canMove and determine weather raven 
		if (canMove == true) {

			if (WayPoints != null) {

				if (Vector3.Distance (gameObject.transform.position, WayPoints [WayPointsIndex].transform.position) <= 0) {
					canMove = false;
					RavenAnimator.SetBool ("ShouldFly", false); //Used to send a message to animator controller to controll it's boolean parameters

					if (WayPointsIndex != WayPoints.Length - 1)
						WayPointsIndex++;
					else
						WayPointsIndex = 0;
				} else {
					transform.LookAt (WayPoints [WayPointsIndex].transform);
					RavenAnimator.SetBool ("ShouldFly", true);
					StartCoroutine ("WaitforSeconds");
				}
			}
		}
	}
}