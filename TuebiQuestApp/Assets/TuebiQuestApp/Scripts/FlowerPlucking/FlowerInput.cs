using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerInput : MonoBehaviour {




	void Start ()
    {
		
	}
	

	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.GetComponent<Flower>() != null)
                    hit.collider.GetComponent<Flower>().Cut();
            }
        }
        else if(Input.touchCount > 0)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                    hit.collider.GetComponent<Flower>().Cut();
            }
        }
	}
}
