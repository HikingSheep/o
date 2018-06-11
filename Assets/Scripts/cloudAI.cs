using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class cloudAI : MonoBehaviour {
	
	private float speed;
	public float speedMin;
	public float speedMax;

	void Update () 
	{
		speed = Random.Range(speedMin, speedMax);
		this.transform.Translate(Vector3.right * Time.deltaTime * speed);
	}

	public void reset()
	{

		float value = Random.Range(0.15f, 0.2f);
		this.transform.localScale = new Vector3(value,value,value);
		if(gameplay.score >= 1500)
		{
			this.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)Random.Range(100,150));	
		}
		if(gameplay.score >= 3000)
		{
			this.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)Random.Range(150,200));	
		}
		else
		{
			this.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)Random.Range(50,100));	
		}
	}
}
