﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class tutorial_gameplay : MonoBehaviour
{

    public float timer = 30f;
    public Text time;
    public Text ForScore;
    public Text ForCombo;
    public Animator comboDisplay;
    private AudioSource tap;
    public AudioClip[] taps;
    public AudioSource stone;
    public static int score;
    public static int re;
    public static int wave;
    public int combo;

    public GameObject sidepanel;


    void Awake()
    {
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
        time.text = timer.ToString("00");
        ForScore.text = score.ToString();
        ForCombo.text = combo.ToString();

        if (score <= 0)
        {
            score = 0;
        }

        if (score < 100)
        {
            ForScore.fontSize = 2;
        }

        if (score >= 100)
        {
            ForScore.fontSize = 1;
        }

        if (re == 5)
        {
            re = 0;
        }

        if (score >= 15 && tutorial_scenario.stage!=6 && tutorial_scenario.stage!=7 && tutorial_scenario.stage!=8)
        {
            score = 0;
            sidepanel.GetComponent<Animator>().SetTrigger("close");
            tutorial_scenario.stage = 4;
        }

        sidepanel.GetComponentInChildren<Text>().text = score.ToString("0");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonUp(0))
        {

            if (IsPointerOverUIObject())
            {
                return;
            }

            if (Physics.Raycast(ray, out hit, 100))
            {
                int tapIndex = Random.Range(0, taps.Length);
                tap.clip = taps[tapIndex];
                if (hit.collider.CompareTag("node"))
                {
                    tap.Play();
                }
                if (hit.collider.CompareTag("tower"))
                {
                    score++;
                    re++;
                    stone.Play();
                    hit.transform.gameObject.GetComponent<cube_ai>().move = true;
                    hit.transform.gameObject.GetComponent<cube_ai>().showVis(0);
                }
                if (tutorial_scenario.stage == 5)
                {
                    if (hit.collider.CompareTag("chance"))
                    {
                        re=1;
                        tutorial_scenario.timer_on = true;
                        timer = timer - 5f;
                        stone.Play();
                        hit.transform.gameObject.GetComponent<cube_ai>().move = true;
                        hit.transform.gameObject.GetComponent<cube_ai>().showVis(2);
                    }
                    if (hit.collider.CompareTag("chance2"))
                    {
                        re=2;
                        tutorial_scenario.combo = true;
                        tap.Play();
                        combo++;
                        timer = timer + 5f;
                        score = score + 10;
                        hit.collider.tag = "tower";
                        StartCoroutine(hit.transform.gameObject.GetComponent<cube_ai>().show(1));
                    }
                }

                if (hit.collider.CompareTag("crap"))
                {
                    tutorial_scenario.stage++;
                    timer = timer - 10f;
                    score = score + 100;
                    stone.Play();
                    hit.transform.gameObject.GetComponent<cube_ai>().move = true;
                    hit.transform.gameObject.GetComponent<cube_ai>().showVis(3);
                }
            }
        }
    }   
}
