using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomManager : MonoBehaviour
{

    private int r1;
    private int r2;
    private int c1;
    private int c2;
    private int c3;
    private int lonelyX;
    private int lonelyY1;
    private int lonelyY2;
    private int lonelyY3;

    private Vector2 lin1_1;
    private Vector2 lin1_2;
    private Vector2 lin2_1;
    private Vector2 lin2_2;
    private Vector2 lin3_1;
    private Vector2 lin3_2;
    private Vector2 lonely1;
    private Vector2 lonely2;
    private Vector2 lonely3;

	private bool moving;

    private Move[] allwatingObject;
    private Shape4[] allLonely;
    private Color[] colors;
    private Vector2[] LonelyPlace;

	public Text scoreText;
	public int score;


    public void init(float x, float y)
    {

        c1 = (int)(x - 7f);
        c2 = (int)(x -3);
        c3 = (int)(x+1);
        r1 = (int)(y + 1.5f);
        r2 = (int)(y - 3f);
        lonelyX = (int) (x  - 8f);
        lonelyY1 = (int)(y + 8f);
        lonelyY2 = (int)(y + 10f);
        lonelyY3 = (int)(y + 6f); 

        lin1_1 = new Vector2(c1, r1);
        lin1_2 = new Vector2(c1, r2);
        lin2_1 = new Vector2(c2, r1);
        lin2_2 = new Vector2(c2, r2);
        lin3_1 = new Vector2(c3, r1);
        lin3_2 = new Vector2(c3, r2);
        lonely1 = new Vector2(lonelyX, lonelyY1);
        lonely2 = new Vector2(lonelyX, lonelyY2);
        lonely3 = new Vector2(lonelyX, lonelyY3);

        allwatingObject = new Move[6];
        allLonely = new Shape4[3];
		colors = new Color[3]{ Color.red, Color.blue, Color.green };
        LonelyPlace = new Vector2[3] { lonely1, lonely2, lonely3 };

        generateFirstLine();
        generateSecondLine();

        generateLonely();


		moving = false;

		score = 0;

    }
    private void generateLonely()
    {
        SpeawnLonely(0);
        SpeawnLonely(1);
        SpeawnLonely(2);
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
			Managers.Time.newRound ();
        }

        //the number of Lonely shape try to add should base the point or if full grid
        tryTOAddLonely(1);


		if (moving) {
			if ((!allwatingObject [0].isRunning ()) && (!allwatingObject [1].isRunning ()) && (!allwatingObject [2].isRunning ())) {
				generateSecondLine ();
				moving = false;
			}
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
		moving = true;

        for (int i = 0; i < 3; i += 1)
        {
            allwatingObject[i] = allwatingObject[i + 3];
            int x = (int)allwatingObject[i].transform.position.x;
            int y = (int)allwatingObject[i].transform.position.y;
			if (y < r1 && !allwatingObject[i].isDone())
            {
				StartCoroutine (allwatingObject [i].SmoothFall (new Vector3 (x, r1, 0f)));
            }

        }

    }
    public void tryTOAddLonely ( int NumbertryToadd)
    {
        while (NumbertryToadd > 0)
        {
            for (int i = 0; i <3; i++)
            {
                if (allLonely[i].done)
                {
                    SpeawnLonely(i);
                }
            }
            NumbertryToadd--;
        }
        NumbertryToadd = 0;
    }
    public void SpeawnLonely (int index)
    {
        GameObject newLonely = (GameObject)Instantiate(Resources.Load("Lonely", typeof(GameObject)), LonelyPlace[index] , Quaternion.identity);
        Shape4 move = newLonely.GetComponent<Shape4>();
        allLonely[index] = move;
        int picked = Random.Range(0, 3);
        newLonely.GetComponent<SpriteRenderer>().color = colors[picked];

		move.assignColor (picked);
    }

    public void SpawnNextShape(Vector2 place, int index)
    {
        GameObject nextShape = (GameObject)Instantiate(Resources.Load(GetRandomShape(), typeof(GameObject)), place, Quaternion.identity);
        Move move = nextShape.GetComponent<Move>();
        Component[] allchild = nextShape.GetComponentsInChildren(typeof(Renderer));
		int picked = Random.Range (0, 3);
		foreach (Renderer r in allchild)
			r.GetComponent<SpriteRenderer> ().color = colors [picked];

		move.assignColor (picked);
		allwatingObject[index] = move;
    }

    string GetRandomShape()
    {
        int randomindex = Random.Range(1, 8);
        return "Shape" + randomindex;

    }
    int GetRandomColor()
    {
        int randomindex = Random.Range(0, 3);
        Debug.Log(randomindex);
        return randomindex;
    }

    public bool isInFirstLine(int y)
    {
        return y == r1;
    }

	public void increScore(int s){
		score += s;
		scoreText.text = "Score: " + score;
	}

}
