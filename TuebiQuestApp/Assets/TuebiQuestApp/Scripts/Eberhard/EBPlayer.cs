using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBPlayer : MonoBehaviour {

    public GameObject Ziegel;
    public float velocityMultiplier = 7;
    public float zGeschw = 5;

    private Transform currentZiegel;

    private Vector3 MaxSpeed;

    private Vector3 SpawnPosition;

	void Start () {
        SpawnPosition = Camera.main.transform.position + Vector3.down * 5 + Vector3.forward * 10;
    }
	

	void Update () {
        if(Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            SpawnPosition = Input.touchCount > 0 ? Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) :
                                         Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (SpawnPosition.y < 7)
                SpawnPosition.y = 7;
            SpawnPosition.z = EBGM.Instance.Eberhardt.position.z;
            currentZiegel = Instantiate(Ziegel, 
                                        SpawnPosition, 
                                        Quaternion.identity).transform;
            MaxSpeed = Vector3.zero;
        }
        if(currentZiegel != null && (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)))
        {
            Vector3 mouseposition = Input.touchCount > 0 ? Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) :
                                         Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mouseposition.y < 7)
                mouseposition.y = 7;
            mouseposition.z = EBGM.Instance.Eberhardt.position.z;
            currentZiegel.position = mouseposition;
            Vector3 velocity = Vector3.zero;// Vector3.up * Input.GetAxis("Mouse Y") * velocityMultiplier + Vector3.right * Input.GetAxis("Mouse X") * velocityMultiplier + Vector3.forward * zGeschw;
            if (velocity.magnitude > MaxSpeed.magnitude)
                MaxSpeed = velocity;
        }
        if(currentZiegel != null && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            currentZiegel.GetComponent<Rigidbody>().velocity = MaxSpeed;
            currentZiegel = null;
            MaxSpeed = Vector3.zero;
        }
	}
}
