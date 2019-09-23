using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour {

    public bool MiniGameWon;
    
    public void SetMiniGameWon(bool won)
    {
        MiniGameWon = won;
    }

    public void EndMiniGame()
    {
        PlayerPrefs.SetString("MGameState", MiniGameWon ? "won" : "lost");
        SceneManager.LoadScene(1);
    }

}
