using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MouseHover : MonoBehaviour {

	private TextMesh text;
    private TextMeshPro textmeshPro;

    // Use this for initialization
    void Start () {
		text = GetComponent<TextMesh> ();
        textmeshPro = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();

        textmeshPro.color = Color.red;
        text.color = Color.red;
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
		if (Input.GetMouseButtonDown (0) && text.color == Color.cyan) {
			
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
