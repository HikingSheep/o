using UnityEngine;
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

        if (score >= 15 && tutorial_scenario.stage!=6 && tutorial_scenario.stage!=7)
        {
            score = 0;
            tutorial_scenario.stage = 4;
        }

        if (tutorial_scenario.timer_on)
        {
            if (!tutorial_scenario.pause)
            {
                timer -= Time.deltaTime;
            }

            if (timer <= 0)
            {
                timer = 0;
                field.end = true;
                ui.MainMenu.SetActive(true);
            }
        }

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
                if (hit.collider.CompareTag("tower") || hit.collider.CompareTag("halp"))
                {
                    if (tutorial_scenario.score_on)
                    {
                        score++;
                        re++;
                    }
                    stone.Play();
                    hit.transform.gameObject.GetComponent<cube_ai>().move = true;
                    hit.transform.gameObject.GetComponent<cube_ai>().showVis(0);
                }
                if (tutorial_scenario.stage == 5)
                {
                    if (hit.collider.CompareTag("chance"))
                    {
                        tutorial_scenario.showTimer = true;
                        timer = timer - 5f;
                        stone.Play();
                        hit.transform.gameObject.GetComponent<cube_ai>().move = true;
                        hit.transform.gameObject.GetComponent<cube_ai>().showVis(2);
                    }
                    if (hit.collider.CompareTag("chance2"))
                    {
                        tutorial_scenario.showTimer = true;
                        tutorial_scenario.combo = true;
                        tap.Play();
                        combo++;
                        DisplayCombo();
                        timer = timer + 5f;
                        score = score + 10;
                        hit.collider.tag = "tower";
                        StartCoroutine(hit.transform.gameObject.GetComponent<cube_ai>().show(1));
                    }
                }

                if (hit.collider.CompareTag("crap"))
                {
                    timer = timer - 10f;
                    score = score + 100;
                    stone.Play();
                    hit.transform.gameObject.GetComponent<cube_ai>().move = true;
                    hit.transform.gameObject.GetComponent<cube_ai>().showVis(3);
                    hit.transform.gameObject.GetComponent<cube_ai>().moveUP = false;
                    tutorial_scenario.stage=7;
                    tutorial_scenario.for_back = false;
                }
            }
        }
    }

    void DisplayCombo()
    {
        switch (combo)
        {
            case 0:
                comboDisplay.SetTrigger("0");
                break;
            case 1:
                comboDisplay.SetTrigger("1");
                break;
            case 2:
                comboDisplay.SetTrigger("2");
                break;
            case 3:
                comboDisplay.SetTrigger("3");
                break;
        }
    }
}
