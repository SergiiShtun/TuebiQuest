using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Answer : MonoBehaviour, IPointerClickHandler {

    public bool correct;

    public void OnPointerClick(PointerEventData eventData)
    {
        FrageMaster.Instance.Answered(correct);
    }
}
