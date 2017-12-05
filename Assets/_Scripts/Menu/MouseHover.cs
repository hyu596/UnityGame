using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseHover : MonoBehaviour {

	private TextMesh text;

	// Use this for initialization
	void Start () {
		text = GetComponent<TextMesh> ();
		text.color = Color.white;
	}

	void OnMouseEnter(){
		text.color = Color.cyan;
	}
		
	void OnMouseExit(){
		text.color = Color.white;
	}

	void Update(){
		if (Input.GetMouseButtonUp (0) && text.color == Color.cyan) {
			
			OnMouseExit ();

			if (this.tag == "start") {
				SceneManager.LoadScene (1);
				if (Time.timeScale == 0)
					Time.timeScale = 1;
			}
			else if (this.tag == "quit")
				Application.Quit ();
				

		}
	}
}
