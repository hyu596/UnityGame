using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	[HideInInspector]
	public bool still_moving;

	[HideInInspector]
	public int[] accumulate;

	[HideInInspector]
	public int[] heights;

	[HideInInspector]
	public int counts;

	private Vector2 destination, pivot;
	private float fallingSpeed;
	private int dest_y;
	private bool done;

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
			int y = Managers.Grid.findPosY(this, x);
			if (y == -1) {
				return;
			} else {
				
				transform.position = new Vector2(x, transform.position.y);
				Vector3 dest = new Vector3 (x, y + Managers.Grid.bot_y, 0);
				StartCoroutine (SmoothFall (dest));
				done = true;

				Managers.Grid.updateGrid (this, y);

				BoxCollider2D[] collider = GetComponents<BoxCollider2D>();
				foreach (BoxCollider2D c in collider) {
					c.enabled = false;
				}
			}

		}
			
	}


		
}
