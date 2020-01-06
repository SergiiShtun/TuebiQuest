using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class Handle : MonoBehaviour, IPointerDownHandler
{
    public static event Action HandlePulled = delegate { };
    [SerializeField]
    private Row[] rows;
    public List<GameObject> answerGameObjects = new List<GameObject>();

    [SerializeField]
    private Transform HandleObject;

    public GameObject slot;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(FrageMaster.rightAnswers > 0)
        {
            foreach (var answer in answerGameObjects)
            {
                answer.GetComponent<GraphicRaycaster>().enabled = false;
            }
            if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
            {
                StartCoroutine("PullHandle");
                FrageMaster.rightAnswers -= 1;
            }
        }    
    }
    private IEnumerator PullHandle()
    {
        for (int i = 0; i < 15; i += 5)
        {
            HandleObject.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < 15; i += 5)
        {
            HandleObject.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < 15; i += 5)
        {
            HandleObject.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }

        HandlePulled();
        slot.SetActive(true);
        for (int i = 0; i < 15; i += 5)
        {
            HandleObject.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
