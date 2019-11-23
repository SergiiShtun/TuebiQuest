using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ziegel : MonoBehaviour
{

    public Text PointsText;

    private int Points;
    private int PointsToGet;
    private float ebene;
    

    // Use this for initialization
    void Start () {
        PointsText = GameObject.Find("PointsText").GetComponent<Text>();
        //PointsText.text = "lol";
        //Points = int.Parse(PointsText.text.Substring(8));
        Destroy(gameObject, 2.0f);
	}


    
    void OnCollisionEnter(Collision collision)
    {
        //print("Collision");
        if (collision.collider.tag == "Character")
        {
            EBGM.points += EBGM.pointsToGet;
            EBGM.pointsToGet += 5;
            PointsToGet =  EBGM.pointsToGet;
            Points = EBGM.points;
            //print("+" + PointsToGet);
            Destroy(gameObject);
            PointsText.text = Points.ToString();
            collision.collider.GetComponent<Eberhardt>().GoUpEbene();
        }
        else if (collision.collider.tag == "Stair")
        {
            EBGM.points -= 1;
            if (EBGM.points <= 0)
                EBGM.points = 0;
            Points = EBGM.points;
            Destroy(gameObject,0.5f);
            PointsText.text = Points.ToString();
        }
    }

}
