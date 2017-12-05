using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [HideInInspector]
    public bool still_moving;

    [HideInInspector]
    public int[] accumulate;

    [HideInInspector]
    public int[] heights;

    [HideInInspector]
    public int counts;

	private Vector2 origin_place;
	private Vector2 destination, pivot;
	private float fallingSpeed;
	private int dest_y, color;
	private bool done, coroutine;
	private GridManager grid_temp;

	protected void Awake () {
		still_moving = false;
		pivot = transform.position;
		fallingSpeed = .2f;
		done = false;
		coroutine = false;
		accumulate = new int[3];
		heights = new int[3];
		color = 0;
	}


	public IEnumerator SmoothFall (Vector3 end, int y=-100){ 

		this.transform.localScale = new Vector3 (1f, 1f, 1f); 
		
		coroutine = true;
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 new_pos = Vector3.MoveTowards (transform.position, end, Time.deltaTime / fallingSpeed);
			transform.position = new_pos;
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
		if(y!=-100)
			grid_temp.updateGrid (this, y);
		coroutine = false;
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

		int test = 0;

		foreach(GridManager g in Managers.Grid) {

			if (g && g.validArea (transform.position.x + x1,
				transform.position.x + x2, transform.position.y + y1,
				transform.position.y + y2)) {
				grid_temp = g;
				return true;
			}

		}

		return false;

	}

    private bool abletomove()
    {
        return transform.position.y > -4 && TimeManager.totalTime > 0;
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

		if (Input.GetMouseButtonDown (0) && abletomove()) {

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
			origin_place = transform.position;
			if (hit.collider != null && hit.collider.transform == this.transform)
			{
				still_moving = true;
				pivot = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
				destination = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			}
		}

		if (still_moving) {
			if (Input.GetMouseButtonUp (0)) {
				still_moving = false;
//				if (!checkForValid () || !Managers.Random.isInFirstLine(((int)origin_place.y))) {
				if (!checkForValid ()) {
					transform.position = origin_place;
				} else {
					int x = (int) Mathf.Round(transform.position.x);
					int y = grid_temp.findPosY(this, x);
					if (y == -1) {
						transform.position = origin_place;
						return;
					} else {
						transform.position = new Vector2(x, transform.position.y);
						Vector3 dest = new Vector3 (x, y + grid_temp.bot_y, 0);
						StartCoroutine (SmoothFall (dest, y));
						done = true;

						SpriteRenderer[] spriteRenderer = GetComponentsInChildren<SpriteRenderer> ();
						foreach(SpriteRenderer s in spriteRenderer){
							s.sortingLayerName = "Grid";
						}

						this.transform.SetParent (grid_temp.transform);

						BoxCollider2D[] collider = GetComponents<BoxCollider2D>();
						foreach (BoxCollider2D c in collider) {
							c.enabled = false;
						}

					}
				}
			} else {
				destination = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				transform.position = destination - pivot;

				if (checkForValid ()) {
					int x = (int)Mathf.Round (transform.position.x);
					int y = grid_temp.findPosY (this, x);
					if (y != -1) {
						grid_temp.addShadow (this, x, y);
					} 

//					grid_temp.clearShadow ();
				} else {
					if (grid_temp != null)
						grid_temp.clearShadow ();
				}

					
			}
		}

	}

	public bool isDone(){ return done; }
	public bool isRunning() {
		return coroutine;
	}  

	public void assignColor(int c) {
		color = c;
	}

	public int getColor(){return color;}




}