using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerDownHandler {

    public float Height;

    float initHeight;
    bool extended;
    bool mouseDown;
    Vector3 goalPosition;


	void Start () {
        initHeight = transform.position.y;
        goalPosition = Vector3.one * initHeight;
    }

    private void Update()
    {

        if(mouseDown)
            goalPosition = Input.mousePosition;
        else if (extended)
            goalPosition.y = Height;
        else
            goalPosition.y = initHeight;

        if (goalPosition.y > Height * 2f / 3f)
            extended = true;
        else
            extended = false;

        if (goalPosition.y > Height * 4f / 3f)
            goalPosition.y = Height * 4f / 3f;

        transform.position = Vector3.Lerp(
                transform.position, 
                new Vector3(transform.position.x, goalPosition.y, transform.position.z), 
                0.1f);

        if(!Input.GetMouseButton(0))
            mouseDown = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseDown = true;
    }
}
