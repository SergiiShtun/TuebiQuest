using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BearGameMaster : MonoBehaviour {

    public static BearGameMaster Instance;

    public Text PointsText;
    public Text GameTimerText;

    public GameObject Bear;
    public Transform[] BearSpawn;
    public Transform[] BearGoal;
    public bool[] LaneBlocked;
    private float[] releaseBlocking;

    private float timer;
    private float GameTimer;
    private bool GameOver;

    [SerializeField]
    private int Points;

    private bool pointLimitReached;

    private void Start()
    {
        Instance = this;
        releaseBlocking = new float[3];
    }

    void Update ()
    {
		if(!GameOver && timer < 0)
        {
            int selectedLane = Random.Range(0, 3);
            if (LaneBlocked[selectedLane])
            {
                timer = Random.Range(0.7f, 1f);
            }
            else
            {
                Bear b = Instantiate(Bear, BearSpawn[selectedLane].position + Vector3.up * 0.7f, Quaternion.identity).GetComponent<Bear>();
                b.currentLane = selectedLane;
                b.goal = BearGoal[selectedLane];
                timer = Random.Range(1.5f, 4f);
            }
            for(int i = 0; i< 3; i++)
            {
                if (LaneBlocked[i] && GameTimer - releaseBlocking[i] > 5)
                    LaneBlocked[i] = false;
            }
        }

        timer -= Time.deltaTime;

        GameTimer += Time.deltaTime;
        if (!GameOver && GameTimer > 180)
        {
            print("GameOver");
            GameOver = true;
            if (pointLimitReached)
                PlayerPrefs.SetString("MGameState", "won");
            else
                PlayerPrefs.SetString("MGameState", "lost");
            GameTimerText.text = "Time: " + 0;
        }
        else if (!GameOver)
        {
            GameTimerText.text = "Time: " + (180 - GameTimer).ToString().Split('.')[0];
        }
        else if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(1);
	}

    public void CollectPoints(int points)
    {
        Points += points;
        PointsText.text = "Points: " + Points;
        if (Points >= 300)
            pointLimitReached = true;
    }

    public void CollectHoney(int bearLane)
    {
        LaneBlocked[bearLane] = true;
        releaseBlocking[bearLane] = GameTimer;
        foreach(Bear bear in FindObjectsOfType<Bear>())
        {
            if(bear.currentLane == bearLane)
            {
                Destroy(bear.gameObject, 5f);
                bear.MoveSpeed = 0;
            }
        }
    }

}
