using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CcolorChange : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position.x);

        GetComponent<Renderer>().material.color = Color.red;

    }

}