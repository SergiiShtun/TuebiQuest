using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sprechblase : MonoBehaviour {
    
    public Transform SpeechCanvas;

    public List<string> TruhenWords;

    private List<string> rightTexts = new List<string>() {
        "Erinnerung", "Herz", "Leben", "Liebende", "sinnlos", "Schatten"
    };

    private List<string> incorrectTexts = new List<string>()
    {
        "Erinerung", "Erinnerun", "Erimerungg", "Rinrung", "Erihnerung",
        "Hertz", "Härz", "Hehrz", "Hrz", "Herrz",    
        "Lebhen", "Lebben", "Lebn", "Lbn", "Lehben",                                               
        "Liepende", "Liebente", "Libende", "Lbnde", "Liehbende",                                               
        "sinlos", "snlos", "snls", "sinloss", "sienlos",
        "Schaaten", "Schadden", "Schaten", "Schttn", "Shatten"
    };

   
	// Use this for initialization
	void Start () {
        InstantiateRandomText();
        transform.SetParent(transform.Find("Canvas"));

        //GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3f, 0), Random.Range(5f, 0));
        Destroy(gameObject, 10f);
	}
	
    void InstantiateRandomText()
    {
        // Sprechfeld initialisieren und referenz auf das TextComponent setzen
        Text text = GetComponentInChildren<Text>();
        // Den Text auswählen:
        string chosenText = "";
        if (Random.Range(0.0f, 100.0f) < 50.0f) // 50% Wahrscheinlichkeit für korrekten text
        {
            foreach(string tW in TruhenWords)
                rightTexts.Remove(tW);
            if(rightTexts.Count > 0)
                chosenText = rightTexts[Random.Range(0, rightTexts.Count)];
            tag = "Sprechblase";

        }
        else
        {
            chosenText = incorrectTexts[Random.Range(0, incorrectTexts.Count)];
            tag = "SprechblaseFalsch";

        }

        text.text = chosenText;
    }
}
