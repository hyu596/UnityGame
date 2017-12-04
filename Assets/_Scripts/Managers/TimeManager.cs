﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {

	public Text remainingTime;
	public Text roundTime;

	private float totalTime;
	private float round;

	// Use this for initialization
	void Start () {
		totalTime = 15f;
		round = 10f;
	}
	
	// Update is called once per frame
	void Update () {

		if (round >= 0) {
			round -= Time.deltaTime;
			roundTime.text = round.ToString ("0.0");
		} else {
			totalTime -= Time.deltaTime;
			if(totalTime > 0)
				remainingTime.text = "Remaining Time: " + totalTime.ToString("0.0");
		}

		if (totalTime <= 0) {
			StaticClass.CrossSceneInformation = Managers.Random.scoreText.text;
			SceneManager.LoadScene (2);
		}
	}

	public void newRound(){
		round = 10f;
	}

}
