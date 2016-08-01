using UnityEngine;
using System.Collections;

public class DangerZone : MonoBehaviour
{
	public Transform RevivePlace;
	//This script is used to kill the player if he stepped or fall into danger zones like lava or trap etc
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player") {
			SceneManager.SM.ReviveInPlace = false;
			SceneManager.SM.tempRevivePosition = RevivePlace.position;
			GameManager.GM.PlayerGameObject.GetComponent <Health_General> ().ApplayDamage (GameManager.GM.Player.maxHealth);
			GameManager.GM.Player.mana = 0;
			GameManager.GM.Player.manaAmount = 0;
		}
	}
}
