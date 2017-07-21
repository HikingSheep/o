using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialManager : MonoBehaviour 
{
	public GameObject[] blocks;
	public GameObject[] buttons;
	public SpriteRenderer[] marks;
	public GameObject buttonGroup;
	public Material[] ForMark;
	public GameObject[] pages;
	public int page = 1;
	private bool down = false;
	private bool up = false;
	public static bool learn = false;
	void Awake()
	{
		foreach (GameObject i in pages)
		{
			i.SetActive(true);
		}
		buttonGroup.SetActive(true);
		buttons[0].SetActive(false);
		buttons[1].SetActive(true);
		PageSwitch();
	}
	public void NextPage()
	{	
		page++;
		PageSwitch();
	}
	public void PrevPage()
	{
		page--;
		PageSwitch();
	}
	public void Up()
	{
		Vector3 target = new Vector3(blocks[0].transform.position.x,blocks[0].transform.position.y+1f,blocks[0].transform.position.z);
		blocks[0].transform.position = Vector3.Lerp(blocks[0].transform.position,target,0.1f);
	}
	void FixedUpdate()
	{
		if(down)
		{
			foreach (SpriteRenderer i in marks)
			{
				i.material = ForMark[3];
			}
			foreach (GameObject i in blocks)
			{
				if(i.transform.position.y>-20f)
				{
					Vector3 target = new Vector3(i.transform.position.x,i.transform.position.y-1f,i.transform.position.z);
					i.transform.position = Vector3.Lerp(i.transform.position,target,0.5f);
				}
				else
				{
					down = false;	
					return;
				}
			}
		}
		if(up)
		{
			marks[0].material = ForMark[0];
			marks[1].material = ForMark[1];
			marks[2].material = ForMark[2];
			foreach (GameObject i in blocks)
			{
				if(i.transform.position.y<-15f)
				{
					Vector3 target = new Vector3(i.transform.position.x,i.transform.position.y+1f,i.transform.position.z);
					i.transform.position = Vector3.Lerp(i.transform.position,target,0.5f);
				}
				else
				{
					up = false;
					return;
				}
			}
		}
		if(!up&&!down)
		{
			buttonGroup.SetActive(true);
		}
		else
		{
			buttonGroup.SetActive(false);	
		}
	}
	void PageSwitch()
	{
		switch(page)
		{
			case 1:
				PageOne();
				break;
			case 2:
				PageTwo();
				break;
			case 3:
				PageThree();
				break;
			case 4:
				PageFour();
				break;
			case 5:
				PageFive();
				break;
			case 6:
				PageSix();
				break;
			case 7:
				PageSeven();
				break;
			case 8:
				PageEight();
				break;
		}
	}
	void PageOne()
	{
		Debug.Log(page);
		buttons[0].SetActive(false);
		buttons[1].SetActive(true);
		up=true;
	}
	void PageTwo()
	{
		Debug.Log(page);
		buttons[0].SetActive(true);
		buttons[1].SetActive(true);

		down = true;
	}
	void PageThree()
	{
		Debug.Log(page);
		buttons[0].SetActive(true);
		buttons[1].SetActive(true);
		up=true;
	}
	void PageFour()
	{
		Debug.Log(page);
		buttons[0].SetActive(true);
		buttons[1].SetActive(true);
		down = true;
	}
	void PageFive()
	{
		Debug.Log(page);
		buttons[0].SetActive(true);
		buttons[1].SetActive(true);
		down = true;
	}
	void PageSix()
	{
		Debug.Log(page);
		buttons[0].SetActive(true);
		buttons[1].SetActive(true);
		down = true;
	}
	void PageSeven()
	{
		Debug.Log(page);
		buttons[0].SetActive(true);
		buttons[1].SetActive(true);
		down = true;
	}
	void PageEight()
	{
		Debug.Log(page);
		buttons[0].SetActive(true);
		buttons[1].SetActive(false);
		up = true;
	}

	public void Play()
	{
		learn = true;
		PlayerPrefs.SetString("tutorial","true");
		SceneManager.LoadScene(1);
	}

	public void Menu()
	{
		learn = true;
		PlayerPrefs.SetString("tutorial","true");
		SceneManager.LoadScene(0);
	}
}
