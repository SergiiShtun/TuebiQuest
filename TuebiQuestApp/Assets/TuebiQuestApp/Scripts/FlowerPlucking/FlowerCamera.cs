using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCamera : MonoBehaviour {

    
    Quaternion fixRotation;
    Quaternion offsetRotation;

    void Start()
    {
        Input.gyro.enabled = true;
        fixRotation = Quaternion.Euler(90, 0, 0);
        offsetRotation = Quaternion.Euler(0, 0, 0);
    }

    protected void Update()
    {
        GyroModifyCamera();
    }

    /********************************************/

    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    void GyroModifyCamera()
    {
        transform.rotation =  offsetRotation * fixRotation * GyroToUnity(Input.gyro.attitude);
        //transform.Translate(Input.acceleration.x, 0, -Input.acceleration.z);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    public void ResetGyroCamera()
    {
        offsetRotation = Quaternion.Inverse(fixRotation * GyroToUnity(Input.gyro.attitude));
    }
}
