using UnityEngine;
using System.Collections;

public class ResetGame_ContinueBtn : MonoBehaviour {

	private Health_General relive__GameObjectRef ;

	void Start()
	{
		relive__GameObjectRef = gameObject.GetComponent<Health_General>();
	}

	public void OnStartEnter(){
		if (GameManager.GM.isDead == true) {
			GameManager.GM.isDead = false;
			if (relive__GameObjectRef.fl_health <= 0) {
				relive__GameObjectRef.fl_health = (relive__GameObjectRef.fl_maxhealth) / 2;
				Mana.mana = (Mana.maxMana)/2;
				GameManager.GM.DieCanvas.SetActive(false);
			}

		}
	}
}
