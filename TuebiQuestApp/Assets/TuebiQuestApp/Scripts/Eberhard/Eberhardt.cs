using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Eberhardt : MonoBehaviour {

    public float MaxMoveRange;
    public float MoveSpeed;
    public float EbenenMultiplikator;

    public SpriteRenderer FadeScreen;
    public SpriteRenderer Gate;
    public GameObject EndScreen;


    public float speed;

    private Rigidbody rb;
    private int ebene = 1;

    private bool gameOver;
    private bool canEndGame;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

        speed = MoveSpeed * EbenenMultiplikator * ebene;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -MaxMoveRange)
            speed = MoveSpeed * EbenenMultiplikator * ebene;
        else if (transform.position.x > MaxMoveRange)
            speed = -MoveSpeed * EbenenMultiplikator * ebene;

        if(ebene != 10)
            transform.position += Vector3.right * speed * Time.deltaTime;
        else
        {
            transform.position = new Vector3(-0.15f, -2f, transform.position.z);
            if (Gate.transform.position.y <= -0.5)
                Gate.transform.position = new Vector3(Gate.transform.position.x, -0.5f, Gate.transform.position.z);
            else
                Gate.transform.position += Vector3.down * 0.1f * 0.5f;
        }
           
        if (!gameOver && ebene == 10)
        {
            gameOver = true;
            StartCoroutine(EndGame());
        }
        else if (canEndGame)
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                PlayerPrefs.SetString("MGameState", "won");
                SceneManager.LoadScene(1);
            }
        }
    }

    public void GoUpEbene()
    {
        ebene++;
        if(ebene >= 8)
        {
            MaxMoveRange = 1.5f;
            Debug.Log("Range " + MaxMoveRange);
        }
        if(ebene != 10)
        {
            transform.position += Vector3.up * 1;
            transform.position += Vector3.forward;
        }
        //if(ebene == 10)
        //{
        //    transform.position = new Vector3(-0.15f,-2f,transform.position.z);
        //}
    }


    private IEnumerator EndGame()
    {
        //for (int i = 0; i < 100; i++)
        //{
        //    FadeScreen.color = new Color(0, 0, 0, FadeScreen.color.a + 1 / 150f);
        //    yield return null;
        //}
        canEndGame = true;

        var text = Instantiate(EndScreen, Vector3.zero, Quaternion.identity).GetComponentInChildren<Text>();
        text.text = "Hurra du hast gewonnen! \n " + "Punkte: " + GameObject.Find("PointsText").GetComponent<Text>().text;

        GameObject.Find("PointsText").transform.parent.gameObject.SetActive(false);
        yield return null;
    }


}
