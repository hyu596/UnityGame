using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	[HideInInspector]
	public bool still_moving;

	private int[] accumulate;
	private int[] heights;
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
		heights = new int[3];
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
		float x1 = 0f, x2 = 0f, y1 = 0f, y2 = 0f;

		if (accumulate [0] != 0)
			x1 = -1f;
		if (accumulate [2] != 0)
			x2 = 1f;
		if (accumulate [1] == 3) {
			y1 = -1f;
			y2 = 1f;
		}
		if (accumulate [1] == 2) {
			y2 = 1f;
		}

		return Managers.Grid.validArea (transform.position.x + x1, 
			 transform.position.x + x2, transform.position.y + y1,
			 transform.position.y + y2);
		
	}

	private int getDestY(int x){
		
		int height = Mathf.Max (accumulate [0], accumulate [1], accumulate [2]);
		int offset = Managers.Grid.mid - 1, index_offset = 0;
		int y_max = 0;

		int start = 0, end = 3;
		if (x == 2) {
			start = 1;
			index_offset = -1;
		} else if (x == 0) {
			end = 2;
			index_offset = 1;
		}
		
		for (int i = start; i < end; i++) {
			int y_potential = Managers.Grid.getMinY (i + offset);
			if (accumulate [i + index_offset] != 0 && y_potential > y_max) {
				y_max = y_potential;
				height = heights [i + index_offset];
			}
		}
		Debug.Log (Managers.Grid.min_y [1]);

						
		if (y_max + height > 3) {
//			Debug.Log (y_max);
			return -1;
		}
		if (height == Mathf.Max (accumulate [0], accumulate [1], accumulate [2])) {
			Managers.Grid.updateGrid ((int)transform.position.x, accumulate, counts);
			return (int)y_max + (int)Managers.Grid.gameGridcol [0].row [0].transform.position.y;
		}
		Managers.Grid.updateGrid ((int)transform.position.x, heights, counts);
		return (int)y_max + (int)Managers.Grid.gameGridcol[0].row[0].transform.position.y - height;

	}

	protected void init(int[] a, int[] h, int c){
		for (int i = 0; i < 3; i++) {
			accumulate [i] = a [i];
			heights [i] = h [i];
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
			int y = getDestY (x - (Managers.Grid.mid - 1));
			if (y == -1) {
				return;
			}
			transform.position = new Vector2(x, transform.position.y);
			Vector3 dest = new Vector3 (x, y, 0);
			StartCoroutine (SmoothFall (dest));
			done = true;
//			Managers.Grid.updateGrid ((int)transform.position.x, accumulate, counts);
		}
			
	}


		
}
