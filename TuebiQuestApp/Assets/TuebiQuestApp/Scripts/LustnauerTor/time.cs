using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class time : MonoBehaviour
{
    public int timeLeft = 90; 
    public Text countdown; 

    void Start()
    {
        StartCoroutine("LoseTime");
    }
    void Update()
    {
            countdown.text = ("Time Left: " + timeLeft);
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            if (timeLeft<0)
        {
            countdown.text = ("That's really embarassing");
            StartCoroutine(Exit());

        }
        }
    }

    IEnumerator Exit()
    {
        countdown.text = "TIMES UP";
        yield return new WaitForSeconds(3);
        Application.LoadLevel(Application.loadedLevel);
    }
}