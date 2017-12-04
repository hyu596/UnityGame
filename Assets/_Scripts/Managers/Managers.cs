using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RandomManager))]
[RequireComponent(typeof(TimeManager))]
public class Managers : MonoBehaviour
{

    public GameObject gridManager;

    [HideInInspector]
    public int index;

    private static GridManager[] _gridManager;
    private static RandomManager _randomManager;
	private static TimeManager _timeManager;

    private int maxSize;

    public static GridManager[] Grid
    {
        get { return _gridManager; }
    }

    public static RandomManager Random
    {
        get { return _randomManager; }
    }

	public static TimeManager Time{
		get { return _timeManager; }
	}

    void Awake()
    {

        maxSize = 5;
        index = 0;

        _gridManager = new GridManager[maxSize];

		addGrid(-2, 1);
		addGrid(2, 1);

        _randomManager = GetComponent<RandomManager>();
        _randomManager.init(3f, -4.5f);

		_timeManager = GetComponent<TimeManager> ();



    }

    private void addGrid(int x, int y)
    {
        if (!reachMax())
        {
            GameObject gameObject = (GameObject)Instantiate(gridManager, new Vector3(x, y, 0f), Quaternion.identity);
            GridManager grid = gameObject.GetComponent<GridManager>();
            grid.init(x, y);
            _gridManager[index] = grid;
            index++;
        }
    }


    public bool reachMax()
    {
        return index >= maxSize - 1;
    }
}
