using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerCamera : MonoBehaviour {

    
    Quaternion fixRotation;
    Quaternion offsetRotation;
    private bool cameraAvailable;
    private WebCamTexture backCamera;
    private Texture defaultBackground;
    public RawImage background;
    public AspectRatioFitter fitter;

    void Start()
    {
        Input.gyro.enabled = true;

        defaultBackground = background.texture;
        WebCamDevice[] webCamDevices = WebCamTexture.devices;

        if (webCamDevices.Length == 0)
        {
            cameraAvailable = false;
            Debug.Log("No camera supported on this device");
            return;
        }

        for (int i = 0; i < webCamDevices.Length; i++)
        {
            //if (!webCamDevices[i].isFrontFacing)
            //{
            backCamera = new WebCamTexture(webCamDevices[i].name, Screen.width, Screen.height);
            //}
        }
        backCamera.Play();
        background.texture = backCamera;

        cameraAvailable = true;

        fixRotation = Quaternion.Euler(90, 0, 0);
        offsetRotation = Quaternion.Euler(0, 0, 0);
    }

    protected void Update()
    {
        if (!cameraAvailable)
        {
            Debug.Log("No camera available");
            return;
        }

        float ratio = (float)backCamera.width / (float)backCamera.height;
        fitter.aspectRatio = ratio;

        float scaleY = backCamera.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orientation = backCamera.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);

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
