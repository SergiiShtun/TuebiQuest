using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalLocationScript : MonoBehaviour
{

    public Text debugLocationText;
    public GeoPoint startPoint;

    //private void Update()
    //{
    //    GeoPoint curLoc = GetCurrentLocation();
    //    if(startPoint != null)
    //    debugLocationText.text = "Latitude: " + curLoc.Latitude + "\n"
    //        + "Longitude: " + curLoc.Longitude + "\n" +
    //        "Altitude: " + curLoc.Altitude + "\n"
    //        + "Distance: " + curLoc.Distance(startPoint);

    //    if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    //        || Input.GetMouseButtonDown(0))
    //    {
    //        startPoint = GetCurrentLocation();
    //    }
    //}

    public GeoPoint GetCurrentLocation()
    { 
        return new GeoPoint(Input.location.lastData);
    }

    IEnumerator Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start(0.1f, 0.5f);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();
        startPoint = GetCurrentLocation();
    }
}
