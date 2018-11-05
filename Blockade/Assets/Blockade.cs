using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blockade : MonoBehaviour {

    public GameObject gridBlock;

    public AudioSource bkgd;
    public AudioSource hit;

    public TextMesh s1;
    public TextMesh s2;

    public Sprite green;
    public Sprite black;

    public Vector3 direction;
    public Vector3 direction2;
    public static Vector3 current;
    public static Vector3 current2;

    public int score1;
    public int score2;

    public int ammo;
    public int ammo2;

    public int winner;
    public bool transition;

    public float speed;

    public float timer = 0f;
    public float transitionTimer = 0f;

    static GameObject[,] grid = new GameObject[100, 100];
    

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
        ammo = 3;
        ammo2 = 3;
        speed = 0.3f;

        transition = false;
        bkgd.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if(transition)
        {
            bkgd.Stop();
            timer += Time.deltaTime;
            transitionTimer += Time.deltaTime;
            if (winner == 1 && timer >= 0.2f)
            {
                if ((grid[(int)current.x, (int)current.y].GetComponent<SpriteRenderer>().sprite == green))
                {
                    grid[(int)current.x, (int)current.y].GetComponent<SpriteRenderer>().sprite = black;
                }
                else
                {
                    grid[(int)current.x, (int)current.y].GetComponent<SpriteRenderer>().sprite = green;
                }
                timer = 0;
            }
            else if (winner == 2 && timer >= 0.2f)
            {
                if ((grid[(int)current2.x, (int)current2.y].GetComponent<SpriteRenderer>().sprite == green))
                {
                    grid[(int)current2.x, (int)current2.y].GetComponent<SpriteRenderer>().sprite = black;
                }
                else
                {
                    grid[(int)current2.x, (int)current2.y].GetComponent<SpriteRenderer>().sprite = green;
                }
                timer = 0;
            }

            if(transitionTimer >= 5f)
            {
                s2.gameObject.SetActive(false);
                s1.gameObject.SetActive(false);
                transitionTimer = 0f;
                timer = 0;
                transition = false;
                gameOver(winner);
            }

        }
        else
        {
            timer += Time.deltaTime;
            //Debug.Log(current);
            //Debug.Log(current2);
            if (timer >= speed)
            {
                timer = 0;
                current += direction;
                if (grid[(int)current.x, (int)current.y].GetComponent<SpriteRenderer>().sprite == green)
                {
                    s2.gameObject.SetActive(true);
                    s1.gameObject.SetActive(true);
                    score1++;
                    s1.text = score1.ToString();
                    transition = true;
                    winner = 1;
                    hit.Play();
                }
                current2 += direction2;
                if (grid[(int)current2.x, (int)current2.y].GetComponent<SpriteRenderer>().sprite == green)
                {
                    s2.gameObject.SetActive(true);
                    s1.gameObject.SetActive(true);
                    score2++;
                    s2.text = score2.ToString();
                    transition = true;
                    winner = 2;
                    if (!hit.isPlaying)
                        hit.Play();
                }
                grid[(int)current.x, (int)current.y].GetComponent<SpriteRenderer>().sprite = green;
                grid[(int)current2.x, (int)current2.y].GetComponent<SpriteRenderer>().sprite = green;
            }

            //Debug.Log(current.x);      

            //grid[(int) current.x, (int) current.y].GetComponent<SpriteRenderer>().sprite = green;

            if (Input.GetKeyDown("w"))
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

            if (Input.GetKeyDown("1"))
            {
                speed = 0.2f;
            }

            if (Input.GetKeyDown("2"))
            {
                speed = 0.3f;
            }

            if (Input.GetKeyDown("3"))
            {
                speed = 0.5f;
            }



            if (Input.GetKeyDown("z") && ammo > 0)
            {
                StartCoroutine(clearShot(direction, current));
                ammo--;
            }

            if (Input.GetKeyDown("right shift") && ammo2 > 0)
            {
                StartCoroutine(clearShot(direction2, current2));
                ammo--;
            }

        }


        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("BlockadeMenu", LoadSceneMode.Single);

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


    IEnumerator clearShot(Vector2 dir, Vector2 cur)
    {

        Debug.Log("shot");

        Vector2 c = cur;

        while(c.x != 99 && c.y != 99)
        {
            c += dir;

            Debug.Log(c);

            if (c.x >= 99 || c.y >= 99 || c.x <= 0 || c.y <= 0)
            {
                break;
            }


            if (grid[(int)c.x, (int)c.y].GetComponent<SpriteRenderer>().sprite == green && !c.Equals(current) && !c.Equals(current2)) {
                grid[(int)c.x, (int)c.y].GetComponent<SpriteRenderer>().sprite = black;
            }
            yield return null;
        }

    }
}
