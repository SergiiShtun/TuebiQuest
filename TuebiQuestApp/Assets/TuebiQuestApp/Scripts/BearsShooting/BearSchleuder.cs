using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSchleuder : MonoBehaviour {

    public GameObject Meat;
    public GameObject Honey;
    public float mouseDistanceFactor;
    public float maxForce;

    Vector3 MousePosition;
    Vector3 OldMousePosition;

    private Vector3 ForceVector;
    


    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            OldMousePosition = Input.mousePosition;
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            MousePosition = Input.mousePosition;

            ForceVector = Vector3.ClampMagnitude((MousePosition - OldMousePosition) / mouseDistanceFactor, maxForce);

            Debug.DrawLine(transform.position, transform.position - (ForceVector / 100f));
        }

        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            Rigidbody throwRb = Instantiate(Random.Range(0, 4) > 0 ? Meat : Honey, transform.position, transform.rotation).GetComponent<Rigidbody>();
            ForceVector = -ForceVector;
            ForceVector.z = ForceVector.y;
            //print(ForceVector);
            throwRb.AddForce(ForceVector);
        }
    }
}
