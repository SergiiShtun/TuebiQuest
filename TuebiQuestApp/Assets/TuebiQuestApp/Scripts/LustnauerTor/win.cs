using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class win : MonoBehaviour {
    public Text winwin;
    public Text timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        winwin.text = "Wow, you won";
        points.counter += 80;
        StartCoroutine(Exit());
    }

    IEnumerator Exit()
    {
        timer.text = "WIN WIN";
        yield return new WaitForSeconds(5);
        Application.LoadLevel(Application.loadedLevel);
    }

}
