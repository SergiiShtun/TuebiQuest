using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{

    public GameObject HelpWindow;

    GlobalGameManager gm;
    float buestenTimer = 0;
    
    void Start()
    {
        gm = GlobalGameManager.Instance;
        Screen.orientation = ScreenOrientation.Portrait;
        GameObject.Find("StartGame").GetComponent<Button>().onClick.AddListener(delegate { gm.LoadScene("Minigames"); });
        //HelpWindow.GetComponentInChildren<Button>().onClick.AddListener(delegate { ToggleHelpWindow(); });
        //GameObject.Find("Help").GetComponent<Button>().onClick.AddListener(delegate { ToggleHelpWindow(); });
        GameObject.Find("MiniGames").GetComponent<Button>().onClick.AddListener(delegate { gm.LoadScene("Minigames"); });
        //print("Button Func" + GlobalGameManager.Instance);
    }

    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            buestenTimer += Time.deltaTime;
            if (buestenTimer > 2)
                SceneManager.LoadScene(10);
        }
    }
    public void ToggleHelpWindow()
    {
        if(HelpWindow.activeInHierarchy)
        {
            HelpWindow.SetActive(false);
        }
        else
        {
            HelpWindow.SetActive(true);
            HelpWindow.GetComponent<BouncePulse>().StartAnimating();
        }
    }
}
