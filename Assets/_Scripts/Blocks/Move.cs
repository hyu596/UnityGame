using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	private bool still_moving;
	private Vector2 destination, pivot;

	// Use this for initialization
	void Start () {
		still_moving = false;
		pivot = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);

			if(hit.collider != null && hit.collider.transform == this.transform)
			{
				still_moving = true;
				pivot = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
				destination = Camera.main.ScreenToWorldPoint (Input.mousePosition);	
			}
		}

		if (still_moving) {
			if (Input.GetMouseButtonUp (0)) {
				still_moving = false;
			} else {
				destination = Camera.main.ScreenToWorldPoint (Input.mousePosition);	
//				if(destination != pivot)
				transform.position = destination - pivot;
			}
		}

//		if (keep_updating) {
//			if (!Input.GetMouseButtonUp (0)) {
//				destination = Camera.main.ScreenToWorldPoint (Input.mousePosition);	
//			} else {
//				keep_updating = false;
//			}
//		}
//
//		if (still_moving) {
//			transform.position = destination - pivot;
//			Vector2 curr = transform.position;
//			if ((curr - (destination - pivot)).sqrMagnitude < float.Epsilon && !keep_updating) {
//				still_moving = false;
//				pivot = transform.position;
//			}
//				
//		}


	}
		
}
