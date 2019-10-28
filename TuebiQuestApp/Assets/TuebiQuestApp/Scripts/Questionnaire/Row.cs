using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Row : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;

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
            if (transform.position.y <= -307.0f)
                transform.position = new Vector3(transform.position.x, 309.0f, transform.position.z);

            transform.position = new Vector3(transform.position.x, Mathf.FloorToInt(transform.position.y - 88.0f), transform.position.z);

            yield return new WaitForSeconds(timeInterval);
        }

        // randomValue = Random.Range(-307, 309);
        //var randList = new List<int> { -307, -219, -131, -43, 45, 133, 221, 309};
        //var random = new System.Random();
        //int index = random.Next(randList.Count);
        //randomValue = randList[index];
        //Debug.Log("rand: " + randomValue);


        //for (int i = 0; i < Mathf.Abs(randomValue); i++)
        //{
        //    if (transform.position.y < -307)
        //        transform.position = new Vector2(transform.position.x, 131);

        //    if(transform.position.y > 309)
        //        transform.position = new Vector2(transform.position.x, 131);

        //    transform.position = new Vector2(transform.position.x, transform.position.y - 88);

        //        timeInterval = 0.02f;

        //    yield return new WaitForSeconds(timeInterval);
        //}
        randomValue = UnityEngine.Random.Range(60, 80);
        Debug.Log("rand: " + randomValue);

        switch (randomValue % 3)
        {
            case 1:
                randomValue += 2;
                break;
            case 2:
                randomValue += 1;
                break;
        }

        for (int i = 0; i < randomValue; i++)
        {
            if (transform.position.y <= -307.0f)
                transform.position = new Vector3(transform.position.x, 309.0f, transform.position.z);

            transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y - 88.0f), transform.position.z);

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

        if (Mathf.Round(transform.position.y) >= -307.0f && Mathf.Round(transform.position.y)  < -219.0f)
            stoppedSlot = "Diamond";
        else if (Mathf.Round(transform.position.y) >= -219.0f && Mathf.Round(transform.position.y) < -131.0f)
            stoppedSlot = "Crown";
        else if (Mathf.Round(transform.position.y) >= -131.0f && Mathf.Round(transform.position.y) < -43.0f)
            stoppedSlot = "Melon";
        else if (Mathf.Round(transform.position.y) >= -43.0f && Mathf.Round(transform.position.y) < 45.0f)
            stoppedSlot = "Bar";
        else if (Mathf.Round(transform.position.y) >= 45.0f && Mathf.Round(transform.position.y) < 133.0f)
            stoppedSlot = "Seven";
        else if (Mathf.Round(transform.position.y) >= 133.0f && Mathf.Round(transform.position.y) < 221.0f)
            stoppedSlot = "Cherry";
        else if (Mathf.Round(transform.position.y) >= 221.0f && Mathf.Round(transform.position.y) < 309.0f)
            stoppedSlot = "Lemon";
        else if (Mathf.Round(transform.position.y) >= 309.0f && Mathf.Round(transform.position.y) < 315.0f) 
            stoppedSlot = "Diamond";

        rowStopped = true;

    }

    private void OnDestroy()
    {
        Handle.HandlePulled -= StartRotating;
    }
}
