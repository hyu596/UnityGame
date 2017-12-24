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
    private int basepoint;

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
    private GameObject[] allshap;
    private static Shape4[] allLonely;
    private static Color[] colors;
    private static Color[] colors_2;
    private static Vector2[] LonelyPlace;
	private int[] shapes = new int[10]{ 1, 1, 1, 3, 3, 3, 7, 7, 7, 7 };
    Color darkR = new Color(0.3f, 0, 0);
    Color darkG = new Color(0, 0.3f, 0);
    Color darkB = new Color(0, 0, 0.3f);

    private string[] types;

	public Text scoreText;
	public int score;


    public void init(float x, float y)
    {

        c1 = (int)(x - 7f);
        c2 = (int)(x -3);
        c3 = (int)(x+1);
        r1 = (int)(y-2);
        r2 = (int)(y - 5f);
        lonelyX = (int) (x  - 8f);
        lonelyY1 = (int)(y + 8.5f);
        lonelyY2 = (int)(y + 10f);
        lonelyY3 = (int)(y + 7.5f); 

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
        allshap = new GameObject[6];
        allLonely = new Shape4[3];
		colors = new Color[3]{ Color.red, Color.blue, Color.green };
        colors_2 = new Color[3] { darkR, darkB, darkG };
        LonelyPlace = new Vector2[3] { lonely1, lonely2, lonely3 };

        generateFirstLine();
        generateSecondLine();

        generateLonely();


		moving = false;

		score = 0;

		types = new string[8]{ "Shape1", "Shape2", "Shape3", "Shape4", "Shape5", "Shape6", "Shape7", "Shape8" };
//		shapes = new int[10]{ 1, 1, 1, 3, 3, 3, 7, 7, 7, 7 };

    }
    private void generateLonely()
    {
        SpeawnLonely(0);
        SpeawnLonely(1);
//        SpeawnLonely(2);        
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
//			Managers.Time.newRound ();
        }


        //the number of Lonely shape try to add should base the point or if full grid
//        tryTOAddLonely(1);
   
        



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
            Component[] allchild = allshap[i+3].GetComponentsInChildren(typeof(Renderer));
            foreach (Renderer r in allchild)
                if (r.GetComponent<SpriteRenderer>().color.Equals(darkB))
                {
                    r.GetComponent<SpriteRenderer>().color = Color.blue;
                    Debug.Log("hi");
                }else if(r.GetComponent<SpriteRenderer>().color.Equals(darkG))
                {
                    r.GetComponent<SpriteRenderer>().color = Color.green;
                }
                else
                {
                    r.GetComponent<SpriteRenderer>().color = Color.red;
                }
            allwatingObject[i] = allwatingObject[i + 3];
			allwatingObject [i].index_a -= 3;
            int x = (int)allwatingObject[i].transform.position.x;
            int y = (int)allwatingObject[i].transform.position.y;
			if (y < r1 && !allwatingObject[i].isDone())
            {
				StartCoroutine (allwatingObject [i].SmoothFall (new Vector3 (x, r1, 0f), -100, false));
            }

        }

    }
    public void tryTOAddLonely ( int NumbertryToadd)
    {
        while (NumbertryToadd > 0)
        {
            for (int i = 0; i <3; i++)
            {
                if (allLonely[i] == null || allLonely[i].done)
                {
                    SpeawnLonely(i);
					return;
                }
            }
            NumbertryToadd--;
        }
        NumbertryToadd = 0;
    }
    public static void SpeawnLonely (int index)
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
		move.index_a = index;
        Component[] allchild = nextShape.GetComponentsInChildren(typeof(Renderer));
		int picked = Random.Range (0, 3);
        foreach (Renderer r in allchild)
            if (place.y == r1)
            {
                r.GetComponent<SpriteRenderer>().color = colors[picked];
            }else
            {
                r.GetComponent<SpriteRenderer>().color = colors_2[picked];
            }
            

        move.assignColor (picked);
        allshap[index] = nextShape;
        allwatingObject[index] = move;
        
    }

    string GetRandomShape()
    {
//        int randomindex = Random.Range(1, 8);
		int rnd = Random.Range(0, 9);
//		Debug.Log (shapes[rnd]);
		int randomindex = shapes[rnd];
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

	public void Rotate(Move obj, int n, int c){

		Vector2 pos = obj.transform.position;
		int index = obj.index_a;
		Debug.Log (index);
		Destroy (obj.gameObject);

		GameObject nextShape = (GameObject)Instantiate(Resources.Load(types[n], typeof(GameObject)), pos, Quaternion.identity);
		Move move = nextShape.GetComponent<Move>();
		move.index_a = index;
		Component[] allchild = nextShape.GetComponentsInChildren(typeof(Renderer));
		foreach (Renderer r in allchild)
			r.GetComponent<SpriteRenderer> ().color = colors [c];

		move.assignColor (c);
		allwatingObject[index] = move;
   //     allshap[index] = nextShape;
	}

}
