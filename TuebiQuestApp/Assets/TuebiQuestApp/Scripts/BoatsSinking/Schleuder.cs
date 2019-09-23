using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schleuder : MonoBehaviour {

    public GameObject Rock;
    public float mouseDistanceFactor;
    public float maxForce;

    Vector3 MousePosition;
    Vector3 OldMousePosition;

    private Vector3 ForceVector;

    private SchleuderLine[] sLines;

    void Start()
    {
        sLines = GetComponentsInChildren<SchleuderLine>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            OldMousePosition = Input.mousePosition;
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            MousePosition = Input.mousePosition;

            ForceVector = Vector3.ClampMagnitude((MousePosition - OldMousePosition) / mouseDistanceFactor, maxForce);

            Debug.DrawLine(transform.position, transform.position - (ForceVector / 100f));

            foreach (SchleuderLine sl in sLines)
                sl.SetTarget(Vector3.back * ForceVector.magnitude / 100f);
        }

        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            Rigidbody rockRb = Instantiate(Rock, transform.position, transform.rotation).GetComponent<Rigidbody>();
            ForceVector = -ForceVector;
            ForceVector.z = ForceVector.y;
            //print(ForceVector);
            rockRb.AddForce(ForceVector);
            foreach (SchleuderLine sl in sLines)
                sl.SetTarget(Vector3.zero);
        }
    }
}
