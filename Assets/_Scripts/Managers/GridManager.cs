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
<<<<<<< HEAD
	private bool[] check;
=======
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
>>>>>>> 8875ba45e58738ac0ae3d94b7f2f333e1d626e8d

    void Awake(){
		left = gameGridcol[0].row[1].position.x - 0.5f;
		right = gameGridcol [0].row [2].position.x + 0.5f;
		bot = gameGridcol [0].row [0].position.y - 0.5f;
		top = validCol [1].row [0].position.y + 2.5f;
		min_y = new int[3]{0, 0, 0};
		cells = 9;
		mid_x = (int) gameGridcol [0].row [0].transform.position.x;
		mid_y = (int) gameGridcol [1].row [0].transform.position.y;
		bot_y = (int) gameGridcol [0].row [0].transform.position.y;
		check = new bool[9]{ false, false, false, false, false, false, false, false, false };
	}

	public int getMinY(int index){
		return min_y[index];
	}

//	public int checkForImp(){
//		int rst = 0;
//		for (int i = 0; i < 9; i++) {
//			if (check [i])
//				rst++;
//		}
//		return rst;
////		if (check [1])
////			Debug.Log ("aaa");
////		else
////			Debug.Log ("bbb");
//	}

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
	}

	public void updateGridForSingleBlock(int x, int y){
		x = x - (mid_x - 1);
		y = y - (mid_y - 1);
		if (min_y [x] <= y) {
			min_y [x] = y + 1;
		}
		check [x + 3 * y] = true;
	}


	public bool validArea(float x1, float x2, float y1, float y2){
		if (x1 <= left || x2 >= right || y1 < bot || y2 > top) {
			return false;
		}
		return true;
	}

<<<<<<< HEAD
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

		Debug.Log (x);

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
					if ( (i + (attempt_y + j_temp) * 3) >= 9 || check [i + (attempt_y + j_temp) * 3])
						fail = true;
				}
			}

			if(!fail)
				return attempt_y;
		}
		return -1;
	}
		
=======
    public static void  SpawnNextShape(Vector2 place)
    {
        GameObject nextShape = (GameObject)Instantiate(Resources.Load(GetRandomShape(), typeof(GameObject)), place, Quaternion.identity);
    }
    static string GetRandomShape()
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
>>>>>>> 8875ba45e58738ac0ae3d94b7f2f333e1d626e8d
}
