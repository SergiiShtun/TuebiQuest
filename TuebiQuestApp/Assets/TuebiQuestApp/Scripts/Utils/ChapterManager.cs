using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Chapter
{
    public GeoPoint location;
    public Dictionary<int, PlainText> pTexts;
    public Dictionary<int, Event> events;
    public Dictionary<int, MiniGame> mGame;

}

public struct PlainText
{
    public string text;
    public string charID;
}

public struct Event
{
    public string text;
}

public struct MiniGame
{
    public string miniSpielID;
}

public class ChapterManager : MonoBehaviour
{
    

    public List<Chapter> Chapters { get; private set; }
    [HideInInspector]
    public int SelectedChapter;

    private int ChapterProgress = 1;
    private MainGameManager mgm;

    void Awake()
    {
        mgm = MainGameManager.Instance;
        Chapters = new List<Chapter>();
        if (GlobalGameManager.Instance != null)
            SelectedChapter = GlobalGameManager.Instance.SelectedChapter;
        else
            SelectedChapter = 0;
    }

    private void Start()
    {
        SelectedChapter = mgm.SaveLoadManager.CurrentChapter;
        ChapterProgress = mgm.SaveLoadManager.CurrentChapterProgress;
    }

    public void AddChapter(Chapter chapter)
    {
        Chapters.Add(chapter);
        //print(Chapters.Count);
    }

    public void Progress()
    {
        //print(ChapterProgress);
        if (SelectedChapter >= Chapters.Count)
            return;

        if(mgm.GlobalLocationManager.GetCurrentLocation().Distance(Chapters[SelectedChapter].location) > 0.1f)
        {
            print("Too Far away");
            return;
        }

        string won = PlayerPrefs.GetString("MGameState");

        if (won.Equals("won"))
        {
            ChapterProgress++;
            print("Hurra, you won the game.");
        }
        else if (won.Equals("lost"))
            print("Oh No, you lost.");

        PlayerPrefs.SetString("MGameState", "none");

        if (Chapters[SelectedChapter].pTexts.ContainsKey(ChapterProgress))
        {
            PlainText pText = Chapters[SelectedChapter].pTexts[ChapterProgress];
            //print("Text: " + pText.text);
            mgm.DisplayPlainText(pText.text, mgm.CharacterManager.Characters[pText.charID]);
        }
        else if (Chapters[SelectedChapter].events.ContainsKey(ChapterProgress))
        {

        }
        else if (Chapters[SelectedChapter].mGame.ContainsKey(ChapterProgress))
        {
            mgm.SaveLoadManager.SaveChapterProgress(SelectedChapter, 0, ChapterProgress);
            mgm.StartMiniGame(Chapters[SelectedChapter].mGame[ChapterProgress].miniSpielID);
            //print("Minigame " + Chapters[SelectedChapter].mGame[ChapterProgress].miniSpielID);
            //PlayerPrefs.SetString("MGameState", "won");
        }
        else
        {
            mgm.DisplayPlainText("Das Kapitel ist vorbei.", mgm.CharacterManager.Characters["AG"]);
            ChapterProgress = 0;
            SelectedChapter++;
        }

        ChapterProgress++;
    }

    

}
