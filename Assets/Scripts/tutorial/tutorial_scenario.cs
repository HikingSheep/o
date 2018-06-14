using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class tutorial_scenario : MonoBehaviour
{

    public static int stage = 0;
    public GameObject[] test_block;
    public static bool timer_on;
    public GameObject score_display;
    public GameObject timer_display;
    public GameObject combo_display;
    public GameObject pointer;
    public GameObject pointer2;
    public GameObject pointer3;
    public GameObject[] text_pieces;
    public GameObject transparent;
    public GameObject sidepanel;
    public GameObject[] nodes;
    public GameObject AnyKey;
    public GameObject AnyKey2;
    public GameObject blockIntro;
    public GameObject nameTutu;

    public static bool showTimer;
    public static bool combo;
    public static bool for_back;


    public GameObject playButton;
    public GameObject menuButton;

    void Start()
    {
        Debug.Log("run");
        stage = 0;
        timer_on = false;
        showTimer = false;
        combo = false;
        for_back = false;

        score_display.SetActive(false);
        timer_display.SetActive(false);
        combo_display.SetActive(false);
        pointer.SetActive(false);
        pointer2.SetActive(false);
        pointer3.SetActive(false);
        sidepanel.SetActive(false);
		transparent.SetActive(false);
        AnyKey.SetActive(false);
        AnyKey2.SetActive(true);

        playButton.SetActive(false);
        menuButton.SetActive(false);

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
                transparent.SetActive(true);
                break;
            case 1:
                AnyKey2.SetActive(false);
                transparent.SetActive(false);
                blockIntro.SetActive(false);
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
                    Camera.main.GetComponent<Animator>().enabled = false;
                    nameTutu.SetActive(false);
                }
                break;
            case 3:
                sidepanel.SetActive(true);
                //try getting points by pressing on greens
                break;
            case 4:
                sidepanel.SetActive(false);
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
					text_pieces[2].SetActive(true);

					timer_display.SetActive(true);
                    combo_display.SetActive(true);
                }
                //timer
                if (timer_on)
                {
					text_pieces[2].SetActive(true);

					timer_display.SetActive(true);
                }

				if(test_block[1].tag == "node"&& test_block[2].tag == "node")
				{
                    pointer2.SetActive(false);
                    pointer3.SetActive(false);
                    tutorial_gameplay.score = 0;
					stage = 6;
				}
                break;
            case 6:
                pointer2.SetActive(false);
                pointer3.SetActive(false);
                combo_display.SetActive(false);
                test_block[0].tag = "crap";
                test_block[0].GetComponent<cube_ai>().moveUP = true;
                text_pieces[2].SetActive(false);
                break;
            case 7:
                text_pieces[3].SetActive(true);
                transparent.SetActive(true);
                break;
            case 8:
                score_display.SetActive(false);
                timer_display.SetActive(false);
                combo_display.SetActive(false);

                playButton.SetActive(true);
                menuButton.SetActive(true);
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

		if(combo||timer_on) //yellow block text
		{
			if(tutorial_gameplay.re == 2)
			{
                pointer2.SetActive(true);
                pointer3.SetActive(false);
				text_pieces[2].GetComponent<Text>().text = "Successfull yellow block grants a combo point. 3 Bonus points grants 100% chance for the bonus form the yellow block (10 points & 5 seconds).";
			}
			if(tutorial_gameplay.re == 1)
			{
                pointer3.SetActive(true);
                pointer2.SetActive(false);
				text_pieces[2].GetComponent<Text>().text = "Yellow blocks have 50/50 chance. Either loose time or get a sweet bonus.";
			}
		}

        if(Input.anyKey&&transparent.activeInHierarchy)
        {
            next_stage();
        }

        if(transparent.activeInHierarchy)
        {
            AnyKey.SetActive(true);
        }
        else
        {
            AnyKey.SetActive(false);
        }
        
    }

    public void next_stage()
    {
        switch (stage)
        {
            case 0:
                Camera.main.GetComponent<Animator>().SetTrigger("move");
                nameTutu.GetComponent<Animator>().SetTrigger("fade");
                stage=1;
                break;
            case 1:
                break;
            case 2:
                pointer.SetActive(false);

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
            case 7:
                text_pieces[3].SetActive(false);
                transparent.SetActive(false);
                stage++;
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

	public void Play()
	{
		PlayerPrefs.SetString("tutorial","true");

        tutorial_gameplay.re = 0;
        stage = 0;
        tutorial_gameplay.score = 0;

        timer_on = false;
        showTimer = false;
        combo = false;
        for_back = false;

		SceneManager.LoadScene(1);
	}

    public void Menu()
    {
        PlayerPrefs.SetString("tutorial","true");

        tutorial_gameplay.re = 0;
        stage = 0;
        tutorial_gameplay.score = 0;

        timer_on = false;
        showTimer = false;
        combo = false;
        for_back = false;

		SceneManager.LoadScene(0);
    }

}
