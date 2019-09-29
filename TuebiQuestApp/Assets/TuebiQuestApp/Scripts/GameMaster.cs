using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    MiniGames,
    MiniGameDescription,
    GameScreen
}

public class GameMaster : MonoBehaviour {

    public static GameMaster Instance;

    public GameObject[] GameScreens;
    public Language DefaultLanguage;
    public Language SelectedLanguage { get; private set; }

    private int selectedGameI;
    private string selectedGameS;

    private void Start()
    {
        Instance = this;
        if (SelectedLanguage != DefaultLanguage)
            SelectedLanguage = SelectedLanguage == Language.en ? Language.de : Language.en;
       // SetGameState(0);
    }

    public void SetGameState(int state)
    {
        foreach (GameObject screen in GameScreens)
            screen.SetActive(false);

        switch(state)
        {
            default:
            case (int)GameState.Intro:
                GameScreens[0].SetActive(true);
                break;
            case (int)GameState.MiniGames:
                GameScreens[1].SetActive(true);
                break;
            case (int)GameState.MiniGameDescription:
                GameScreens[2].SetActive(true);
                break;
            case (int)GameState.GameScreen:
                GameScreens[3].SetActive(true);
                GameScreens[4].SetActive(true);
                break;
        }
    }

    public void ChangeLanguage()
    {
        SelectedLanguage = SelectedLanguage == Language.en ? Language.de : Language.en;
        foreach(LanguageObjects lo in FindObjectsOfType<LanguageObjects>())
        {
            lo.UpdateText();
        }
    }

    public void SetMiniGame(int game)
    {
        selectedGameI = game;
    }

    public void SetMiniGame(string game)
    {
        selectedGameS = game;
    }

    public void StartMiniGame()
    {
        SceneManager.LoadScene(selectedGameI);
    }

    public void StartMiniGameS()
    {
        SceneManager.LoadScene(selectedGameS);
    }

}
