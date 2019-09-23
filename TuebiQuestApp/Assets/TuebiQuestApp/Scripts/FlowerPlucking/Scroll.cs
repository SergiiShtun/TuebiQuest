using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour {

    public Sprite[] ScrollSprites;
    Image img;
    bool isTriggered;

    void Start()
    {
        img = GetComponent<Image>();
    }

	public void TriggerScroll()
    {
        if(isTriggered)
        {
            img.sprite = ScrollSprites[0];
            isTriggered = false;
        }
        else
        {
            img.sprite = ScrollSprites[1];
            isTriggered = true;
        }
    }
}
