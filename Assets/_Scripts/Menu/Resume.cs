using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Resume : MonoBehaviour {

//	public GameObject concaves, buttons;
	public Pause pause;

	private TextMesh text;

	void Start(){
		text = GetComponent<TextMesh> ();
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
			pause.Resume ();
		}
	}

}
