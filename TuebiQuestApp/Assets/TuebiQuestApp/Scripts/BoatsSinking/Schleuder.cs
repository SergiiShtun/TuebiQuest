using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schleuder : MonoBehaviour {

    public GameObject Rock;
    public float mouseDistanceFactor;
    public float maxForce;

    Vector3 MousePosition;
    Vector3 OldMousePosition;

    private Vector3 ForceVector;

    private SchleuderLine[] sLines;

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public float percentHead = 0.4f;

    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;

        // A simple 2 color gradient with a fixed alpha of 0.5f.
        float alpha = 0.5f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

        sLines = GetComponentsInChildren<SchleuderLine>();
    }

    [System.Obsolete]
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            OldMousePosition = Input.mousePosition;
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            MousePosition = Input.mousePosition;

            ForceVector = Vector3.ClampMagnitude((MousePosition - OldMousePosition) / mouseDistanceFactor, maxForce);

            //Debug.DrawLine(transform.position, transform.position - (ForceVector / 100f), Color.green,2,false);
            var x = transform.position - (ForceVector / 100f);
            lineRenderer.widthCurve = new AnimationCurve(
            new Keyframe(0, 3f)
            , new Keyframe(0.999f - percentHead, 3f)  // neck of arrow
            , new Keyframe(1 - percentHead, 1f)  // max width of arrow head
            , new Keyframe(1, 0f)); // tip of arrow
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, x);
            print("tp: " + transform.position + "2tp: " + x);
            foreach (SchleuderLine sl in sLines)
                sl.SetTarget(Vector3.back * ForceVector.magnitude / 100f);
        }

        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            Rigidbody rockRb = Instantiate(Rock, transform.position, transform.rotation).GetComponent<Rigidbody>();
            ForceVector = -ForceVector;
            ForceVector.z = ForceVector.y;
            print(ForceVector);
            rockRb.AddForce(ForceVector);
            foreach (SchleuderLine sl in sLines) { 
                sl.SetTarget(Vector3.zero);
            }
            lineRenderer.widthCurve = new AnimationCurve(
             new Keyframe(0, 3f)
             , new Keyframe(0.999f - percentHead, 3f)  // neck of arrow
             , new Keyframe(1 - percentHead, 1f)  // max width of arrow head
             , new Keyframe(1, 0f));
            lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
            lineRenderer.SetPosition(1, new Vector3(0,0,0));
        }
    }
}
