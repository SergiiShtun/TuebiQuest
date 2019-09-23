using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePulse : MonoBehaviour {

    public bool PlayOnAwake;

    public float maxSize = 4;
    public float steps = 15;
    public int skipSteps = 0;
    public float GrowFrameDelay = 0.03f;
    public float BounceSupress = 10;
    public Vector3 PulseSize;
    public float PulseSpeed = 10f;
    public Transform pivot;

    private Coroutine scalling;
    private float pulseTimer;
    private bool pulse;
    private Vector3 pulseScale;
    private Vector3 pivotPos;

    internal Vector3 initScale;
    internal Vector3 initPos;

    void Start()
    {
        initScale = transform.localScale;
        initPos = transform.position;
        pulse = false;

        if (pivot != null)
            pivotPos = pivot.position;

        if (PlayOnAwake)
            StartAnimating();
    }

    public void StartAnimating()
    {
        if (!gameObject.activeInHierarchy)
            return;
        if (scalling != null)
            StopCoroutine(scalling);
        pulseTimer = 0;
        pulse = false;
        scalling = StartCoroutine(Scale());
    }
    private void Update()
    {
        if (pulse)
        {
            pulseTimer += Time.deltaTime * PulseSpeed;
            transform.localScale = pulseScale + PulseSize * (Mathf.Cos(pulseTimer + Mathf.PI) + 1) / 2.0f;
            if (pivot != null)
                transform.position = transform.position + (pivotPos - pivot.position);
            if (pulseTimer >= Mathf.PI * 2)
                pulseTimer = 0;
        }

    }

    IEnumerator Scale()
    {
        for (int i = skipSteps; i < steps; i++)
        {
            float step = i / steps;
            float p = 0.3f;
            transform.localScale = Vector3.one *
                ((maxSize) *
                (Mathf.Pow(2, -BounceSupress * step) * Mathf.Sin((step - (p / 4)) * (Mathf.PI * 2) / p) + 1));
            yield return new WaitForSeconds(GrowFrameDelay);
        }
        pulse = true;
        pulseScale = transform.localScale;
    }

}
