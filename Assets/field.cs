using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class field : MonoBehaviour 
{
	public GameObject[] nodes;
	public static bool end;
	public static bool wait = true;
	public static int crapstack;


	void Update()
	{
		if(gameplay.re>=5)
		{
			gameplay.wave++;
			Reset();
		}
		if(end)
		{
			TotalReset();
			CancelInvoke();
		}
	}

	void Block()
	{
		int index = Random.Range(0,3);
		if(crapstack==10)
		{
			switch(index)
			{
				case 1:
					Boost();
					break;
				case 2:
					break;
				case 3:
					GoodChance();
					break;
			}			
		}
		else
		{
			switch(index)
			{
				case 1:
					Boost();
					break;
				case 2:
					crapstack++;
					BadChance();
					break;
				case 3:
					GoodChance();
					break;
			}
		}
	}
	public void StartPlaying()
	{
		wait = false;
		end = false;
		TotalReset();
		InvokeRepeating("Block",0,3f);
		for (int i = 0; i < 5; i++)
		{
			RandomCheck();
		}
	}
	void Play()
	{
		wait = false;
		for (int i = 0; i < 5; i++)
		{
			RandomCheck();
		}
	}

	void RandomCheck()
	{
		int newIndex = Random.Range(0,nodes.Length);
		int chance = Random.Range(0,4);
		if(crapstack<10)
		{
			if(nodes[newIndex].CompareTag("tower")||nodes[newIndex].CompareTag("chance")||nodes[newIndex].CompareTag("crap")||nodes[newIndex].CompareTag("halp"))
				{
					RandomCheck();
				}
			else
				{
					if(chance==2)
					{
						nodes[newIndex].tag = "chance";
					}
					else
					{
						nodes[newIndex].tag = "tower";
					}
					nodes[newIndex].transform.gameObject.GetComponent<ai>().moveUP = true;
				}
		}
		else
		{
			if(nodes[newIndex].CompareTag("tower")||nodes[newIndex].CompareTag("chance")||nodes[newIndex].CompareTag("halp"))
				{
					RandomCheck();
				}
			else
				{
					if(nodes[newIndex].CompareTag("crap"))
					{
						crapstack--;

						if(chance==2)
						{
							nodes[newIndex].tag = "chance";
						}
						else
						{
							nodes[newIndex].tag = "tower";
						}
					}
					else
					{
						if(chance==2)
						{
							nodes[newIndex].tag = "chance";
						}
						else
						{
							nodes[newIndex].tag = "tower";
						}	

						nodes[newIndex].transform.gameObject.GetComponent<ai>().moveUP = true;
					}
				}			
		}


	} 

	void Boost()
	{
		int newIndex = Random.Range(0,nodes.Length);
		if(nodes[newIndex].tag == "tower"||nodes[newIndex].tag =="chance"||nodes[newIndex].tag =="crap"||nodes[newIndex].tag =="halp")
		{
			Boost();
		}
		else
		{

			nodes[newIndex].tag = "chance";
		 	nodes[newIndex].transform.gameObject.GetComponent<ai>().moveUP = true;
		}
	}

	void BadChance()
	{
		int newIndex = Random.Range(0,nodes.Length);
		if(nodes[newIndex].tag == "tower"||nodes[newIndex].tag =="chance"||nodes[newIndex].tag =="crap"||nodes[newIndex].tag =="halp")
		{
			BadChance();
		}
		else
		{

			nodes[newIndex].tag = "crap";
		 	nodes[newIndex].transform.gameObject.GetComponent<ai>().moveUP = true;
		}
	}

	void GoodChance()
	{
		int newIndex = Random.Range(0,nodes.Length);
		if(nodes[newIndex].tag == "tower"||nodes[newIndex].tag =="chance"||nodes[newIndex].tag =="crap"||nodes[newIndex].tag =="halp")
		{
			GoodChance();
		}
		else
		{

			nodes[newIndex].tag = "halp";
		 	nodes[newIndex].transform.gameObject.GetComponent<ai>().moveUP = true;
		}
	}
	void TotalReset()
	{
		for (int i = 0; i < nodes.Length; i++)
		{
			nodes[i].tag = "node";
			nodes[i].GetComponent<ai>().move =false;
			nodes[i].GetComponent<ai>().moveUP =false;
			nodes[i].transform.position = new Vector3(nodes[i].transform.position.x, -23f+9.339844f,nodes[i].transform.position.z);
		}
	}
	void Reset()
	{
		StartCoroutine(delay());
	}

	IEnumerator delay()
	{
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < nodes.Length; i++)
		{
			if(nodes[i].CompareTag("tower"))
			{
				nodes[i].tag = "node";
				nodes[i].GetComponent<ai>().move =false;
				nodes[i].GetComponent<ai>().moveUP =false;
				nodes[i].transform.position = new Vector3(nodes[i].transform.position.x, -23f+9.339844f,nodes[i].transform.position.z);
			}
		}
		Play();
	}
}
