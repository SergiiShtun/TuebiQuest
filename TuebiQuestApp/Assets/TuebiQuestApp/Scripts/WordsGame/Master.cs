using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Master : MonoBehaviour
{

    public GameObject Sprechfeld;
    
    private float timer;
    private Transform ThrowingObject;
    private float randTimer;
    public Truhe Truhe;
    public Hoelderlin Hoelderlin;

    public SpriteRenderer FadeScreen;
    public GameObject EndScreen;

    private bool gameOver;
    private bool canEndGame;


    void Start()
    {
        Vector2 position = new Vector2(Random.Range(-25.0f, 25.0f), 14.3f);
        //Instantiate(Sprechfeld, position, Quaternion.identity);
        randTimer = 1;
        ThrowingObject = GameObject.Find("shoutingMonkey").transform;
    }

    void Update()
    {
        Vector2 position;
        if (ThrowingObject == null)
            position = new Vector2(Random.Range(-15.0f, 15.0f), 14.3f);
        else
            position = ThrowingObject.position;
        if (!gameOver && timer > randTimer)
        {
            timer = 0;

            float randNumber = Random.Range(0.0f, 100.0f);
            var spr = Instantiate(Sprechfeld, position, Quaternion.identity).GetComponent<Sprechblase>();
            spr.TruhenWords = Truhe.UsedWords;
            Truhe.ExistingSpeechBubbles.Add(spr.transform);
            spr.GetComponent<Rigidbody2D>().gravityScale = 0;
            Hoelderlin.StartThrowing();
            Hoelderlin.SpeechBubble = spr.transform;
            randTimer = Random.Range(3f, 5f);
        }

        if(!gameOver && Truhe.UsedWords.Count >= 6)
        {
            gameOver = true;
            Hoelderlin.enabled = false;
            Truhe.enabled = false;
            StartCoroutine(EndGame());
        }
        else if(canEndGame)
        {
            if(Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                PlayerPrefs.SetString("MGameState", "won");
                SceneManager.LoadScene(1);
            }
        }

        timer += Time.deltaTime;
    }

    private IEnumerator EndGame()
    { 
        for(int i = 0; i < 100; i++)
        {
            FadeScreen.color = new Color(0, 0, 0, FadeScreen.color.a + 1 / 150f);
            yield return null;
        }
        var text = Instantiate(EndScreen, Vector3.zero, Quaternion.identity).GetComponentInChildren<Text>();
        text.text = "Ach! wo bist du, <b>Liebende</b>, nun? Sie haben mein Auge / \n" + 
            "Mir genommen, mein <b>Herz</b> hab ich verloren mit ihr. / \n " + 
            "Darum irr ich umher, und wohl, wie die <b>Schatten</b>, so muß ich / \n" + 
            "<b>Leben</b> und <b>sinnlos</b> dünkt lange das Übrige mir. / \n" +
            "Danken möcht ich, aber wofür? verzehret das Letzte / \n" + 
            "Selbst die <b>Erinnerung</b> nicht? nimmt von der Lippe denn nicht / \n" +
            "Bessere Rede mir der Schmerz, und lähmet ein Fluch nicht / \n" + 
            "Mir die Sehnen und wirft, wo ich beginne, mich weg? \n \n"
            + Truhe.PointsText.text;
        canEndGame = true;
    }

}
