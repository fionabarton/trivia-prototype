using System.Collections.Generic;
using UnityEngine;

public enum eTopic { plasticPollution, deforestation, airPollution, climateChange, other };

public class QuestionManager : MonoBehaviour {
    [Header("Set Dynamically")]
    private static QuestionManager  _S;
    public static QuestionManager   S { get { return _S; } set { _S = value; } }

    public List<Question>           questions = new List<Question>();

    void Awake() {
        S = this;
    }

    // Start is called before the first frame update
    void Start() {
        questions.Add(
            new Question("Which of the following companies\ngenerates the most plastic pollution?",
            new List<string> { "Coca-Cola", "PepsiCo", "Nestlé ", "Unilever" },
            eTopic.plasticPollution, 0));

        questions.Add(
            new Question("What colour is the sky?",
            new List<string> { "Dog", "Apple", "Dragon Ball", "Blue" },
            eTopic.other, 3));

        questions.Add(
            new Question("Which of the following people\nis NOT a member of our team?",
            new List<string> { "Robocop", "Davina", "Siddhant", "Tertius" },
            eTopic.other, 0));

        questions.Add(
            new Question("What date is our midterm\nproposal due?",
            new List<string> { "10 January 2021", "2 September 1945", "10 January 2022", "4 December 1985" },
            eTopic.other, 2));
    }
}

public class Question {
    public string       questionString;
    public List<string> answerStrings;

    public eTopic       topic;
    public int          correctAnswerNdx;

    public bool         answeredCorrectly;

    public Question(string _question, List<string> _answers, eTopic _topic, int _correctAnswerNdx) {
        questionString = _question;
        answerStrings = _answers;
        topic = _topic;
        correctAnswerNdx = _correctAnswerNdx;
    }
}