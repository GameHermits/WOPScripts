using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static List<GameObject> li_Enemys = new List<GameObject> ();

	private bool pausedC = false;

	private bool paused;

	public bool Paused {
		get {
			return paused;
		}
	}

	private static GameManager gm;

	public static GameManager GM {
		get {
			if(gm == null)
				gm = GameObject.FindObjectOfType<GameManager>();
			return GameManager.gm;
		}
	}
	public GameObject PauseCanvas;
    public GameObject PlayerCanvas;
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
            if (pausedC == true)
            {
                PauseCanvas.SetActive(true);
                PlayerCanvas.SetActive(false);
                Cursor.visible = true;
            }
            else {
                PauseCanvas.SetActive(false);
                PlayerCanvas.SetActive(true);
            }
        }
			
	}

	public void PauseGame(){
		paused = !paused;
		pausedC = !pausedC;
	}
}
