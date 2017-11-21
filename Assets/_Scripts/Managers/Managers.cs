using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(GridManager))]
public class Managers : MonoBehaviour {

	public GameObject gridManager;

	[HideInInspector]
	public int index;

	private static GridManager[] _gridManager;
	private int maxSize;
    private static RandomManager randomManager;
    public static GridManager[] Grid{
		get { return _gridManager;}
	}


    void Awake(){

		maxSize = 5;
		index = 0;
		_gridManager = new GridManager[maxSize];

		addGrid (2, 1);
		addGrid (6, 1);

	}

	private void addGrid(int x, int y){
		if (!reachMax ()) {
			GameObject gameObject = (GameObject)Instantiate (gridManager, new Vector3 (x, y, 0f), Quaternion.identity);
			GridManager grid = gameObject.GetComponent<GridManager> ();
			grid.init (x, y);
			_gridManager [index] = grid;
			index ++;
		}
	}

	public bool reachMax(){
		return index >= maxSize - 1;
	}
}
