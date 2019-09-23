using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance;

    public AVXmlLoader XmlLoader { get; private set; }
    public ChapterManager ChapterManager { get; private set; }
    public RandomFactManager RandomFactManager { get; private set; }
    public CharacterManager CharacterManager { get; private set; }
    public AssetManager AssetManager { get; private set; }
    public SaveLoadManager SaveLoadManager { get; private set; }
    public GlobalLocationScript GlobalLocationManager { get; private set; }

    public VuforiaMonoBehaviour ArCamera;
    public Transform PlainTextContainer;

    public int CurrentPoints;
    // Name of the Xml File
    public string CurrentSelectedQuest;

    void Awake()
    {
        Instance = this;

        XmlLoader = GetComponent<AVXmlLoader>();
        ChapterManager = GetComponent<ChapterManager>();
        RandomFactManager = GetComponent<RandomFactManager>();
        CharacterManager = GetComponent<CharacterManager>();
        AssetManager = GetComponent<AssetManager>();
        SaveLoadManager = GetComponent<SaveLoadManager>();
        GlobalLocationManager = GetComponent<GlobalLocationScript>();

        CurrentSelectedQuest = "NeckarbrueckeRoute";
        //SaveLoadManager.ResetSave();
    }


    void Update()
    {
        if (AVInputHandler.PointerDown() && AVInputHandler.PointerPosition().y > 600 && AVInputHandler.PointerPosition().y < 1600)
        {
            
            ChapterManager.Progress();
        }
    }

    public void ToggleCamera()
    {
        ArCamera.enabled = !ArCamera.enabled; 
    }

    //Only for reset save button, delete later
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DisplayPlainText(string rText, GameObject rChar)
    {
        PlainTextContainer.gameObject.SetActive(true);
        PlainTextContainer.GetChild(1).GetComponent<Image>().sprite = rChar.GetComponent<Image>().sprite;
        PlainTextContainer.GetChild(1).GetComponent<BouncePulse>().StartAnimating();
        PlainTextContainer.GetChild(3).GetComponentInChildren<Text>().text = rText;
    }

    public void StartEvent(string eID)
    {

    }

    public void DisplayRandomFact(string fact)
    {

    }

    public void StartMiniGame(string mID)
    {
        switch (mID)
        {
            case "Wortschatz":
                SceneManager.LoadScene(2);
                break;
            case "Stocherkahnversenken":
                SceneManager.LoadScene(3);
                break;
            case "Baerenfuetterungsspiel":
                SceneManager.LoadScene(4);
                break;
            case "Ebarhard_im_Bart":
                SceneManager.LoadScene(5);
                break;
            case "Blumen_sammeln":
                SceneManager.LoadScene(6);
                break;
            case "Schlossspiel":
                SceneManager.LoadScene(7);
                break;
            case "Rathausspiel":
                SceneManager.LoadScene(11);
                break;
            case "Lustnauer_Tor":
                SceneManager.LoadScene(9);
                break;
            case "Augustusspiel":
                print("Minigame Augustusspiel");
                PlayerPrefs.SetString("MGameState", "won");
                SceneManager.LoadScene(1);
                break;
            case "Skulpturen":
                SceneManager.LoadScene(10);
                break;
            case "WWM-Quiz":
                SceneManager.LoadScene(7);
                break;

            default: break;
        }
    }

}
