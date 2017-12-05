using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finalScore : MonoBehaviour {

	public Text scoreText;
	public Text highestScoreText;

	// Use this for initialization
	void Start () {

		scoreText.text = "Score: " + StaticClass.CrossSceneInformation;

		if (StaticClass.highestScore < StaticClass.CrossSceneInformation) {
			StaticClass.highestScore = StaticClass.CrossSceneInformation;
			PlayerPrefs.SetInt ("highest", StaticClass.highestScore);
			PlayerPrefs.Save ();
		}
		highestScoreText.text = "Highest Score: " + StaticClass.highestScore;
	}

}
