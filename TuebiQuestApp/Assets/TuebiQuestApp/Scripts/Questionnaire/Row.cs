using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
using System.Linq;

public class Row : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;

    private List<int> list = new List<int> { -9, -3, 3, 9 };
    // Start is called before the first frame update
    void Start()
    {
        rowStopped = true;
        Handle.HandlePulled += StartRotating;
    }

    private void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine("Rotate");
    }
    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;

        for (int i = 0; i < 30; i++)
        {
            if (transform.position.x <= -9.0f)
                transform.position = new Vector2(9.0f, transform.position.y);

            transform.position = new Vector2(transform.position.x - 3.0f, transform.position.y);

            yield return new WaitForSeconds(timeInterval);
        }

        randomValue = Random.Range(60, 100);
        switch (randomValue % 2)
        {
            case 1:
                randomValue += 1;
                break;
        }

        for (int i = 0; i < randomValue; i++)
        {

            if (!rowStopped)
            {
                if (transform.position.x <= -9.0f)
                    transform.position = new Vector2(9.0f, transform.position.y);

                transform.position = new Vector2(transform.position.x - 3.0f, transform.position.y);

                if (i > Mathf.RoundToInt(randomValue * 0.25f))
                    timeInterval = 0.05f;
                if (i > Mathf.RoundToInt(randomValue * 0.5f))
                    timeInterval = 0.1f;
                if (i > Mathf.RoundToInt(randomValue * 0.75f))
                    timeInterval = 0.15f;
                if (i > Mathf.RoundToInt(randomValue * 0.95f))
                    timeInterval = 0.2f;

                yield return new WaitForSeconds(timeInterval);
            }
            else
            {
                int closest = list.OrderBy(item => System.Math.Abs(transform.position.x - item)).First();
                Debug.Log("pos" + transform.position.x);
                Debug.Log("cl" + closest);
                transform.position = new Vector2(closest, transform.position.y);
                i = randomValue - 1;
            }
        }

        if (transform.position.x == -9.0f)
            stoppedSlot = "Castle1";
        else if (transform.position.x == -3.0f)
            stoppedSlot = "Castle2";
        else if (transform.position.x == 3.0f)
            stoppedSlot = "Castle3";
        else if (transform.position.x == 9.0f)
            stoppedSlot = "Castle4";

        rowStopped = true;

    }

    private void OnDestroy()
    {
        Handle.HandlePulled -= StartRotating;
    }
}
