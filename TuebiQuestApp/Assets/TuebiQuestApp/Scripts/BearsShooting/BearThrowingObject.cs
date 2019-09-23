using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearThrowingObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 10f);
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 0.5f);
    }

}
