using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class idle : MonoBehaviour 
{
	public static bool mute = false;
	public Text high;
	public Sprite[] volume;
	public Image sound;
	void Awake()
	{	
		Time.timeScale = 1;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		if(PlayerPrefs.GetInt("score")<100)
		{
			high.fontSize = 200;
		}
		else
		{
			high.fontSize = 150;
		}
		high.text=PlayerPrefs.GetInt("score").ToString();

		if(mute)
		{
			sound.sprite = volume[0];
			AudioListener.volume = 0;
		}
		else
		{
			sound.sprite = volume[1];
			AudioListener.volume = 1;
		}
		if(PlayerPrefs.HasKey("tutorial"))
		{
			tutorialManager.learn = true;
		}
	}
	public void Play()
	{
		if(!tutorialManager.learn)
		{
			SceneManager.LoadScene(2);
		}
		else
		{
			SceneManager.LoadScene(1);
		}
	}
	public void Sound()
	{
		if(!mute)
		{
			mute = true;
			sound.sprite = volume[0];
			AudioListener.volume = 0;
		}
		else
		{
			mute = false;
			sound.sprite = volume[1];
			AudioListener.volume = 1;
		}
	}

	public void Tutorial()
	{
		SceneManager.LoadScene(2);
	}
}
