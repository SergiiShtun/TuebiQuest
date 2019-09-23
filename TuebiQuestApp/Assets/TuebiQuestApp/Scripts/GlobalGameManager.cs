using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameManager : MonoBehaviour {

    public static GlobalGameManager Instance;

    public int SelectedChapter;

    private float MiniGameSkipTime = 5;
    private float mGameTimer;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(MainGameManager.Instance != null)
            {
                MainGameManager.Instance.SaveLoadManager.ResetSave();
            }
            if (SceneManager.GetActiveScene().buildIndex > 0)
                SceneManager.LoadScene(0);
            else
                Application.Quit();
        }
        else if (Input.GetKey(KeyCode.K))
        {
            if (SceneManager.GetActiveScene().buildIndex > 1)
            {
                PlayerPrefs.SetString("MGameState", "won");
                SceneManager.LoadScene(1);
            }
        }
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            mGameTimer += Time.deltaTime;
            if (mGameTimer > MiniGameSkipTime)
            {
                PlayerPrefs.SetString("MGameState", "won");
                SceneManager.LoadScene(1);
            }
        }
        else
            mGameTimer = 0;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
