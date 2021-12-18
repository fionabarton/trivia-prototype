using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public Text             questionText;
    public List<GameObject> answers;

    [Header("Set Dynamically")]
    // Index of the question currently displayed
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

        // Set up first question/answers
        SetQuestions();
    }

    // Set up current question/answers
    void SetQuestions() {
        // Get data for current question
        Question q = QuestionManager.S.questions[currentNdx];

        // Set question text
        questionText.text = q.questionString;

        // Set answers text
        for (int i = 0; i < answers.Count; i++) {
            answerTexts[i].text = q.answerStrings[i];
        }

        // Remove listeners from all answer buttons
        for (int i = 0; i < answerButtons.Count; i++) {
            answerButtons[i].onClick.RemoveAllListeners();
        }

        // Set OnClick methods
        for (int i = 0; i < answers.Count; i++) {
            if(i == q.correctAnswerNdx) {
                // If user has selected correct answer...
                answerButtons[i].onClick.AddListener(delegate { AnswerSelected("<color=green>" + "Correct answer!" + "</color>"); });
            } else {
                // If user has selected incorrect answer...
                answerButtons[i].onClick.AddListener(delegate { AnswerSelected("<color=red>" + "Incorrect answer!" + "</color>"); });
            }
        }

        // Make the answer buttons interactable
        for (int i = 0; i < answerButtons.Count; i++) {
            answerButtons[i].interactable = true;
        }

        // Set selected game object to first answer button
        //UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(answers[0].gameObject);
    }

    // Called when an answer button is selected by the user
    void AnswerSelected(string messageToDisplay) {
        // Set question text
        questionText.text = messageToDisplay;

        // Deactivate the interactability of the answer buttons
        for (int i = 0; i < answerButtons.Count; i++) {
            answerButtons[i].interactable = false;
        }

        // Increment currentNdx to display next question
        currentNdx = (currentNdx + 1) % QuestionManager.S.questions.Count;

        // Call SetQuestions() in approximately 2 seconds
        Invoke("SetQuestions", 2);
    }
}