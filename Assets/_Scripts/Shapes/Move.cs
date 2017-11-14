using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	[HideInInspector]
	public bool still_moving;

	private int[] accumulate;
	private int counts;

	private Vector2 destination, pivot;
	private float fallingSpeed;
	private int dest_y;
	private bool done;

	// Use this for initialization
	protected void Awake () {
		still_moving = false;
		pivot = transform.position;
		fallingSpeed = .2f;
		done = false;
		accumulate = new int[3];
	}

	public IEnumerator SmoothFall (Vector3 end){
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 new_pos = Vector3.MoveTowards (transform.position, end, Time.deltaTime / fallingSpeed);
			transform.position = new_pos;
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}

	private bool checkForValid (){
		float x = 0f, y = 0f;
		if (accumulate [2] == 1)
			x = 1f;
		if (accumulate [1] == 2)
			y = 1f;

		return Managers.Grid.validArea (transform.position.x - 1f, 
			transform.position.x + x, transform.position.y,
			transform.position.y + y);
		
	}

	private int getDestY(int x){
		int height = Mathf.Max (accumulate [0], Mathf.Max (accumulate [1], accumulate [2]));
		int y_max = 0;
		for (int i = x; i < 3; i++) {
			if(accumulate[i-x] != 0)
				y_max = Mathf.Max (y_max, Managers.Grid.getMinY (i));
		}
		if (y_max + height > 3)
			return -1;
		return (int)y_max;
	}

	protected void init(int[] a, int c){
		for (int i = 0; i < 3; i++) {
			accumulate [i] = a [i];
		}
		counts = c;
	}

	protected void Update () {

		if (done)
			return;

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

		if (!still_moving && checkForValid()) {
			int x = (int) Mathf.Round(transform.position.x);
			int y = getDestY (x);
			if (y == -1)
				return;
			transform.position = new Vector2(x, transform.position.y);
			Vector3 dest = new Vector3 (x, y, 0);
			StartCoroutine (SmoothFall (dest));
			done = true;
			Managers.Grid.updateGrid ((int)transform.position.x, accumulate, counts);
		}
			
	}


		
}
