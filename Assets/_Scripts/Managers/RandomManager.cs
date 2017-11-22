using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager : MonoBehaviour {


    public static int r1 = -3;
    public static int r2 = -6;
    public static int c1 = 1;
    public static int c2 = 5;
    public static int c3 = 9;

    public static Vector2 lin1_1 = new Vector2(c1, r1);
    public static Vector2 lin1_2 = new Vector2(c1, r2);
    public static Vector2 lin2_1 = new Vector2(c2, r1);
    public static Vector2 lin2_2 = new Vector2(c2, r2);
    public static Vector2 lin3_1 = new Vector2(c3, r1);
    public static Vector2 lin3_2 = new Vector2(c3, r2);

    public static List<GameObject> allwatingObject = new List<GameObject>();

    // Use this for initialization
    private void Start()
    {
        SpawnNextShape(lin1_1);
        SpawnNextShape(lin1_2);
        SpawnNextShape(lin2_1);
        SpawnNextShape(lin2_2);
        SpawnNextShape(lin3_1);
        SpawnNextShape(lin3_2);
        allwatingObject[1].GetComponent<Renderer>().material.color = Color.red;
    }
//    public bool checkInsidG(Vector2 pos)
//    {
//        return ((int)pos.x >= 0 && (int)pos.x <= waitingGridW && (int)pos.y >= 0 && (int)pos.y <= waitingGridH);
//    }
    public static bool IsFirstlineEmpt()
    {
        
        for (int i = 0; i < allwatingObject.Count; i+=1)
        {

            if(allwatingObject[i].transform.position.y == r1)
            {
                return false;
            }
            
        }


        return true;
    }
    public static void removeNotWatingObject()
    {
        List<GameObject> newallwatingObject = new List<GameObject>();
        for (int i = 0; i < allwatingObject.Count; i += 1)
        {
            if (allwatingObject[i].transform.position.y == -5)
            {
                newallwatingObject.Add(allwatingObject[i]);
            }
        }
        allwatingObject = newallwatingObject;
    }
    public static void Move2ndLineUp()
    {
        for (int i = 0; i < allwatingObject.Count; i += 1)
        {
           
            int x = (int)allwatingObject[i].transform.position.x; 
            int y = (int)allwatingObject[i].transform.position.y;
            if (y < r1) {
                allwatingObject[i].transform.position = new Vector2(x, r1);
            }
            
        }
    }

    // Update is called once per frame
    void Update () {
	}

    public static void SpawnNextShape(Vector2 place)
    {
        GameObject nextShape = (GameObject)Instantiate(Resources.Load(GetRandomShape(), typeof(GameObject)), place, Quaternion.identity);
        allwatingObject.Add(nextShape);
    }

    static string GetRandomShape()
    {
        int randomindex = Random.Range(1, 4);
        string randomShapeName = "Shape";

        switch (randomindex)
        {
            case 1:
                randomShapeName = "Shape1_1";
                break;
            case 2:
                randomShapeName = "Shape2_1";
                break;
            case 3:
                randomShapeName = "Shape3_1";
                break;
        }
        return randomShapeName;

    }
}
