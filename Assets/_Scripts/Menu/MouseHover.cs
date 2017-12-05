using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MouseHover : MonoBehaviour {

	private TextMesh text;
   // private TextMeshPro textmeshPro;

    // Use this for initialization
    void Start () {
		text = GetComponent<TextMesh> ();
    //    textmeshPro = GetComponent<TextMeshPro>();

    //    textmeshPro.color = Color.white;
        text.color = Color.white;
	}

	void OnMouseEnter(){
		text.color = Color.cyan;
    //    textmeshPro.color = Color.cyan;
    }
		
	void OnMouseExit(){
		text.color = Color.white;
     //   textmeshPro.color = Color.white;
    }

	void Update(){
		if (Input.GetMouseButtonDown (0) ) {
			if (this.tag == "start")
				SceneManager.LoadScene (1);
			else if (this.tag == "quit")
				Application.Quit ();
		}
	}
}
