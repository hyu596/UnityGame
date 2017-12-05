using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	public GameObject concaves, buttons;

	private SpriteRenderer spriteRenderer;
	private GameObject[] objs;

	void Start(){
		spriteRenderer = GetComponent<SpriteRenderer> ();
		objs = GameObject.FindGameObjectsWithTag ("gameObject");
	}

	void OnMouseEnter(){
		spriteRenderer.color = Color.cyan;
	}

	void OnMouseExit(){
		spriteRenderer.color = Color.white;
	}

	void Update(){
		if (Input.GetMouseButtonUp (0) && spriteRenderer.color == Color.cyan) {


			OnMouseExit ();

			if (Time.timeScale == 0) {

				Resume ();

			}
			else {

				Time.timeScale = 0;
				concaves.SetActive (false);
				buttons.SetActive (true);

				foreach (GameObject obj in objs) {
					obj.SetActive (false);
				}

			}


		}
	}

	public void Resume(){

		Time.timeScale = 1;
		concaves.SetActive (true);
		buttons.SetActive (false);

		foreach (GameObject obj in objs) {
			obj.SetActive (true);
		}

	}

}
