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

	private float left, right, top, bot;
	private int[] min_y;

	void Awake(){
		left = gameGridcol[0].row[1].position.x - 0.5f;
		right = gameGridcol [0].row [2].position.x + 0.5f;
		bot = gameGridcol [0].row [0].position.y - .5f;
		top = validCol [1].row [0].position.y;
		min_y = new int[3]{0, 0, 0};
	}

	public int getMinY(int index){return min_y[index];}

	public bool validArea(Transform obj){
		int pos_x = (int)obj.position.x;
		int pos_y = (int)obj.position.y;

		if (pos_x < left || pos_x > right || pos_y < bot || pos_y > top)
			return false;
		return true;
	}

//	public void updateGrid(Transform obj){
//		
//	}
}
