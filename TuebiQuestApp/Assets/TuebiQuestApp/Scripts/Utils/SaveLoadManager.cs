using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public int CurrentChapter { get; private set; }
    public int CurrentPoints { get; private set; }
    public int CurrentChapterProgress { get; private set; }


    void Awake()
    {
        LoadChapterProgres();
    }
    
    public void SaveChapterProgress(int currentChapter, int currentPoints, int currentChapterProgress)
    {
        CurrentChapter = currentChapter;
        CurrentPoints = currentPoints;
        CurrentChapterProgress = currentChapterProgress;

        PlayerPrefs.SetInt("chapter", CurrentChapter);
        PlayerPrefs.SetInt("points", CurrentPoints);
        PlayerPrefs.SetInt("progress", CurrentChapterProgress);

    }

    public void ResetSave()
    {
        PlayerPrefs.SetInt("chapter", 0);
        PlayerPrefs.SetInt("points", 0);
        PlayerPrefs.SetInt("progress", 0);
    }

    public void LoadChapterProgres()
    {
        CurrentChapter = PlayerPrefs.GetInt("chapter");
        CurrentPoints = PlayerPrefs.GetInt("points");
        CurrentChapterProgress = PlayerPrefs.GetInt("progress");

        // ToDo Delete
        if (CurrentChapterProgress == 0)
            CurrentChapterProgress = 1;
    }

}
