using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatGameMaster : MonoBehaviour {

    public static BoatGameMaster Instance;

    public GameObject Boat;
    public Transform BoatSpawn1;
    public Transform BoatSpawn2;
    public float BoatUpOffset;
    public float RightOffset;
    public Text PointsText; 
    public Text TimerText;

    private Vector3 BoatSpawnDirection;
    private float BoatSpawnLength;
    private float timer;
    private float Points;

    private float GameTimer;

    private bool GameOver;

    public SpriteRenderer FadeScreen;
    public GameObject EndScreen;

	void Start ()
    {
        Instance = this;
        BoatSpawnDirection = (BoatSpawn2.position - BoatSpawn1.position).normalized;
        BoatSpawnDirection.y = 0;
	}
	

	void Update ()
    {
		if(!GameOver && timer < 0)
        {
            BoatSpawnLength = UnityEngine.Random.Range(10f, 30f);
            Instantiate(Boat, transform.position + Vector3.right * RightOffset + BoatSpawnDirection * BoatSpawnLength + Vector3.up * BoatUpOffset, Quaternion.identity);
            timer = UnityEngine.Random.Range(2f, 4f);

        }
        timer -= Time.deltaTime;
        GameTimer += Time.deltaTime;
        if(!GameOver)
            TimerText.text = "Time: " + (60 - GameTimer).ToString().Split('.')[0];

        if(!GameOver && (GameTimer > 60 || Points >= 300))
        {
            GameOver = true;
            Array.ForEach(FindObjectsOfType<Rock>(), r => Destroy(r.gameObject));
            FindObjectOfType<Schleuder>().enabled = false;
            StartCoroutine(EndGame());
        }

	}


    public void BoatHit(int points)
    {
        Points += points;
        if (Points < 0)
            Points = 0;
        if (Points > 300)
            Points = 300;
        PointsText.text = Points > 200 ? Points + "/300" : Points + "/200";
    }

    private IEnumerator EndGame()
    {
        for (int i = 0; i < 100; i++)
        {
            FadeScreen.color = new Color(0, 0, 0, FadeScreen.color.a + 1 / 150f);
            yield return null;
        }
        var text = Instantiate(EndScreen, Vector3.zero, Quaternion.identity).GetComponentInChildren<Text>();
        text.text = "Hurra du hast gewonnen! \n Der coole Satz ist cool! \n " + PointsText.text;
    }


}
