using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	public Text remainingTime;
	public Text roundTime;

	public static float totalTime;
	private float round;

	// Use this for initialization
	void Start () {
		totalTime = 30f;
		round = 5f;
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
	}

	public void newRound(){
		round = 5f;
	}

}
