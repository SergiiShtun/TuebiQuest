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

    public bool vis;

    private List<float> coordinatesList = new List<float> { -307.0f, -219.0f, -131.0f, -43.0f, 45.0f, 133.0f, 221.0f, 309.0f};
    private float position;

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
        Debug.Log("rand: " + randomValue);

        switch (randomValue % 2)
        {
            case 1:
                randomValue += 1;
                break;
        }

        for (int i = 0; i < randomValue; i++)
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

    //private IEnumerator Rotate()
    //{
    //    rowStopped = false;
    //    timeInterval = 0.025f;

    //    for (int i = 0; i < 30; i++)
    //    {
    //        if (transform.position.y <= -307.0f)
    //            transform.position = new Vector3(transform.position.x, 309.0f, transform.position.z);

    //        if (transform.position.y >= 309.0f)
    //            transform.position = new Vector3(transform.position.x, -307.0f, transform.position.z);

    //        transform.position = new Vector3(transform.position.x, transform.position.y - 88.0f, transform.position.z);
    //        if ((30 - i) == 1)
    //        {
    //            position = coordinatesList.OrderBy(item => Math.Abs(transform.position.y - item)).First();
    //            Debug.Log("pos " + position);
    //            transform.position = new Vector3(transform.position.x, position, transform.position.z);
    //        }

    //        yield return new WaitForSeconds(timeInterval);
    //    }

    //    randomValue = UnityEngine.Random.Range(60, 80);
    //    Debug.Log("rand: " + randomValue);

    //    switch (randomValue % 3)
    //    {
    //        case 1:
    //            randomValue += 2;
    //            break;
    //        case 2:
    //            randomValue += 1;
    //            break;
    //    }

    //    for (int i = 0; i < randomValue; i++)
    //    {
    //        if (transform.position.y <= -307.0f)
    //            transform.position = new Vector3(transform.position.x, 309.0f, transform.position.z);

    //        if (transform.position.y >= 309.0f)
    //            transform.position = new Vector3(transform.position.x, -307.0f, transform.position.z);

    //        transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y - 88.0f), transform.position.z);
    //        if ((randomValue - i) == 1)
    //        {
    //            position = coordinatesList.OrderBy(item => Math.Abs(transform.position.y - item)).First();
    //            Debug.Log("pos " + position);
    //            transform.position = new Vector3(transform.position.x, position, transform.position.z);
    //        }

    //        if (i > Mathf.RoundToInt(randomValue * 0.25f))
    //            timeInterval = 0.05f;
    //        if (i > Mathf.RoundToInt(randomValue * 0.5f))
    //            timeInterval = 0.1f;
    //        if (i > Mathf.RoundToInt(randomValue * 0.75f))
    //            timeInterval = 0.15f;
    //        if (i > Mathf.RoundToInt(randomValue * 0.95f))
    //            timeInterval = 0.2f;

    //        yield return new WaitForSeconds(timeInterval);
    //    }
    //    transform.position = new Vector3(transform.position.x, position, transform.position.z);
    //    if (Mathf.Round(transform.position.y) >= -307.0f && Mathf.Round(transform.position.y)  < -219.0f)
    //        stoppedSlot = "Diamond";
    //    else if (Mathf.Round(transform.position.y) >= -219.0f && Mathf.Round(transform.position.y) < -131.0f)
    //        stoppedSlot = "Crown";
    //    else if (Mathf.Round(transform.position.y) >= -131.0f && Mathf.Round(transform.position.y) < -43.0f)
    //        stoppedSlot = "Melon";
    //    else if (Mathf.Round(transform.position.y) >= -43.0f && Mathf.Round(transform.position.y) < 45.0f)
    //        stoppedSlot = "Bar";
    //    else if (Mathf.Round(transform.position.y) >= 45.0f && Mathf.Round(transform.position.y) < 133.0f)
    //        stoppedSlot = "Seven";
    //    else if (Mathf.Round(transform.position.y) >= 133.0f && Mathf.Round(transform.position.y) < 221.0f)
    //        stoppedSlot = "Cherry";
    //    else if (Mathf.Round(transform.position.y) >= 221.0f && Mathf.Round(transform.position.y) < 309.0f)
    //        stoppedSlot = "Lemon";
    //    else if (Mathf.Round(transform.position.y) >= 309.0f && Mathf.Round(transform.position.y) < 315.0f) 
    //        stoppedSlot = "Diamond";

    //    rowStopped = true;

    //}

    private void OnDestroy()
    {
        Handle.HandlePulled -= StartRotating;
    }
}
