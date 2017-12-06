using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MouseHover : MonoBehaviour {

    private TextMeshProUGUI textmeshPro;

    // Use this for initialization
    void Start () {
        textmeshPro = GetComponent<TextMeshProUGUI>();
	}

	void OnMouseEnter(){
        textmeshPro.color = Color.cyan;
    }
		
	void OnMouseExit(){
        textmeshPro.color = Color.white;
    }

	void Update(){
		if (Input.GetMouseButtonUp (0) && textmeshPro.color == Color.cyan) {
			
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
