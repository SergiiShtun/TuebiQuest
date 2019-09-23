using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EBGM : MonoBehaviour
{

    private float timer;
    private Text timeText;

    public static EBGM Instance;
    public Transform Eberhardt;

    void Start()
    {
        Instance = this;
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        timeText.text = "Time: " + timer.ToString().Split('.')[0];
    }

    /// <summary>
    /// Call EndGame when Eberhard is on the last stair!!
    /// </summary>
    public void EndGame()
    {
        // ToDo: Finish Game
        int bonus = 0;
        if (timer < 20)
            bonus = 25;
        else if (timer < 30)
            bonus = 20;
        else if (timer < 40)
            bonus = 15;
        else if (timer < 50)
            bonus = 10;
        else if (timer < 60)
            bonus = 5;

        int Points = int.Parse(GameObject.Find("Text").GetComponent<Text>().text.Split(' ')[1]) + bonus;

        print(Points);
    }

}
