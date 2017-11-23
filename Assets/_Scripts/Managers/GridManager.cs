using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Column{
	public Transform[] row = new Transform[3];
}

public class GridManager : MonoBehaviour {

	public Column[] gameGridcol = new Column[3];
	public Column[] validCol = new Column[2];

	[HideInInspector]
	public int[] min_y;

	[HideInInspector]
	public int cells;

	[HideInInspector]
	public int mid_x, mid_y, bot_y;

	private float left, right, top, bot;
	private bool[] check;
	private Transform trans;
	private List<GameObject> fixedShapes;

	public void init(int x, int y){

		mid_x = x;
		bot_y = y;

		left = mid_x - 1.5f;
		right = mid_x + 1.5f;
		bot = bot_y - 0.5f;
		top = bot_y + 5.5f;
		min_y = new int[3]{0, 0, 0};
		cells = 9;

		mid_y = bot_y + 1;
		check = new bool[9]{ false, false, false, false, false, false, false, false, false };

		fixedShapes = new List<GameObject> ();
	}

	public int getMinY(int index){
		return min_y[index];
	}

	public void addShape(GameObject obj){fixedShapes.Add(obj);}

	public void updateGrid(Move obj, int y){

		int x = (int)obj.transform.position.x - (mid_x - 1);
		int index_offset = 1 - x;

		int start = 0, end = 3;
		if (x == 2)
			start = 1;
		else if (x == 0)
			end = 2;

		for (int i = start; i < end; i++) {
			int a = obj.accumulate [i + index_offset], h = obj.heights [i + index_offset];
			for(int j=0; j<Mathf.Min(a, h); j++){
				int j_temp = j;
				if (a != h)
					j_temp += 1;
				check [i + (y + j_temp) * 3] = true;
				min_y[i] = y + j_temp + 1;
			}
		}

		cells -= obj.counts;

		addShape (obj.gameObject);

		Managers.Random.increScore (obj.counts);
	}

	public void updateGridForSingleBlock(int x, int y){
		x = x - (mid_x - 1);
		y = y - (mid_y - 1);
		if (min_y [x] <= y) {
			min_y [x] = y + 1;
		}
		check [x + 3 * y] = true;
		cells -= 1;

		Managers.Random.increScore (1);

	}


	public bool validArea(float x1, float x2, float y1, float y2){

		if (x1 <= left || x2 >= right || y1 < bot || y2 > top) {
			return false;
		}
		return true;
	}

	public bool checkForSingleBlock(int x, int y){
		x = x - (mid_x - 1);
		y = y - (mid_y - 1);
		if (x >= 3 || x < 0 || y >= 3 || y < 0)
			return false;
		return !check [x + 3 * y];
	}

	public int findPosY(Move obj, int x){

		x -= mid_x - 1;
		int index_offset = 1 - x;

		int start = 0, end = 3;
		if (x == 2)
			start = 1;
		else if (x == 0)
			end = 2;

		for (int attempt_y = getMinY (x); attempt_y < 3; attempt_y++) {

			bool fail = false;

			for (int i = start; i < end && !fail; i++) {
				int a = obj.accumulate [i + index_offset], h = obj.heights [i + index_offset];
				for(int j=0; j<Mathf.Min(a, h); j++){
					int j_temp = j;
					if (a != h)
						j_temp += 1;
					if ( (i + (attempt_y + j_temp) * 3) >= 9 || check [i + (attempt_y + j_temp) * 3] || (attempt_y + j_temp) < getMinY(i))
						fail = true;
				}
			}

			if(!fail)
				return attempt_y;
		}
		return -1;
	}

	void Update(){
		bool sameColor = true;
		int temp = -1;
		if (cells == 0) {
			foreach (GameObject obj in fixedShapes) {
				if (temp == -1)
					temp = obj.GetComponent<Move> ().getColor ();
				else {
					if (obj.GetComponent<Move> () && temp != obj.GetComponent<Move> ().getColor ())
						sameColor = false;
					else if (obj.GetComponent<Shape4> () && temp != obj.GetComponent<Shape4> ().getColor ())
						sameColor = false;
				}
//				Destroy (obj);
			}
			foreach (GameObject obj in fixedShapes) {
				Destroy (obj);
			}

			if (sameColor)
				Managers.Random.increScore (10);
			else
				Managers.Random.increScore (1);
			
			fixedShapes.Clear ();
			revertOrigin ();
		}
	}

	private void revertOrigin(){

		min_y = new int[3]{0, 0, 0};
		cells = 9;
		check = new bool[9]{ false, false, false, false, false, false, false, false, false };

	}

}