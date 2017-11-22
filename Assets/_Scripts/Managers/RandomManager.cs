using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager : MonoBehaviour
{

    private int r1;
    private int r2;
    private int c1;
    private int c2;
    private int c3;

    private Vector2 lin1_1;
    private Vector2 lin1_2;
    private Vector2 lin2_1;
    private Vector2 lin2_2;
    private Vector2 lin3_1;
    private Vector2 lin3_2;

    private Move[] allwatingObject;


    public void init(float x, float y)
    {

        c1 = (int)(x - 4f);
        c2 = (int)x;
        c3 = (int)(x + 4f);
        r1 = (int)(y + 1.5f);
        r2 = (int)(y - 1.5f);

        lin1_1 = new Vector2(c1, r1);
        lin1_2 = new Vector2(c1, r2);
        lin2_1 = new Vector2(c2, r1);
        lin2_2 = new Vector2(c2, r2);
        lin3_1 = new Vector2(c3, r1);
        lin3_2 = new Vector2(c3, r2);

        allwatingObject = new Move[6];

        generateFirstLine();
        generateSecondLine();
    }

    private void generateFirstLine()
    {
        SpawnNextShape(lin1_1, 0);
        SpawnNextShape(lin2_1, 1);
        SpawnNextShape(lin3_1, 2);
    }

    private void generateSecondLine()
    {

        SpawnNextShape(lin1_2, 3);
        SpawnNextShape(lin2_2, 4);
        SpawnNextShape(lin3_2, 5);

    }

    void LateUpdate()
    {
        if (IsFirstlineEmpt())
        {
            Move2ndLineUp();
        }
    }


    private bool IsFirstlineEmpt()
    {

        for (int i = 0; i < 3; i += 1)
        {
            if (!allwatingObject[i].isDone())
                return false;
        }

        return true;
    }

    public void Move2ndLineUp()
    {
        for (int i = 0; i < 3; i += 1)
        {
            allwatingObject[i] = allwatingObject[i + 3];
            int x = (int)allwatingObject[i].transform.position.x;
            int y = (int)allwatingObject[i].transform.position.y;
            if (y < r1)
            {
                allwatingObject[i].transform.position = new Vector2(x, r1);
            }

        }
        generateSecondLine();
    }

    public void SpawnNextShape(Vector2 place, int index)
    {
        GameObject nextShape = (GameObject)Instantiate(Resources.Load(GetRandomShape(), typeof(GameObject)), place, Quaternion.identity);
        Move move = nextShape.GetComponent<Move>();
        allwatingObject[index] = move;
  
        Component[] allchild = nextShape.GetComponentsInChildren(typeof(Renderer));
        int color = GetRandomColor();
        if (color == 0)
        {
            foreach (Renderer x in allchild)
            {
                x.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        if (color == 1)
        {
            foreach (Renderer x in allchild)
            {
                x.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
        if (color == 2)
        {
            foreach (Renderer x in allchild)
            {
                x.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }



    }

    string GetRandomShape()
    {
        int randomindex = Random.Range(1, 4);
        return "Shape" + randomindex + "_1";

    }
    int GetRandomColor()
    {
        int randomindex = Random.Range(0, 3);

        return randomindex;
    }

    public bool isInFirstLine(int y)
    {
        return y == r1;
    }

}
