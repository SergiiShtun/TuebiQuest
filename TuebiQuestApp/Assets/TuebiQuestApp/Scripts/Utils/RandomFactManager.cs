using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFactManager : MonoBehaviour
{

    public float EventCheckTimer;

    public Dictionary<GeoPoint, string> FactLocations { get; private set; }

    private MainGameManager mgm;
    private float evTimer;

    void Awake()
    {
        FactLocations = new Dictionary<GeoPoint, string>();
        mgm = MainGameManager.Instance;
    }

    void Update()
    {
        if(evTimer <= 0)
        {
            foreach(GeoPoint gp in FactLocations.Keys)
            {
                if (gp.Compare(mgm.GlobalLocationManager.GetCurrentLocation(), 10 / 1000f))
                {
                    print(FactLocations[gp]);
                    Handheld.Vibrate();
                }
            }
            evTimer = EventCheckTimer;
        }
        evTimer -= Time.deltaTime;
    }

    public void AddLocation(GeoPoint location, string text)
    {
        FactLocations.Add(location, text);
    }

}
