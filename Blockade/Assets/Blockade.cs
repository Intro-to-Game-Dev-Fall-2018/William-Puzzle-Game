using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour {

    public GameObject gridBlock;

    public Sprite green;

    public Vector3 direction = new Vector3(0, 0);
    public Vector3 current;

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

        current = new Vector3(50f, 50f);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0;
            current += direction;
        }

        //Debug.Log(current.x);

        grid[(int) current.x, (int) current.y].GetComponent<SpriteRenderer>().sprite = green;

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

        current += direction;
    }
}
