using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using System.Collections;

public class ui : MonoBehaviour {

	private bool pause = false;
	public bool blockdown = false;
	public bool blockup = false;
	private field field;
	private gameplay gameplay;
	public GameObject MainMenu;
	public GameObject PauseMenu;
	public GameObject PauseBlock;
	public GameObject gameplayUI;
	public Text display;
	public Text displayBest;
	public string[] tips;
	public Text ForTips;
	void Awake () 
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		field = FindObjectOfType<field>();
		gameplay = FindObjectOfType<gameplay>();
		PauseMenu.SetActive(false);
		gameplayUI.SetActive(false);
		Play();
		Time.timeScale = 1f;

		if(idle.mute)
		{
			AudioListener.volume = 0;
		}
		else
		{
			AudioListener.volume = 1;
		}

		InvokeRepeating("Tip",1f,10f);
	}
	void FixedUpdate() 
	{
		if(blockdown)
		{
			PauseBlock.transform.Translate(0,0,-0.5f);
			blockdown = false;
			PauseMenu.SetActive(true);
			Time.timeScale = 0.1f;
		}
		if(blockup)
		{
			PauseBlock.transform.Translate(0,0,+0.5f);
			blockup = false;
			PauseMenu.SetActive(false);
			Time.timeScale = 1f;
		}

		if(field.end)
		{
			display.text = gameplay.score.ToString();
			if(PlayerPrefs.HasKey("score"))
			{
				int high = PlayerPrefs.GetInt("score");
				if(high<gameplay.score)
				{
					PlayerPrefs.SetInt("score",gameplay.score);
				}
				else
				{
					displayBest.text = "Best: " + high.ToString();
				}
			}
			else
			{
				PlayerPrefs.SetInt("score",gameplay.score);				
			}
			gameplayUI.SetActive(false);	
		}
	}

	public void Pause()
	{
		if(!pause)
		{
			pause = true;
			blockdown = true;
		}
		else
		{
			pause = false;
			blockup = true;
		}
	}

	public void Play()
	{
		StartCoroutine(PlayButton());
	}

	public void Home()
	{
		SceneManager.LoadScene(0);
	}



	int last = 0;
	void Tip()
	{
		if(field.end)
		{
			ForTips.text ="";
		}
		else
		{
			int index = Random.Range(0,tips.Length);
			if(index==last)
			{
				Tip();
			}
			else
			{
				last = index;
				ForTips.text = "Tip: " + tips[index];
			}
		}
	}

	IEnumerator PlayButton()
	{
		yield return new WaitForSecondsRealtime(0.2f);
		MainMenu.SetActive(false);
		gameplayUI.SetActive(true);
		field.StartPlaying();
		gameplay.Play();
	}
}
