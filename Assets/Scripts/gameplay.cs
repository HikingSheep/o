using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class gameplay : MonoBehaviour 
{
	public static int re;
	public float timer;
	public Text time;
	public Text ForScore;
	public Text ForCombo;
	public Animator comboDisplay;
	private AudioSource tap;
	public AudioClip[] taps;
	public AudioSource stone;
	public static int score;
	private int bonus;
	public static int wave;
	public int combo;
	private ui ui;
	void Awake()
	{
		ui = FindObjectOfType<ui>();
		tap = this.GetComponent<AudioSource>();
	}
	private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

	void Update()
	{
		if(!field.wait)
		{
			timer -= Time.deltaTime;
		}
		time.text = timer.ToString("00");

		ForScore.text = score.ToString();
		ForCombo.text = combo.ToString();

		if(timer <= 0)
		{
			timer = 0;
			field.end = true;
			ui.MainMenu.SetActive(true);
		}
		if(timer > 0 && !field.wait)
		{
			DisplayCombo();
		}
		if(timer > 60)
		{
			timer = 60;
		}
		if(re==5)
		{
			re=0;
			timer++;
		}
		if(score <=0)
		{
			score = 0;
		}
		if(bonus==10)
		{
			if(combo<3)
			{
				combo++;
			}
			bonus = 0;
		}
		if(score<100)
		{
			ForScore.fontSize = 2;
		}
		if(score>=100)
		{
			ForScore.fontSize = 1;
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonUp(0))
        {    

			if(IsPointerOverUIObject())
			{
				return;
			}

            if (Physics.Raycast(ray, out hit, 100))
            {
				int tapIndex = Random.Range(0,taps.Length);
				tap.clip = taps[tapIndex];
                if (hit.collider.CompareTag("node"))
                {
					tap.Play();
                }				
                if (hit.collider.CompareTag("tower")||hit.collider.CompareTag("halp"))
                {
					re++;
					score++;
					bonus++;
					stone.Play();
					hit.transform.gameObject.GetComponent<ai>().move = true;
					hit.transform.gameObject.GetComponent<ai>().show(0);
                }
				if (hit.collider.CompareTag("chance"))
                {
					if(combo>=3)
					{
						StartCoroutine(hit.transform.gameObject.GetComponent<ai>().showR(1));
						tap.Play();
						combo = 0;
						timer = timer + 5f;
						score = score + 10;
						hit.collider.tag = "tower";
					}
					else
					{
						int index = Random.Range(0,2);
						if(index==1)
						{
							re++;
							timer = timer - 5f;
							stone.Play();
							hit.transform.gameObject.GetComponent<ai>().move = true;
							hit.transform.gameObject.GetComponent<ai>().show(2);
						}
						else
						{
							if(combo>=3)
							{
								combo = 0;
							}
							StartCoroutine(hit.transform.gameObject.GetComponent<ai>().showR(1));
							tap.Play();
							combo++;
							timer = timer + 5f;
							score = score + 10;
							hit.collider.tag = "tower";
						}
					}
                }
				if(hit.collider.CompareTag("crap"))
				{
					field.crapstack--;
					re++;
					timer = timer - 10f;
					score = score + 100;
					stone.Play();
					hit.transform.gameObject.GetComponent<ai>().move = true;
					hit.transform.gameObject.GetComponent<ai>().show(3);
				}
            }
        }
	}
	void DisplayCombo()
	{
		if(comboDisplay.gameObject.activeSelf == true)
		{
			switch(combo)
			{
				case 0:
					comboDisplay.SetBool("1",false);
					comboDisplay.SetBool("2",false);
					comboDisplay.SetBool("3",false);
					comboDisplay.SetBool("0",true);
					break;			
				case 1:
					comboDisplay.SetBool("1",true);
					break;
				case 2:
					comboDisplay.SetBool("2",true);
					break;
				case 3:
					comboDisplay.SetBool("3",true);
					comboDisplay.SetBool("1",false);
					comboDisplay.SetBool("2",false);
					comboDisplay.SetBool("0",false);
					break;
			}
		}
		else
		{
			return;
		}
	}
	public void Play()
	{
		re = 0;
		combo = 0;
		timer = 30f;
		score = 0;
		wave = 0;
		bonus = 0;
		field.crapstack = 0;
		comboDisplay.SetTrigger("0");
	}
}
