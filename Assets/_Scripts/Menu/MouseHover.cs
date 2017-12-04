using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour {

	private Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		text.color = Color.white;

		Debug.Log (transform.position.x);
	}
	
	void OnMouseEnter(){
		Debug.Log ("aaaa");
		text.color = Color.blue;
	}

	void OnMouseExit(){
		text.color = Color.white;
	}
}
