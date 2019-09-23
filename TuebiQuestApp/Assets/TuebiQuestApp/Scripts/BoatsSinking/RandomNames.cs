using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomNames : MonoBehaviour
{
    // Sprechfeld objekt braucht ein textcomponent
    // Um das TextComponent anzuzeigen muss es in einem Canvas gerendert werden, dazu kannst du einen Canvas in die 
    // Szene setzen, auf render in WorldSpace setzen und die Sprechblasen als kinder vom Canvas initialisieren.

    public GameObject SpeechField;
    public Transform SpeechCanvas;

    private List<string> rightTexts = new List<string>()
    {
        "Hallo",
        "das",
        "ist",
        "ein",
        "Text."
    };

    private List<string> incorrectTexts = new List<string>()
    {
        "Tschüss",
        "dies",
        "kann",
        "kein",
        "Name",
        "sein"
    };


    void InstantiateRandomText()
    {
        // Sprechfeld initialisieren und referenz auf das TextComponent setzen
        Text text = Instantiate(SpeechField).GetComponent<Text>();

        // Sprechfeld als kind vom Canvas setzen
        text.transform.parent = SpeechCanvas;

        // Den Text auswählen:
        string chosenText;
        if (Random.Range(0.0f, 100.0f) < 50.0f) // 50% Wahrscheinlichkeit für korrekten text
        {
            chosenText = rightTexts[Random.Range(0, rightTexts.Count)];
        }
        else
        {
            chosenText = incorrectTexts[Random.Range(0, incorrectTexts.Count)];
        }

        text.text = chosenText;
    }

    // Wenn die Sprechblase eingesammelt ist kannst du den korrekten text aus der Liste entfernen:
    // Das kannst du auch in dem Script von der Truhe machen, bei OnTrigerEnter
    void OnRightTextCollected(string correctText)
    {
        rightTexts.Remove(correctText);
    }
}
