using UnityEngine;

public class TestField : MonoBehaviour 
{
	public GameObject node;

	public void RandomCheck()
	{
		node.tag = "tower";
		node.transform.gameObject.GetComponent<ai>().moveUP = true;
	} 

	public void Boost()
	{
		node.tag = "chance";
		node.transform.gameObject.GetComponent<ai>().moveUP = true;
	}

	public void BadChance()
	{
		node.tag = "crap";
		node.transform.gameObject.GetComponent<ai>().moveUP = true;
	}
}
