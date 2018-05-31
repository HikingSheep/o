using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cube_ai : MonoBehaviour {

	public bool move = false;
	public bool moveUP = false;
	private Vector3 targetDOWN;
	private Vector3 targetUP;
	private GameObject mark;
	private GameObject nodeMark;
	public Sprite[] marks;
	public GameObject pop;

	private GameObject visuals;
	public Sprite[] numbers;
	Image visual;
	void Awake()
	{
		mark = this.transform.GetChild(0).gameObject;
		nodeMark = mark.transform.GetChild(0).gameObject;
		visuals = mark.transform.GetChild(1).gameObject;
		targetDOWN = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
		targetUP = new Vector3(this.transform.position.x,this.transform.position.y+1f,this.transform.position.z);
		nodeMark.GetComponent<SpriteRenderer>().sprite = marks[3]; 
	}

	void Start()
	{
		visual = visuals.GetComponent<Image>();
		visual.enabled = false;
	}

	void FixedUpdate () 
	{
		if(this.CompareTag("tower"))
		{
			nodeMark.GetComponent<SpriteRenderer>().sprite = marks[0];  
		}
		if(this.CompareTag("chance")||this.CompareTag("chance2"))
		{
			nodeMark.GetComponent<SpriteRenderer>().sprite = marks[1]; 
		}
		if(this.CompareTag("crap"))
		{
			nodeMark.GetComponent<SpriteRenderer>().sprite = marks[2]; 
		}
		if(this.CompareTag("node")&&this.transform.position==targetUP)
		{
			move = true;
		}
		if(move)
		{
			this.GetComponent<Collider>().enabled = false;
			this.transform.position = Vector3.Lerp(this.transform.position,targetDOWN,0.5f);
			if(this.transform.position==targetDOWN)
			{
				this.tag="node";
				GameObject Pop = (GameObject)Instantiate(pop, nodeMark.transform.position, Quaternion.identity);
				move = false;
				this.GetComponent<Collider>().enabled = true;

				Destroy(Pop, 1f);
			}
		}
		if(moveUP)
		{
			visual.enabled = false;
			this.GetComponent<Collider>().enabled = false;
			this.transform.position = Vector3.Lerp(this.transform.position,targetUP,0.25f);
			if(this.transform.position==targetUP)
			{
				moveUP = false;
				this.GetComponent<Collider>().enabled = true;
			}
		}
		if(move)
		{
			moveUP=false;
		}
		if(moveUP)
		{
			move = false;
		}
	}

	void Update()
	{
		if(this.CompareTag("node"))
		{
			nodeMark.GetComponent<SpriteRenderer>().sprite = marks[3];   
			visual.enabled = false;
		}
	}

	public IEnumerator show(int value)
	{
		visual.enabled = true;
		visual.sprite = numbers[value];
		yield return new WaitForSeconds(1f);
		visual.enabled = false;
	}

	public void showVis(int value)
	{
		visual.enabled = true;
		visual.sprite = numbers[value];
	}
}
