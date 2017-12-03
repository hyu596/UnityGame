using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape4 : MonoBehaviour {

	[HideInInspector]
	public bool still_moving;

	[HideInInspector]
	public int counts;
//    public static Vector2 origin_place;
    private Vector2 destination, pivot;
	private int dest_y, color;
	public bool done;

	private GridManager grid_temp;

	// Use this for initialization
	void Awake () {
		still_moving = false;
		pivot = transform.position;
        //origin_place = transform.position;
        done = false;
		color = 0;
	}

	public int getColor(){
		return color;
	}

	private bool checkForValid(int x, int y){
		foreach(GridManager g in Managers.Grid) {
			if (g && g.checkForSingleBlock (x, y)) {
				grid_temp = g;
				return true;
			}
		}
		return false;
	}


	void Update () {

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

		if (!still_moving) {

			int x = (int)Mathf.Round (transform.position.x);
			int y = (int)Mathf.Round (transform.position.y);
			if (checkForValid (x, y)) {
				transform.position = new Vector2 (x, y);
				grid_temp.updateGridForSingleBlock (x, y);
				done = true;

				SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
				spriteRenderer.sortingLayerName = "Grid";

				BoxCollider2D[] collider = GetComponents<BoxCollider2D>();
				foreach (BoxCollider2D c in collider) {
					c.enabled = false;
				}

				grid_temp.addShape (this.gameObject);

			}

		}

	}



}