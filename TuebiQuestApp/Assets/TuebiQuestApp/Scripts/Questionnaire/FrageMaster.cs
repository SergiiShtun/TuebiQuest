using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;

public struct Frage
{
    public string Question;
    public string[] RightAnswer;
    public string[] FalseAnswer;

    // Number of right answers, so position can be random
    public int RightNumberOfAnswers;
    public List<int> RightNums;

    public Frage(string q, string[] rA, string[] fA, int rN)
    {
        Question = q;
        RightAnswer = rA;
        FalseAnswer = fA;
        RightNumberOfAnswers = rN;
        RightNums = new List<int>();
        for(int i = 0; i < RightNumberOfAnswers; i++)
        {
            int rand = Random.Range(0, 4);
            while (RightNums.Contains((rand = Random.Range(0, 4))));
            RightNums.Add(rand);
        }
    }
}

public class FrageMaster : MonoBehaviour
{
    public static FrageMaster Instance;

    public Transform FrageUICanvas;
    public float MaxTime;
    public Text AnswerFeedbackText;
    public Text TimerText;
    public Button fiftyButton;
    public Button internetButton;
    public Button triviaButton;


    public Transform GameCanvas;
    public Image QuestionImage;
    public int currentAnswerIndex;
    Color greenColor = new Color(0, 1, 0, 1.0f);
    Color redColor = new Color(1, 0, 0, 1.0f);
    Color defaultColor;
    public GameObject slot;
    [SerializeField]
    private Text hintText;
    [SerializeField]
    private Row[] rows;
    public Text levelText;
    private bool resultsChecked;
    private int hint = 1;
    public static int rightAnswers = 10;

    /// <summary>
    /// Progression overall in the game
    /// </summary>
    public int currentLevel;
    public int currentPoints;
    /// <summary>
    /// Kind of questions that will be asked 1, 2 or 3
    /// </summary>
    public int currentTier;

    private int currentQuestionIndex;

    private List<Frage>[] tierQuestions;
    private List<int>[] tierAsked;

    private float timer;
    private Frage currentFrage;
    private bool gameOver;

    List<int> wrongAnswers;

    void Start()
    {
        slot.SetActive(false);
        Instance = this;
        timer = MaxTime;
        defaultColor = GameCanvas.GetChild(1).GetChild(0).GetComponent<Image>().color;

        ReadXmlQuestion();

        if(PlayerPrefs.GetString("Internet") == "Active")
        {
            currentLevel = PlayerPrefs.GetInt("Level");
            currentPoints = PlayerPrefs.GetInt("Points");
            currentTier = PlayerPrefs.GetInt("Tier");
            fiftyButton.interactable = PlayerPrefs.GetInt("fifty") == 0;
            internetButton.interactable = PlayerPrefs.GetInt("internet") == 0;
            triviaButton.interactable = PlayerPrefs.GetInt("trivia") == 0;
            AnswerFeedbackText.text = "\nFrage " + (currentLevel + 1) + "\nLevel " + (currentTier + 1);
        }

        PlayerPrefs.SetString("Internet", "Inactive");
        NewQuestion();
    }

    void Update()
    {
        if (timer < 0)
        {
            print("GameOver");
            AnswerFeedbackText.text = "Zu lange.";
            PlayerPrefs.SetString("MGameState", "lost");
            PlayerPrefs.SetString("Internet", "Inactive");
            currentLevel -= 2;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        timer -= Time.deltaTime;
        TimerText.text = (timer).ToString().Split('.')[0];
        levelText.text = (currentTier + 1).ToString() + "/" + currentLevel.ToString();
        hintText.text = "Hints:\n" + rightAnswers;
        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            resultsChecked = false;
        }

        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked && rows[2].stoppedSlot != "" && rows[1].stoppedSlot != "" && rows[0].stoppedSlot != "") 
        {
            CheckResults();
        }
    }

    private void CheckResults()
    {
        if (rows[0].stoppedSlot == rows[1].stoppedSlot &&
            rows[1].stoppedSlot == rows[2].stoppedSlot && rows[0].stoppedSlot != "")
        {
            hint = 3;
        }
            
        else if (rows[0].stoppedSlot == rows[1].stoppedSlot || rows[1].stoppedSlot == rows[2].stoppedSlot && rows[0].stoppedSlot != "")
            hint = 2;
        else if (rows[0].stoppedSlot == rows[2].stoppedSlot && rows[0].stoppedSlot != "")
            hint = 1;
        else
            hint = 0;

        removeQuestions(hint);
        hintText.text = "Hints:\n" + rightAnswers;
        resultsChecked = true;
        StartCoroutine(waitNext());
    }
    private void removeQuestions(int numberOfHints)
    {
        List<int> numberList = new List<int> { 1, 2, 3, 4 };
        int toRemove = currentFrage.RightNums[0] + 1;
        
        numberList.Remove(toRemove);
        for (int i = 0; i < numberOfHints; i++)
        {
            int index = numberList[2-i];
            var txt = GameCanvas.GetChild(index).GetComponentInChildren<Text>();
            txt.enabled = false;
            numberList.Remove(index);
        }
    }

    void NewQuestion()
    {
        wrongAnswers = new List<int>();
        for (int i = 1; i < 5; i++)
            FrageUICanvas.GetChild(i).gameObject.SetActive(true);

        currentQuestionIndex = Random.Range(0, tierQuestions[currentTier].Count);
        while (tierAsked[currentTier].Contains(currentQuestionIndex))
            currentQuestionIndex = Random.Range(0, tierQuestions[currentTier].Count);

        currentFrage = tierQuestions[currentTier][currentQuestionIndex];

        FrageUICanvas.GetChild(0).GetComponentInChildren<Text>().text = currentFrage.Question;

        GameCanvas.GetChild(0).GetComponentInChildren<Text>().text = currentFrage.Question;

        List<int> freeNums = new List<int>() { 1, 2, 3, 4 };
        for (int i = 0; i < currentFrage.RightNumberOfAnswers; i++)
        {
            int rightIndex = currentFrage.RightNums[i] + 1;
            FrageUICanvas.GetChild(rightIndex).GetComponentInChildren<Text>().text = currentFrage.RightAnswer[i];
            FrageUICanvas.GetChild(rightIndex).GetComponent<Answer>().correct = true;

            GameCanvas.GetChild(rightIndex).GetComponentInChildren<Text>().enabled = true;
            GameCanvas.GetChild(rightIndex).GetComponentInChildren<Text>().text = currentFrage.RightAnswer[i];
            GameCanvas.GetChild(rightIndex).GetComponent<Answer>().correct = true;
            GameCanvas.GetChild(rightIndex).GetChild(0).GetComponent<Image>().color = defaultColor;
            currentAnswerIndex = rightIndex;

            freeNums.Remove(rightIndex);
        }

        for (int i = 0; i < 4 - currentFrage.RightNumberOfAnswers; i++)
        {
            FrageUICanvas.GetChild(freeNums[i]).GetComponentInChildren<Text>().text = currentFrage.FalseAnswer[i];
            FrageUICanvas.GetChild(freeNums[i]).GetComponent<Answer>().correct = false;

            GameCanvas.GetChild(freeNums[i]).GetComponentInChildren<Text>().enabled = true;
            GameCanvas.GetChild(freeNums[i]).GetComponentInChildren<Text>().text = currentFrage.FalseAnswer[i];
            GameCanvas.GetChild(freeNums[i]).GetComponent<Answer>().correct = false;
            GameCanvas.GetChild(freeNums[i]).GetChild(0).GetComponent<Image>().color = defaultColor;

            wrongAnswers.Add(freeNums[i]);
        }
    }

    public void Answered(bool correct, string index)
    {
        var ind = Convert.ToInt32(index);
        if (correct)
        {
            tierAsked[currentTier].Add(currentQuestionIndex); 
            currentLevel++;
            currentTier = currentLevel / 5;
            var img = GameCanvas.GetChild(ind).GetChild(0).GetComponent<Image>();
            img.color = greenColor;
            Debug.Log(GameCanvas.GetChild(ind).GetChild(0).GetComponent<Image>().color);
            AnswerFeedbackText.text = "Richtig!\nFrage " + (currentLevel + 1) + "\nLevel " + (currentTier + 1);
            if (currentTier > 0 && (currentLevel % 2) == 0)
            {
                rightAnswers += 1;
            }
            else if ((currentTier == 0 && currentLevel >= 5) || (currentTier == 1 && currentLevel >= 10))
                rightAnswers += 1;
            hintText.text = "Hints:\n" + rightAnswers;
            StartCoroutine(correctLoadNext());
        }
        else
        {
            var img = GameCanvas.GetChild(ind).GetChild(0).GetComponent<Image>();
            img.color = redColor;
            AnswerFeedbackText.text = "Ne, du.";
            StartCoroutine(loadNext());
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetString("MGameState", "lost");
        }
    }

    void ReadXmlQuestion()
    {
        tierQuestions = new List<Frage>[3];
        tierAsked = new List<int>[3];
        tierQuestions[0] = new List<Frage>();
        tierQuestions[1] = new List<Frage>();
        tierQuestions[2] = new List<Frage>();
        tierAsked[0] = new List<int>();
        tierAsked[1] = new List<int>();
        tierAsked[2] = new List<int>();

        string xmlDocumentName = "Fragen1";
        TextAsset xmlAsset = (TextAsset)Resources.Load(xmlDocumentName, typeof(TextAsset));

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlAsset.text);

        foreach (XmlNode questionNode in xmlDocument.GetElementsByTagName("question"))
        {
            string question = questionNode.FirstChild.InnerText;
            int tier = int.Parse(questionNode.Attributes["level"].Value) - 1;
            List<string> correctAnswer = new List<string>();
            List<string> falseAnswers = new List<string>();
            foreach (XmlNode answerNode in questionNode.ChildNodes)
            {
                if (answerNode.Name == "antwort")
                {
                    if (answerNode.Attributes["type"].Value == "w")
                        correctAnswer.Add(answerNode.InnerText);
                    else
                        falseAnswers.Add(answerNode.InnerText);
                }
            }
            tierQuestions[tier].Add(new Frage(
                        question,
                        correctAnswer.ToArray(),
                        falseAnswers.ToArray(),
                        correctAnswer.Count
                ));
        }

    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Debug.Log("Paused");
            
        }
        else
        {
            Debug.Log("resumed");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    IEnumerator correctLoadNext()
    {
        yield return new WaitForSeconds(3.0f);
        PlayerPrefs.SetString("Internet", "Inactive");
        timer = MaxTime;
        if (currentLevel <= 15)
            NewQuestion();
        else
        {
            PlayerPrefs.SetString("MGameState", "won");
            SceneManager.LoadScene("Intro");
        }
    }

    IEnumerator loadNext()
    {
        yield return new WaitForSeconds(2.0f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (currentLevel > 2 && currentTier > 0)
        {
            currentLevel -= 2;
            currentTier = currentLevel / 5;
        }
        else if(currentLevel > 2 && currentTier == 0)
        {
            currentLevel -= 2;
            currentTier = currentLevel / 5;
        }
        else
        {
            currentLevel = 0;
            currentTier = 0;
        }     

        PlayerPrefs.SetString("Internet", "Inactive");
        timer = MaxTime;
        if (currentLevel <= 15)
            NewQuestion();
        else
        {
            PlayerPrefs.SetString("MGameState", "won");
            SceneManager.LoadScene("Intro");
        }
    }

    IEnumerator waitNext()
    {
        yield return new WaitForSeconds(2.0f);
        slot.SetActive(false);
    }
}
