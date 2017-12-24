using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {

	public Text remainingTime;
//	public Text roundTime;

	private float round;
	private float totalCount;
	private float totalTime;


	// Use this for initialization
	void Start () {
		totalTime = 10f;
		round = 10f;
		totalCount = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (round);

//		if (round >= 0) {
//			round -= Time.deltaTime;
//			roundTime.text = round.ToString ("0.0");
//		} else {
//			totalTime -= Time.deltaTime;
//			if(totalTime > 0)
//				remainingTime.text = "Remaining Time: " + totalTime.ToString("0.0");
//		}

		totalCount += Time.deltaTime;

		if (totalTime >= 0) {
			totalTime -= Time.deltaTime;
			if(totalTime > 0)
				remainingTime.text = "Remaining Time: " + totalTime.ToString("0.0");
		}
		else {
			StaticClass.CrossSceneInformation = Managers.Random.score;
			SceneManager.LoadScene (2);
		}
	}

	public void newRound(){
		totalTime = round;
	}

	public bool easier(){
		if (round >= 7 ) {
			round -= 1.5f;
			totalCount = 0f;
			newRound ();
			return true;
		}
		else if (round >= 3 ) {
			round -= 1f;
			totalCount = 0f;
			newRound ();
			return true;
		}

		return false;
	}

	public bool isEasy(){
		return (round * totalCount) >= 200;
	}

}
