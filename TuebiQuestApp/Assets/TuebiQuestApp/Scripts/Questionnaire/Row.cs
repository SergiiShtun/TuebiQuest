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
        FrageMaster.HandlePulled += StartRotating;
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

        //for (int i = 0; i < 30; i++)
        //{
        //    if (transform.position.y <= -3.5f)
        //        transform.position = new Vector2(transform.position.x, 1.75f);

        //    transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);

        //    yield return new WaitForSeconds(timeInterval);
        //}

       // randomValue = Random.Range(-307, 309);
        var randList = new List<int> { -307, -219, -131, -43, 45, 133, 221, 309};
        var random = new System.Random();
        int index = random.Next(randList.Count);
        randomValue = randList[index];
        Debug.Log("rand: " + randomValue);


        for (int i = 0; i < randomValue; i++)
        {
            if (transform.position.y <= -307)
                transform.position = new Vector2(transform.position.x, 131);

            transform.position = new Vector2(transform.position.x, transform.position.y - 88);

                timeInterval = 0.2f;

            yield return new WaitForSeconds(timeInterval);
        }

        if (transform.position.y == -3.5f)
            stoppedSlot = "Diamond";
        else if (transform.position.y == -2.75f)
            stoppedSlot = "Crown";
        else if (transform.position.y == -2f)
            stoppedSlot = "Melon";
        else if (transform.position.y == -1.25f)
            stoppedSlot = "Bar";
        else if (transform.position.y == -0.5f)
            stoppedSlot = "Seven";
        else if (transform.position.y == 0.25f)
            stoppedSlot = "Cherry";
        else if (transform.position.y == 1f)
            stoppedSlot = "Lemon";
        else if (transform.position.y == 1.75f)
            stoppedSlot = "Diamond";

        rowStopped = true;

    }

    private void OnDestroy()
    {
        FrageMaster.HandlePulled -= StartRotating;
    }
}
