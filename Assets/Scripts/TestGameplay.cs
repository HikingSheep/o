using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class TestGameplay : MonoBehaviour 
{
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

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonUp(0))
        {    

			if(IsPointerOverUIObject())
			{
				return;
			}

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.CompareTag("node"))
                {
                }				
                if (hit.collider.CompareTag("tower")||hit.collider.CompareTag("halp"))
                {
					hit.transform.gameObject.GetComponent<ai>().move = true;
                }
				if (hit.collider.CompareTag("chance"))
                {
					int index = Random.Range(0,2);
					if(index==1)
						{
							hit.transform.gameObject.GetComponent<ai>().move = true;
						}
						else
						{
							hit.collider.tag = "tower";
						}
                }
				if(hit.collider.CompareTag("crap"))
				{
					int index = Random.Range(0,10);
					if(index==1)
					{
						hit.collider.tag = "tower";
					}	
					else
					{
						hit.transform.gameObject.GetComponent<ai>().move = true;
					}
				}
            }
        }
	}
}
