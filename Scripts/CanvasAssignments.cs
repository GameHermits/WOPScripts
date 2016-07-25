using UnityEngine;
using System.Collections;

public class CanvasAssignments : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
	{
		if (gameObject.name == "PlayerCanvas") {
			GameManager.GM.PlayerCanvas = gameObject;
		} else if (gameObject.name == "DieCanvas") {
			GameManager.GM.DieCanvas = gameObject;
		} else if (gameObject.name == "PauseCanvas") {
			GameManager.GM.PauseCanvas = gameObject;
		} else if (gameObject.name == "ReviveCanvas") {
			GameManager.GM.ReviveCanvas = gameObject;
		}
	}
}
