﻿/*Class: LoadingBar
 * Requires: Image
 * Provides: making the looking of loading bar.
 * Definition: this class makes a bar loads. usually used for loading bars in spalsh screens before levels. the loading isn't based on the actual loading of the level.
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{

	public float loadingTime;
	private double fill;
	public string SceneToLoad;
	// Use this for initialization
	void Start ()
	{
		gameObject.GetComponent<Image> ().fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gameObject.GetComponent<Image> ().fillAmount <= 1.0)
			gameObject.GetComponent<Image> ().fillAmount += 0.1f;
		if (gameObject.GetComponent<Image> ().fillAmount == 1.0)
			LoadScene ();
	}

	
	public void LoadScene ()
	{
		Application.LoadLevel (SceneToLoad);
	}
}
