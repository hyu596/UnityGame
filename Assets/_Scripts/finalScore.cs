using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finalScore : MonoBehaviour {

	private Text scoreText;

	// Use this for initialization
	void Start () {

		scoreText = GetComponent<Text> ();
		scoreText.text = StaticClass.CrossSceneInformation;
		
	}

}
