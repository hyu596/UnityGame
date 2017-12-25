using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
    public Image timer;
    public float increaseAmount;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
        timer.fillAmount = TimeManager.totalTime/5;
        Debug.Log(TimeManager.totalTime / 5);
	}
}
