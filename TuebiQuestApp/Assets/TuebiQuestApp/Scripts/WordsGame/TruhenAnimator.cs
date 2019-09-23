using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruhenAnimator : MonoBehaviour {

    public Sprite TruheOffen;
    public Sprite TruheZu;
    public Sprite TruheGold;
    public Sprite TruheRot;

    public float GlowTime;
    public int TruhenState;

    private SpriteRenderer sr;
    private float timer;

	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        ChangeState(0);
	}

    private void Update()
    {
        if ((TruhenState == 2 || TruhenState == 3) &&
            timer < 0)
            ChangeState(0);
        timer -= Time.deltaTime;
    }

    public void ChangeState(int newState)
    {
        if (newState < 0 || newState > 3)
            return;

        if(newState == 0)
        {
            sr.sprite = TruheZu;
        }
        else if(newState == 1)
        {
            sr.sprite = TruheOffen;
        }
        else if (newState == 2)
        {
            sr.sprite = TruheGold;
            timer = GlowTime;
        }
        else if (newState == 3)
        {
            sr.sprite = TruheRot;
            timer = GlowTime;
        }
        TruhenState = newState;
    }

}
