using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Answer : MonoBehaviour, IPointerClickHandler {

    public bool correct;

    public void OnPointerClick(PointerEventData eventData)
    {
        string index = eventData.pointerCurrentRaycast.gameObject.name.Substring(
            eventData.pointerCurrentRaycast.gameObject.name.IndexOf('%') + 1);
        FrageMaster.Instance.Answered(correct, index);
    }
}
