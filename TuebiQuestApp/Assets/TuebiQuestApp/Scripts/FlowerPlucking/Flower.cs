using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {
    
    public bool Poisonous;
    public bool Fuchsia;
    public int MaxCuts;
    public float UpOffset;
    public Transform pivot;

    private int curCuts;
    private Transform flowerPivot;
    private float maxScale;
    private float yPosition;

    private Coroutine currentRoutine;

    private void Start()
    {
        flowerPivot = transform.Find("FlowerPivot");
        maxScale = transform.localScale.x;
        yPosition = transform.position.y;
        transform.localScale = Vector3.one * 0.3f;
        //SetCurrentRoutine(Grow());
    }

    void SetCurrentRoutine(IEnumerator routine)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(routine);
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SetCurrentRoutine(Shrink());
    }
    */

    public void Cut()
    {
        curCuts++;
        if (curCuts >= MaxCuts)
        {
            GameObject.Find("Handlers").GetComponent<FlowerGameMaster>().PluckFlower(this);
            Destroy(transform.parent.gameObject, 0f);
        }
    }


    IEnumerator FallOver()
    {

        int maxSteps = 1000;
        float curStep = 0;
        float fallSpeed = 0.1f;
        while(maxSteps > 0)
        {
            maxSteps--;
            curStep -= fallSpeed;
            transform.rotation = Quaternion.Euler(270 - curStep, 0, 0);
            transform.position = pivot.position + Vector3.up * UpOffset + (pivot.position - flowerPivot.position);
            yield return new WaitForSeconds(0.033f);
        }


        yield return null;

    }

    IEnumerator Grow()
    {
        int maxSteps = 50;
        float curStep = 10;
        while (curStep < maxSteps)
        {
            curStep++;
            transform.localScale = Vector3.one * (maxScale * curStep / maxSteps);
            transform.position = new Vector3(transform.position.x, yPosition * curStep / maxSteps, transform.position.z);
            yield return new WaitForSeconds(0.033f);
        }

        yield return null;
    }

    IEnumerator Shrink()
    {
        //int maxSteps = 20;
        //float curStep = maxSteps;
        //float curScale = transform.localScale.x;
        //float curYPosition = transform.position.y;
        //while (curStep > 0)
        //{
        //    curStep--;
        //    transform.localScale = Vector3.one * (curScale * curStep / maxSteps);
        //    transform.position = new Vector3(transform.position.x, curYPosition * curStep / maxSteps, transform.position.z);
        //    yield return new WaitForSeconds(0.033f);
        //}

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MainCamera"))
        {
            SetCurrentRoutine(Grow());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            SetCurrentRoutine(Shrink());
        }
    }

}
