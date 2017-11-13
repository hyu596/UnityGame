using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	
	private bool still_moving;
	private Vector2 destination, pivot;
	private float fallingSpeed;
	private int dest_y;

	// Use this for initialization
	void Start () {
		still_moving = false;
		pivot = transform.position;
		fallingSpeed = 1.5f;
	}

	IEnumerator SmoothFall (Vector3 end){
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 new_pos = Vector3.MoveTowards (transform.position, end, Time.deltaTime / fallingSpeed);
			transform.position = new_pos;
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
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
				transform.position = destination - pivot;
			}
		}

		if (!still_moving && Managers.Grid.validArea (this.transform)) {
			int x = (int)transform.position.x;
			transform.position = new Vector2(x, transform.position.y);
			Vector3 dest = new Vector3 (x, Managers.Grid.getMinY (x), 0);
			StartCoroutine (SmoothFall (dest));
		}

	}


		
}
