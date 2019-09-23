using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class healthyhealth : MonoBehaviour {

    //
    public GameObject heart1, heart2, heart3;
    public Text gameOver;
    public static int health, stars;
    public bool FirstChance = true;

	void Start () {
        health = 3;
        stars = 3;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        switch (health)
        {
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                if (stars == 0 && FirstChance == true)
                {
                    health += 1;
                    heart1.gameObject.SetActive(true);
                    StartCoroutine(Announce());
                    FirstChance = false;
                    break;
                }
                gameOver.text = "Game over. You scored " + points.counter;
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                StartCoroutine(Wait());
                Application.LoadLevel(Application.loadedLevel);
                break;
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
    }
    IEnumerator Announce()
        {
            gameOver.text = "Got an extra heart for your stars!";
            yield return new WaitForSeconds(2);
            gameOver.text = "";
        }


        



}
