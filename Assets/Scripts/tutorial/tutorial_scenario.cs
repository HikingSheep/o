using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tutorial_scenario : MonoBehaviour
{

    public static int stage = 1;
    public GameObject[] test_block;
    public static bool timer_on;
    public GameObject score_display;
    public GameObject timer_display;
    public GameObject combo_display;
    public GameObject pointer;
    public GameObject[] text_pieces;

    public GameObject transparent;

    public static bool pause;
    public static bool showTimer;
    public static bool combo;
    public GameObject[] nodes;
    public static bool for_back;
	bool next = false;

    void Start()
    {
        timer_on = false;
        showTimer = false;
        combo = false;
        for_back = false;
        score_display.SetActive(false);
        timer_display.SetActive(false);
        combo_display.SetActive(false);
        pointer.SetActive(false);

		transparent.SetActive(false);

        foreach (GameObject i in text_pieces)
        {
            i.SetActive(false);
        }
    }

    void Update()
    {
        Debug.Log(stage);
        switch (stage)
        {
            case 0:
                //tutorial
                break;
            case 1:
                test_block[0].tag = "tower";
                test_block[0].GetComponent<cube_ai>().moveUP = true;
                stage = 2;
                break;
            case 2:
                if (test_block[0].tag == "node")
                {
                    score_display.SetActive(true);
                    pointer.SetActive(true);
                    pointer.GetComponent<Animator>().SetBool("end", true);
                    text_pieces[0].SetActive(true);
					transparent.SetActive(true);
                }
                break;
            case 3:
                //try getting points by pressing on greens
                break;
            case 4:
				text_pieces[1].SetActive(false);
                test_block[1].tag = "chance";
                test_block[2].tag = "chance2";
                foreach (GameObject i in nodes)
                {
                    if (i.tag == "tower")
                    {
                        i.GetComponent<cube_ai>().move = true;
                    }
                }
                test_block[1].GetComponent<cube_ai>().moveUP = true;
                test_block[2].GetComponent<cube_ai>().moveUP = true;
                stage = 5;
                break;
            case 5:
                //bonus points
                if (combo)
                {
					//arrow on
					text_pieces[2].SetActive(true);

					timer_display.SetActive(true);
                    combo_display.SetActive(true);
                }
                //timer
                if (timer_on)
                {
					//arrow on
					text_pieces[2].SetActive(true);

					timer_display.SetActive(true);
                }

				if(test_block[1].tag == "node"&& test_block[2].tag == "node")
				{
					stage = 6;
				}
                break;
            case 6:
                test_block[0].tag = "crap";
                test_block[0].GetComponent<cube_ai>().moveUP = true;
                break;
            case 7:
                break;
            case 8:
                score_display.SetActive(false);
                timer_display.SetActive(false);
                combo_display.SetActive(false);
                break;
        }

        if ((tutorial_gameplay.re >= 5) && stage < 4)
        {
            tutorial_gameplay.wave++;
            for (int i = 0; i < 5; i++)
            {
                GreensOnly();
            }
        }
        if (stage == 4 && tutorial_gameplay.score >= 15)
        {
            foreach (GameObject i in nodes) // back to greys
            {
                i.tag = "node";
                i.GetComponent<cube_ai>().move = false;
                i.GetComponent<cube_ai>().moveUP = false;
                i.transform.position = new Vector3(i.transform.position.x, -23f + 9.339844f, i.transform.position.z);
            }

            tutorial_gameplay.score = 0;
			tutorial_gameplay.re = 0;
        }

        if (showTimer && stage != 8)
        {
            timer_display.SetActive(true);
        }

        if (stage == 5 && (test_block[1].tag == "node" && test_block[2].tag == "node"))
        {
            stage = 6;
        }

		if(stage==3&&tutorial_gameplay.re == 5) //wave explained
		{
			text_pieces[1].GetComponent<Text>().text = "Every 5 blocks pressed, new 5 appear.";
		}

		if(combo||timer_on)
		{
			if(tutorial_gameplay.re == 2)
			{
				text_pieces[2].GetComponent<Text>().text = "Successfull yellow block grants a combo point. 3 Bonus points grants 100% chance for the bonus form the yellow block.";
			}
			if(tutorial_gameplay.re == 1)
			{
				text_pieces[2].GetComponent<Text>().text = "Yellow blocks have 50/50 chance. Either loose time or get a sweet bonus.";
			}
		}

    }

    public void next_stage()
    {
        switch (stage)
        {
            case 1:

                break;
            case 2:
                pointer.SetActive(false);

                text_pieces[0].GetComponent<Animator>().SetBool("end", true);

                text_pieces[0].SetActive(false);
                text_pieces[1].SetActive(true);
				stage++;
                break;
            case 3:
				transparent.SetActive(false);
				tutorial_gameplay.re = 0;
                for (int i = 0; i < 5; i++)
                {
                    GreensOnly();
                }
                break;
            case 6:

                break;
        }
    }

    void GreensOnly()
    {
        int newIndex = Random.Range(0, nodes.Length);
        if (nodes[newIndex].CompareTag("tower"))
        {
            GreensOnly();
        }
        else
        {
            nodes[newIndex].tag = "tower";
            nodes[newIndex].transform.gameObject.GetComponent<cube_ai>().moveUP = true;
        }
    }

}
