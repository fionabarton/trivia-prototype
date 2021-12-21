using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public Text             questionHeaderText;
    public Text             questionText;
    public List<GameObject> answers;

    [Header("Set Dynamically")]
    // Index of the question currently being asked
    public int              currentNdx = 0;

    public List<Text>       answerTexts;
    public List<Button>     answerButtons;

    // Start is called before the first frame update
    void Start() {
        // Get Answer Text and Button components
        for (int i = 0; i < answers.Count; i++) {
            answerTexts[i] = answers[i].GetComponentInChildren<Text>();
            answerButtons[i] = answers[i].GetComponent<Button>();
        }

        // Set up and display first question/answers
        SetQuestions();
    }

    private void Update() {
        // Switch scenes/color palettes
        if (Input.GetKeyDown(KeyCode.Space)) {
            if(SceneManager.GetActiveScene().name == "Blue Palette") {
                SceneManager.LoadScene("Summer Palette");
            } else {
                SceneManager.LoadScene("Blue Palette");
            }
        }
    }

    // Set up and display current question/answers
    void SetQuestions() {
        // Get data for current question
        Question q = QuestionManager.S.questions[currentNdx];

        // Set question text
        questionHeaderText.text = "Question " + (currentNdx + 1);
        questionText.text = "\n" + q.questionString;

        // Set answers text
        for (int i = 0; i < answers.Count; i++) {
            answerTexts[i].text = q.answerStrings[i];
        }

        // Remove listeners from all answer buttons
        for (int i = 0; i < answerButtons.Count; i++) {
            answerButtons[i].onClick.RemoveAllListeners();
        }

        // Activate all the answer buttons
        for (int i = 0; i < answerButtons.Count; i++) {
            answerButtons[i].gameObject.SetActive(true);
        }

        // Set OnClick methods
        for (int i = 0; i < answers.Count; i++) {
            if(i == q.correctAnswerNdx) {
                // If user has selected correct answer, call ...
                answerButtons[i].onClick.AddListener(delegate { AnswerSelected("<color=#00FF00>" + "Correct answer!" + "</color>"); });
            } else {
                // If user has selected incorrect answer, call ...
                answerButtons[i].onClick.AddListener(delegate { AnswerSelected("<color=red>" + "Incorrect answer!" + "</color>"); });
            }
        }
    }

    // Called when an answer button is selected by the user
    void AnswerSelected(string messageToDisplay) {
        // Display feedback and info about the question/answer
        questionHeaderText.text = messageToDisplay;
        questionText.text = QuestionManager.S.questions[currentNdx].feedback;

        // Deactivate all but the first answer button
        for (int i = 1; i < answerButtons.Count; i++) {
            answerButtons[i].gameObject.SetActive(false);
        }

        // Set first answer button to be "Next question" button
        answerButtons[0].onClick.RemoveAllListeners();
        answerTexts[0].text = "Next question";
        answerButtons[0].onClick.AddListener(SetQuestions);

        // Increment currentNdx to display next question
        currentNdx = (currentNdx + 1) % QuestionManager.S.questions.Count;
    }
}