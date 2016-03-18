using UnityEngine;
using System.Collections;

public class SplashScreen_PlayBtn : MonoBehaviour {

	public GameObject go_CanvasHolder;//the game object holds the canvas

	public void OnStartEnter(){// when click play btn
		go_CanvasHolder.SetActive (true);//show the canvas with the splash screen image
	}
}
