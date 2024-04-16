using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizSceneController : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public QuizDatabase quizDatabase; // Assign this in the inspector

    private Question[] objectQuestions;
    private int questionIndex;

    void Start()
    {
        string objectName = GameManager.Instance.LastClickedObjectName;

        if (quizDatabase == null)
        {
            Debug.LogError("QuizDatabase not assigned in the inspector.");
            return;
        }

        objectQuestions = quizDatabase.GetQuestionsForObjectName(objectName);
        if (objectQuestions == null || objectQuestions.Length == 0)
        {
            Debug.LogError("No questions found for object: " + objectName);
            return;
        }

        questionIndex = GameManager.Instance.ObjectQuestionIndex.ContainsKey(objectName)
                        ? GameManager.Instance.ObjectQuestionIndex[objectName] : 0;

        SetupQuestion(objectQuestions[questionIndex]);
    }


    public void SetupQuestion(Question question)
    {
        questionText.text = question.questionText;

        // Assuming there are exactly 4 answer buttons and choices
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (i < question.answerChoices.Length)
            {
                buttonText.text = question.answerChoices[i];
                answerButtons[i].onClick.RemoveAllListeners();
                int index = i; // Local copy for correct closure capture
                answerButtons[i].onClick.AddListener(() => AnswerQuestion(index));
                answerButtons[i].gameObject.SetActive(true);
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false); // In case there are less than 4 choices
            }
        }
    }


    public void AnswerQuestion(int index)
    {
        Question currentQuestion = objectQuestions[questionIndex];

        if (PlayFabDataManager.Instance == null)
        {
            Debug.LogError("PlayFabDataManager is not found in the scene.");
            return;
        }

        if (index == currentQuestion.correctAnswerIndex)
        {
            // Correct answer
            Debug.Log("Correct Answer!");
            // Update score through PlayFabDataManager
            PlayFabDataManager.Instance.UpdateScore(currentQuestion.scoreValue);
            SceneManager.LoadScene("WorldScene");
            // Proceed with your game's logic
            // Load the next question or close the quiz
        }
        else
        {
            // Incorrect answer
            Debug.Log("Incorrect Answer. Try again!");
            // Handle incorrect answer feedback
        }
    }



    // ... Rest of your code
}
