using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameExplanation : MonoBehaviour {

    public GameObject StartButton;
    public Sprite[] Images;

    private Image image;
    private int imageIndex;

	void Start () {
        image = GetComponent<Image>();
        image.sprite = Images[0];
    }
	
	void Update () {
		if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            || Input.GetMouseButtonDown(0))
        {
            imageIndex++;
            if (imageIndex >= Images.Length)
                StartButton.SetActive(true);
            else
                image.sprite = Images[imageIndex];
        }
	}
}
