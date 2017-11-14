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

	private float left, right, top, bot;

	void Awake(){
		left = gameGridcol[0].row[1].position.x - 0.5f;
		right = gameGridcol [0].row [2].position.x + 0.5f;
		bot = gameGridcol [0].row [0].position.y - 0.5f;
		top = validCol [1].row [0].position.y + 2.5f;
		min_y = new int[3]{0, 0, 0};
		cells = 9;
	}

	public int getMinY(int index){
		return min_y[index];
	}

	public void updateGrid(int pos_x, int[] a, int c){
		int diff = pos_x - (int)gameGridcol [0].row [0].position.x;
		for (int i = diff; i < 3; i++) {
			min_y [i] += a [i - diff];
		}
		cells -= c;
	}

	public bool validArea(float x1, float x2, float y1, float y2){

		if (x1 <= left || x2 >= right || y1 < bot || y2 > top) {
			return false;
		}
		return true;
	}
}
