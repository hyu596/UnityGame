using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridManager))]
public class Managers : MonoBehaviour {

	private static GridManager _gridManager;
	public static GridManager Grid{
		get { return _gridManager;}
	}

	void Awake(){
		_gridManager = GetComponent<GridManager> ();
	}
}
