using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            while (RightNums.Contains((rand = Random.Range(0, 4)))) ;
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

    private bool fiftyFifty;
    private bool trivia;
    private bool internet;
    List<int> wrongAnswers;

    void Start()
    {
        Instance = this;
        timer = MaxTime;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        timer -= Time.deltaTime;
        TimerText.text = (timer).ToString().Split('.')[0];
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

        List<int> freeNums = new List<int>() { 1, 2, 3, 4 };

        for (int i = 0; i < currentFrage.RightNumberOfAnswers; i++)
        {
            int rightIndex = currentFrage.RightNums[i] + 1;
            FrageUICanvas.GetChild(rightIndex).GetComponentInChildren<Text>().text = currentFrage.RightAnswer[i];
            FrageUICanvas.GetChild(rightIndex).GetComponent<Answer>().correct = true;
            freeNums.Remove(rightIndex);
        }

        for (int i = 0; i < 4 - currentFrage.RightNumberOfAnswers; i++)
        {
            FrageUICanvas.GetChild(freeNums[i]).GetComponentInChildren<Text>().text = currentFrage.FalseAnswer[i];
            FrageUICanvas.GetChild(freeNums[i]).GetComponent<Answer>().correct = false;
            wrongAnswers.Add(freeNums[i]);
        }
    }

    public void Answered(bool correct)
    {
        if (correct)
        {
            tierAsked[currentTier].Add(currentQuestionIndex); 
            currentLevel++;
            currentTier = currentLevel / 5;
            AnswerFeedbackText.text = "Richtig!\nFrage " + (currentLevel + 1) + "\nLevel " + (currentTier + 1);
        }
        else
        {

            AnswerFeedbackText.text = "Ne, du.";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetString("MGameState", "lost");
        }
        PlayerPrefs.SetString("Internet", "Inactive");
        timer = MaxTime;
        if(currentLevel <= 15)
            NewQuestion();
        else
        {
            PlayerPrefs.SetString("MGameState", "won");
            SceneManager.LoadScene("Intro");
        }
    }

    public void FiftyFifty()
    {
        if (fiftyFifty)
            return;
        for(int i = 0; i < 2; i++)
        {
            int rndWrongAnswer = wrongAnswers[Random.Range(0, wrongAnswers.Count)];
            FrageUICanvas.GetChild(rndWrongAnswer).gameObject.SetActive(false);
            wrongAnswers.Remove(rndWrongAnswer);
        }
        fiftyFifty = true;
    }

    public void Internet()
    {
        if (internet)
            return;
        internet = true;
        PlayerPrefs.SetString("Internet", "Active");
        PlayerPrefs.SetInt("Tier", currentTier);
        PlayerPrefs.SetInt("Level", currentLevel);
        PlayerPrefs.SetInt("Points", currentPoints);
        PlayerPrefs.SetInt("fifty", fiftyFifty ? 1 : 0);
        PlayerPrefs.SetInt("internet", internet ? 1 : 0);
        PlayerPrefs.SetInt("trivia", trivia ? 1 : 0);
    }

    public void Trivia()
    {
        if (trivia)
            return;
        trivia = true;
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
            string question = questionNode.InnerText;
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

}
