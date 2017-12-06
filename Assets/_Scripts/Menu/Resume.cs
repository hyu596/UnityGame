using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Resume : MonoBehaviour {

//	public GameObject concaves, buttons;
	public Pause pause;

	private TextMesh text;
    private TextMeshProUGUI textmeshPro;

    void Start(){
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
			pause.Resume ();
		}
	}

}
