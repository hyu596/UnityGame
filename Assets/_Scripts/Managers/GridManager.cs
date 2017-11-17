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
	public int mid;

	private float left, right, top, bot;
    Vector2 lin1_1 = new Vector2(-5, -3);
    Vector2 lin1_2 = new Vector2(-5, -6);
    Vector2 lin2_1 = new Vector2(0, -3);
    Vector2 lin2_2 = new Vector2(0, -6);
    Vector2 lin3_1 = new Vector2(4, -3);
    Vector2 lin3_2 = new Vector2(4, -6);
    private void Start()
    {
        SpawnNextShape(lin1_1);
        SpawnNextShape(lin1_2);
        SpawnNextShape(lin2_1);
        SpawnNextShape(lin2_2);
        SpawnNextShape(lin3_1);
        SpawnNextShape(lin3_2);
    }

    void Awake(){
		left = gameGridcol[0].row[1].position.x - 0.5f;
		right = gameGridcol [0].row [2].position.x + 0.5f;
		bot = gameGridcol [0].row [0].position.y - 0.5f;
		top = validCol [1].row [0].position.y + 2.5f;
		min_y = new int[3]{0, 0, 0};
		cells = 9;
		mid = (int) gameGridcol [0].row [0].transform.position.x;
	}

	public int getMinY(int index){
		return min_y[index - (mid - 1)];
	}

	public void updateGrid(int pos_x, int[] a, int c){
		int x = pos_x - (mid - 1);
		int index_offset = 1 - x;

		int start = 0, end = 3;
		if (x == 2)
			start = 1;
		else if (x == 0)
			end = 2;
		for (int i = start; i < end; i++) {
			min_y [i] += a [i + index_offset];
		}
		cells -= c;
	}

	public bool validArea(float x1, float x2, float y1, float y2){
		if (x1 <= left || x2 >= right || y1 < bot || y2 > top) {
			return false;
		}
		return true;
	}

    public void SpawnNextShape(Vector2 place)
    {
        GameObject nextShape = (GameObject)Instantiate(Resources.Load(GetRandomShape(), typeof(GameObject)), place, Quaternion.identity);
    }
    string GetRandomShape()
    {
        int randomindex = Random.Range(1, 8);
        string randomShapeName = "Shape";
        
        switch(randomindex)
        {
            case 1:
                randomShapeName = "Shape1_1";
                break;
            case 2:
                randomShapeName = "Shape1_2";
                break;
            case 3:
                randomShapeName = "Shape2_2";
                break;
            case 4:
                randomShapeName = "Shape2_3";
                break;
            case 5:
                randomShapeName = "Shape2_4";
                break;
            case 6:
                randomShapeName = "Shape3_1";
                break;
            case 7:
                randomShapeName = "Shape3_2";
                break;
            case 8:
                randomShapeName = "Shape2_4";
                break;
        }
        return randomShapeName;

    }
}
