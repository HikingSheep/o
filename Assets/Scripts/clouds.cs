using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class clouds : MonoBehaviour 
{                        
    public float spawnRate;
    public float columnMin;                                   
    public float columnMax;                                  

    public GameObject[] cloud;                                   
    private int current = 0;                                  

    private float timeSinceLastSpawned;


    void Start()
    {
        timeSinceLastSpawned = 0f;
    }


    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (timeSinceLastSpawned >= spawnRate) 
        {   
            timeSinceLastSpawned = 0f;

            float spawnZPosition = Random.Range(columnMin, columnMax);
			float spawnYPosition = Random.Range(this.transform.position.y, this.transform.position.y+5f);

            cloud[current].transform.position = new Vector3(this.transform.position.x,spawnYPosition, spawnZPosition);
			cloud[current].GetComponent<cloudAI>().reset();

            current++;

            if (current >= 5) 
            {
                current = 0;
            }
        }
    }
}