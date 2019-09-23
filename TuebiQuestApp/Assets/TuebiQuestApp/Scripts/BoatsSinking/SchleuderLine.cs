using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchleuderLine : MonoBehaviour {


    Vector3 InitPosition;
    internal float initScale;
    Vector3 target;
    
	void Start ()
    {
        InitPosition = transform.localPosition;
        target = InitPosition + Vector3.forward;
        initScale = transform.localScale.x;
    }
	
	void Update ()
    {
        transform.localPosition = InitPosition + target;
    }


    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
}
