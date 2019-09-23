using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truhe : MonoBehaviour
{


    public float MovementSpeed;
    public Text PointsText;
    public GameObject FloatingText;

    private float yPosition;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private int Points = 0;

    private BouncePulse bp;
    private TruhenAnimator tAnim;

    public List<string> UsedWords { get; private set; }

    [HideInInspector]
    public List<Transform> ExistingSpeechBubbles;

    public float OpenDistance;
    
    void Start()
    {
        bp = GetComponent<BouncePulse>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        tAnim = GetComponent<TruhenAnimator>();
        //Points = 0;

        PointsText.text = "Points: " + Points.ToString();
        yPosition = transform.position.y;

        UsedWords = new List<string>();
    }
    
    void Update()
    {

        float horizontalMovement = (Input.GetAxis("Mouse X") + Input.GetAxis("Horizontal")*10f) * MovementSpeed * Time.deltaTime * 60f;
        
        Vector2 movement = new Vector2(horizontalMovement, 0);
        rb.velocity = movement;

        bool openTrigger = false;
        foreach(Transform s in ExistingSpeechBubbles)
        {
            if (s == null)
                continue;
            if (Vector2.Distance(transform.position, s.position) < OpenDistance)
            {
                openTrigger = true;
                if(tAnim.TruhenState == 0)
                    tAnim.ChangeState(1);
                break;
            }
        }
        if (!openTrigger && tAnim.TruhenState == 1)
            tAnim.ChangeState(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sprechblase")
        {
            bp.StartAnimating();

            Points += 50;
            print("+50");
            Destroy(other.gameObject);
            PointsText.text = "Points: " + Points.ToString();

            string collectedText = other.GetComponentInChildren<Text>().text;
            UsedWords.Add(collectedText);

            foreach (string w in UsedWords)
                print(w);

            tAnim.ChangeState(2);

            var text = Instantiate(FloatingText, transform.position + new Vector3(0, 10, 0), Quaternion.identity).GetComponentInChildren<Text>();
            text.text = collectedText;

            text = Instantiate(FloatingText, transform.position + new Vector3(0, 10, 0) + Vector3.right * 10, Quaternion.identity).GetComponentInChildren<Text>();
            text.text = "+50";
        }
        if (other.tag == "SprechblaseFalsch")
        {
            bp.StartAnimating();

            Points -= 5;
            print("-5");
            Destroy(other.gameObject);
            PointsText.text = "Points: " + Points.ToString();
            tAnim.ChangeState(3);

            var text = Instantiate(FloatingText, transform.position + new Vector3(0, 10, 0), Quaternion.identity).GetComponentInChildren<Text>();
            text.text = "-5";

        }
    }

    void SetCountText()
    {
        PointsText.text = "Points: " + Points.ToString();
    }
}
