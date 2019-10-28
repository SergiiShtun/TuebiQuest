using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Handle : MonoBehaviour, IPointerDownHandler
{
    public static event Action HandlePulled = delegate { };
    [SerializeField]
    private Row[] rows;

    [SerializeField]
    private Transform HandleObject;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine("PullHandle");
        }
    }
    private IEnumerator PullHandle()
    {
        for (int i = 0; i < 15; i += 5)
        {
            HandleObject.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

        HandlePulled();

        for (int i = 0; i < 15; i += 5)
        {
            HandleObject.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
