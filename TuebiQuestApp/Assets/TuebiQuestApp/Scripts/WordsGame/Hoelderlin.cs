using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoelderlin : MonoBehaviour {

    public Sprite Idle;
    public Sprite Up;
    public Sprite Down;

    public float xDirL;
    public float xDirR;


    [HideInInspector]
    public Transform SpeechBubble;

    private SpriteRenderer sr;
    private Coroutine throwRoutine;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}

    public void StartThrowing()
    {
        if (throwRoutine != null)
        {
            StopCoroutine(throwRoutine);
            if(SpeechBubble != null)
                SpeechBubble.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        throwRoutine = StartCoroutine(ThrowWord());
    }

    private IEnumerator ThrowWord()
    {
        yield return null;
        sr.sprite = Up;
        SpeechBubble.position = transform.position + new Vector3(1, 1, 0);
        yield return new WaitForSeconds(1f);
        SpeechBubble.position = transform.position + new Vector3(1, 0, 0);
        SpeechBubble.GetComponent<Rigidbody2D>().gravityScale = 1;
        SpeechBubble.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(xDirL, xDirR), Random.Range(0, 2f));
        sr.sprite = Down;
        yield return new WaitForSeconds(1.7f);
        sr.sprite = Idle;

        yield return null;
    }

}
