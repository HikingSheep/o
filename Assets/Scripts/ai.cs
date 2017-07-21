using UnityEngine;

public class ai : MonoBehaviour {

	public bool move = false;
	public bool moveUP = false;
	private Vector3 targetDOWN;
	private Vector3 targetUP;
	private GameObject mark;
	private GameObject nodeMark;
	public Material[] marks;
	void Awake()
	{
		mark = this.transform.GetChild(0).gameObject;
		nodeMark = mark.transform.GetChild(0).gameObject;
		targetDOWN = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
		targetUP = new Vector3(this.transform.position.x,this.transform.position.y+1f,this.transform.position.z);
		nodeMark.GetComponent<Renderer>().material = marks[3]; 
	}
	void FixedUpdate () 
	{
		if(this.CompareTag("tower")||this.CompareTag("halp"))
		{
			nodeMark.GetComponent<Renderer>().material = marks[0]; 
		}
		if(this.CompareTag("chance"))
		{
			nodeMark.GetComponent<Renderer>().material = marks[1]; 
		}
		if(this.CompareTag("crap"))
		{
			nodeMark.GetComponent<Renderer>().material = marks[2]; 
		}
		if(move)
		{
			this.GetComponent<Collider>().enabled = false;
			this.transform.position = Vector3.Lerp(this.transform.position,targetDOWN,0.25f);
			if(this.transform.position==targetDOWN)
			{
				this.tag="node";
				move = false;
				this.GetComponent<Collider>().enabled = true;
			}
		}
		if(moveUP)
		{
			this.GetComponent<Collider>().enabled = false;
			this.transform.position = Vector3.Lerp(this.transform.position,targetUP,0.25f);
			if(this.transform.position==targetUP)
			{
				moveUP = false;
				this.GetComponent<Collider>().enabled = true;
			}
		}
	}

	void Update()
	{
		if(field.end||this.CompareTag("node"))
		{
			nodeMark.GetComponent<Renderer>().material = marks[3]; 
		}
	}
}
