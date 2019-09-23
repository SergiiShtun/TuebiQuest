using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoPoint
{
    public double Latitude;
    public double Longitude;
    public double Altitude;

    public GeoPoint()
    {
        Latitude = 0;
        Longitude = 0;
        Altitude = 0;
    }

    public GeoPoint(LocationInfo locInfo)
    {
        Latitude = locInfo.latitude;
        Longitude = locInfo.longitude;
        Altitude = locInfo.altitude;
    }

    public GeoPoint(double lat, double longi, double alt)
    {
        Latitude = lat;
        Longitude = longi;
        Altitude = alt;
    }

    public double Distance(GeoPoint otherPoint)
    {
        float latDist = Mathf.Abs((float)(Latitude - otherPoint.Latitude));
        float longDist = Mathf.Abs((float)(Longitude - otherPoint.Longitude));
        return Mathf.Sqrt(latDist * latDist + longDist * longDist);
    }

    public bool Compare(GeoPoint otherPoint, float accuracy, float heightAccuracy = 0)
    {
        return (Distance(otherPoint) < accuracy) && (heightAccuracy == 0 || Mathf.Abs((float)(Altitude - otherPoint.Altitude)) < heightAccuracy);
    }

    public override string ToString()
    {
        return "Lat: " + Latitude + " Long: " + Longitude + " Alt: " + Altitude;
    }
}
