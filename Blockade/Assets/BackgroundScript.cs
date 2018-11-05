using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundScript : MonoBehaviour {

    public GameObject gridBlock;


    public Sprite green;
    public Sprite black;

    public Vector3 direction;
    public Vector3 direction2;
    public static Vector3 current;
    public static Vector3 current2;

    public float timer = 0f;

    static GameObject[,] grid = new GameObject[100, 100];


    // Use this for initialization
    void Start()
    {
        for (int i = -50; i < 50; i++)
        {
            for (int j = -50; j < 50; j++)
            {
                grid[i + 50, j + 50] = Instantiate(gridBlock, new Vector3((i + 0.5f), (j + 0.5f)), Quaternion.identity);
            }
        }

        for (int i = 0; i < 100; i++)
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

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log(current);
        //Debug.Log(current2);
        if (timer >= 0.3f)
        {
            timer = 0;
            current += direction;
            current2 += direction2;
            grid[(int)current.x, (int)current.y].GetComponent<SpriteRenderer>().sprite = green;
            grid[(int)current2.x, (int)current2.y].GetComponent<SpriteRenderer>().sprite = green;

        }

        int r = Random.Range(0, 3);
        int r2 = Random.Range(0, 3);

        if (r == 0)
        {
            direction = new Vector3(0, 1f);
        }
        if (r == 1)
        {
            direction = new Vector3(-1f, 0);
        }
        if (r == 2)
        {
            direction = new Vector3(0, -1f);
        }
        if (r == 3)
        {
            direction = new Vector3(1f, 0);
        }

        if (r2 == 0)
        {
            direction2 = new Vector3(0, 1f);
        }
        if (r2 == 1)
        {
            direction2 = new Vector3(-1f, 0);
        }
        if (r2 == 2)
        {
            direction2 = new Vector3(0, -1f);
        }
        if (r2 == 3)
        {
            direction2 = new Vector3(1f, 0);
        }


        //Debug.Log(current.x);      

        //grid[(int) current.x, (int) current.y].GetComponent<SpriteRenderer>().sprite = green;

        if (Input.GetKeyDown("1"))
        {
            SceneManager.LoadScene("BlockadeScene", LoadSceneMode.Single);
        }
        

    }



}
