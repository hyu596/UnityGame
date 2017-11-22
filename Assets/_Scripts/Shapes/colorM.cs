using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class colorM : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

}
