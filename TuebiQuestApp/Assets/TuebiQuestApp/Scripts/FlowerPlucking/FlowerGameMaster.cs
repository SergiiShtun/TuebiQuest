using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Game Starts when in bota
/// Game is started on the field
/// start location is saved
/// when moved chance increases to find flower
/// flower point is saved and when moved some meters flowers can increase again
/// </summary>
public class FlowerGameMaster : MonoBehaviour {

    //Debugging:
    public Text DebugUIText;

    public List<Sprite> FlowerSprites;
    public GameObject FlowerInstance;
    public FlowerCamera FlowerCamera;

    public float NextFlowerDistance = 5;
    public float ChanceIncreasePerDistance = 10;
    public float DistanceDivisor = 5000;

    // Phone Attributes
    // Vibration
    public float VibrationIntervall;
    public float VibrationTime;

    private float vibrationTimer;

    private GeoPoint lastLocation;
    private GlobalLocationScript locationScript;

    private bool flowerSpawned;
    private double currentSpawnChance;
    //private double currentDistaceSpawnChance;

    private void Start()
    {
        //locationScript = GlobalGameManager.Instance.GetComponent<GlobalLocationScript>();
        //lastLocation = locationScript.GetCurrentLocation();
    }


    private void Update()
    {
        if (flowerSpawned)
        {
            vibrationTimer += Time.deltaTime;
            if (vibrationTimer > VibrationIntervall && vibrationTimer < VibrationIntervall + VibrationTime)
                Handheld.Vibrate();
            else if (vibrationTimer >= VibrationIntervall + VibrationTime)
                vibrationTimer = 0;
        }
        else
        {
            if (Random.Range(30.0f, 100.0f) < currentSpawnChance) //+ currentDistaceSpawnChance
            {
                flowerSpawned = true;
                var flower = Instantiate(FlowerInstance, FlowerCamera.transform.position + FlowerCamera.transform.forward * 5, Quaternion.identity);
                flower.transform.LookAt(flower.transform.position + (flower.transform.position - FlowerCamera.transform.position));
            }
//            currentDistaceSpawnChance = lastLocation.Distance(locationScript.GetCurrentLocation()) / DistanceDivisor;

            currentSpawnChance += Time.deltaTime;
        }
        DebugUIText.text =
            "Location:" + " \n" +
            "Last Location: " + lastLocation + " \n" +
            "Distance:" +  " \n" +
            "Chance: " + currentSpawnChance;
    }


    public void PluckFlower(Flower flowerPlucked)
    {
        flowerSpawned = false;
        //lastLocation = locationScript.GetCurrentLocation();
        currentSpawnChance = -1.0f;
    }
}
