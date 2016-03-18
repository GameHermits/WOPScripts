using UnityEngine;
using System.Collections;

public class Support : MonoBehaviour {

    public float fl_CDTimer;
    public int in_suppLimit;

    private enum SupportCharacters{Clover, Adam, Ethan, Lauren }
    private SupportCharacters sc_CurrentSupp = SupportCharacters.Clover;

    private float fl_CoolDown = 0;
    private Health_General he_Heal;
    private bool Is_SuppUsed = false;
    private HPController_General hpc_GameObjectRef;

	// Use this for initialization
	void Start () {
        he_Heal = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_General>();
        hpc_GameObjectRef = GameObject.FindGameObjectWithTag("Player").GetComponent<HPController_General>();
	}
	
    public void SupportJob()
    {
        switch (sc_CurrentSupp)
        {
            case SupportCharacters.Clover:
                he_Heal.fl_health = he_Heal.fl_maxhealth;
                he_Heal.HealHealthBar(he_Heal.fl_maxhealth);
                fl_CoolDown = 60;
                Mana.mana = Mana.maxMana;
                hpc_GameObjectRef.fl_tmpManabar = 1;

                break;
        }
    }
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyUp(KeyCode.Mouse1) && Is_SuppUsed == false)
        {
            SupportJob();
            Is_SuppUsed = true;
        }
        if (Is_SuppUsed == true)
            fl_CoolDown -= 60 * Time.deltaTime;

        else if (fl_CoolDown <=0) {
            Is_SuppUsed = false;
        } 
	}
}
