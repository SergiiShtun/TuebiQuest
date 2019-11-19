using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Answer : MonoBehaviour, IPointerClickHandler {

    public bool correct;
    public List<GameObject> answerGameObjects = new List<GameObject>();

    public void OnPointerClick(PointerEventData eventData)
    {
        string index = eventData.pointerCurrentRaycast.gameObject.name.Substring(
            eventData.pointerCurrentRaycast.gameObject.name.IndexOf('%') + 1);
        GraphicRaycaster gr = answerGameObjects[0].GetComponent<GraphicRaycaster>();
        var x = answerGameObjects[0].GetComponent<GraphicRaycaster>();
        var y = answerGameObjects[0].GetComponents<GraphicRaycaster>();
        //  y[0].enabled = false;
        Debug.Log("x " + x);
        Debug.Log("y " + y);
        FrageMaster.Instance.Answered(correct, index);
    }
}
