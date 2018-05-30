using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tutorial_scenario : MonoBehaviour {

	public static int stage = 1;
	public GameObject[] test_block;
	public static bool timer_on;
	public static bool score_on;
	public GameObject dark_background;
	public GameObject dark_background2;
	public GameObject score_display;
	public GameObject timer_display;
	public GameObject combo_display;
	public GameObject pointer;
	public GameObject[] text_pieces;

	public static bool pause;
	public static bool showTimer;
	public static bool combo;
	public GameObject[] nodes;

	public bool forWave = true;

	void Start () 
	{
		timer_on = false;
		score_on = false;
		showTimer = false;
		combo = false;
		score_display.SetActive(false);
		timer_display.SetActive(false);
		combo_display.SetActive(false);
		dark_background.SetActive(false);
		dark_background2.SetActive(false);
		pointer.SetActive(false);

		foreach (GameObject i in text_pieces)
		{
			i.SetActive(false);
		}
	}
	
	void Update () 
	{	
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
				if(test_block[0].tag == "node")
				{
					score_on = true;
					score_display.SetActive(true);
					tutorial_gameplay.score = 1;
					dark_background.SetActive(true);
					pointer.SetActive(true);
					pointer.GetComponent<Animator>().SetBool("end",true);
					text_pieces[0].SetActive(true);
				}
			break;
			case 3:
				//try getting points by pressing on greens
			break;
			case 4:
				foreach (GameObject i in nodes)
				{
					if(i.CompareTag("tower"))
					{
						i.GetComponent<cube_ai>().move = true;
					}
				}
				test_block[1].tag = "chance";
				test_block[2].tag = "chance2";
				test_block[1].GetComponent<cube_ai>().moveUP = true;
				test_block[2].GetComponent<cube_ai>().moveUP = true;
				stage = 5;
			break;
			case 5:
				//bonus points
				if(combo)
				{
					dark_background2.SetActive(true);
					combo_display.SetActive(true);
				}
			break;
			case 6:
				//red + timer (-10s)
			break;
		}

		if((tutorial_gameplay.re>=5)&&stage<4)
		{
			tutorial_gameplay.wave++;
			for (int i = 0; i < 5; i++)
			{
				GreensOnly();	
			}
		}
		if(stage==4&&tutorial_gameplay.score>=15)
		{
			Debug.Log("run");
			foreach (GameObject i in nodes)
			{
				i.tag = "node";
				i.GetComponent<cube_ai>().move =false;
				i.GetComponent<cube_ai>().moveUP =false;
				i.transform.position = new Vector3(i.transform.position.x, -23f+9.339844f,i.transform.position.z);
			}
			tutorial_gameplay.score = 0;
		}

		if(tutorial_gameplay.score == 6&&forWave)
		{
			text_pieces[2].SetActive(true);
		}

		if(showTimer)
		{
			timer_display.SetActive(true);
		}
	}

	public void bg()
	{
		if(stage == 2)
		{
            pointer.SetActive(false);

            text_pieces[0].GetComponent<Animator>().SetBool("end", true);

            text_pieces[0].SetActive(false);
            text_pieces[1].SetActive(true);

            stage++;
		}
		else
		{
			Debug.Log("stage "+stage+" started");
			dark_background.SetActive(false);
			text_pieces[1].SetActive(false);
			for (int i = 0; i < 5; i++)
			{
				GreensOnly();	
			}
		}
	}

	public void ComboBG()
	{
		Debug.Log("test");
		dark_background2.SetActive(false);
	}

	public void wave_explained()
	{
		forWave = false;
		text_pieces[2].SetActive(false);
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
