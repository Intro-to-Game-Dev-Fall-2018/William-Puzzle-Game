using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour {

    public GameObject gridBlock;

    public AudioSource bkgd;

    public TextMesh s1;
    public TextMesh s2;

    public Sprite green;
    public Sprite black;

    public Vector3 direction;
    public Vector3 direction2;
    public Vector3 current;
    public Vector3 current2;

    public int score1;
    public int score2;


    public float timer = 0f;

    GameObject[,] grid = new GameObject[100, 100];
    

	// Use this for initialization
	void Start () {
		for(int i = -50; i < 50; i++)
        {
            for(int j = -50; j < 50; j++)
            {
                grid[i + 50, j + 50] = Instantiate(gridBlock, new Vector3((i + 0.5f), (j + 0.5f)), Quaternion.identity);
            }
        }

        for(int i = 0; i < 100; i++)
        {
            grid[0, i].GetComponent<SpriteRenderer>().sprite = green;
            grid[i, 0].GetComponent<SpriteRenderer>().sprite = green;
            grid[99, i].GetComponent<SpriteRenderer>().sprite = green;
            grid[i, 99].GetComponent<SpriteRenderer>().sprite = green;
        }


        current = new Vector3(25f, 75f);
        current2 = new Vector3(75f, 25f);
        direction = new Vector3(0, -1);
        direction2 = new Vector3(0, 1);

        score1 = 0;
        score2 = 0;

        bkgd.Play();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        //Debug.Log(current);
        //Debug.Log(current2);
        if (timer >= 0.3f)
        {
            timer = 0;
            current += direction;
            current2 += direction2;
            if(grid[(int)current.x, (int)current.y].GetComponent<SpriteRenderer>().sprite == green)
            {
                score2++;
                gameOver(1);
            }
            if (grid[(int)current2.x, (int)current2.y].GetComponent<SpriteRenderer>().sprite == green)
            {
                score1++;
                gameOver(2);
            }
            grid[(int)current.x, (int)current.y].GetComponent<SpriteRenderer>().sprite = green;
            grid[(int)current2.x, (int)current2.y].GetComponent<SpriteRenderer>().sprite = green;

        }

        //Debug.Log(current.x);      

        //grid[(int) current.x, (int) current.y].GetComponent<SpriteRenderer>().sprite = green;

        if(Input.GetKeyDown("w"))
        {
            direction = new Vector3(0, 1f);
        }
        if (Input.GetKeyDown("a"))
        {
            direction = new Vector3(-1f, 0);
        }
        if (Input.GetKeyDown("s"))
        {
            direction = new Vector3(0, -1f);
        }
        if (Input.GetKeyDown("d"))
        {
            direction = new Vector3(1f, 0);
        }

        if (Input.GetKeyDown("up"))
        {
            direction2 = new Vector3(0, 1f);
        }
        if (Input.GetKeyDown("left"))
        {
            direction2 = new Vector3(-1f, 0);
        }
        if (Input.GetKeyDown("down"))
        {
            direction2 = new Vector3(0, -1f);
        }
        if (Input.GetKeyDown("right"))
        {
            direction2 = new Vector3(1f, 0);
        }
        if (Input.GetKeyDown("r"))
        {
            gameOver(3);
        }

    }

    void gameOver(int s)
    {
        for (int i = -50; i < 50; i++)
        {
            for (int j = -50; j < 50; j++)
            {
                grid[i + 50, j + 50].GetComponent<SpriteRenderer>().sprite = black;
            }
        }

        for (int i = 0; i < 100; i++)
        {
            grid[0, i].GetComponent<SpriteRenderer>().sprite = green;
            grid[i, 0].GetComponent<SpriteRenderer>().sprite = green;
            grid[99, i].GetComponent<SpriteRenderer>().sprite = green;
            grid[i, 99].GetComponent<SpriteRenderer>().sprite = green;
        }

        s1.text = score1.ToString();
        s2.text = score2.ToString();

        //Debug.Log("hello");


        current = new Vector3(25f, 75f);
        current2 = new Vector3(75f, 25f);
        direction = new Vector3(0, -1);
        direction2 = new Vector3(0, 1);

        bkgd.Play();
    }
}
