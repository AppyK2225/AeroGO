using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public QuizDatabase quizDatabase;
    public ObjectPrefabDatabase objectPrefabDatabase; // Added to access object models
    public Transform modelSpawnLocation;  // Location to spawn the object model
    public TextMeshProUGUI questionTextUI;
    public Button[] answerButtons;
    private Question currentQuestion;
    private string currentObjectName;

    void Start()
    {
        StartCoroutine(InitializeQuizAfterDataLoad());
    }

    IEnumerator InitializeQuizAfterDataLoad()
    {
        // Wait for the PlayFab data to be loaded, if necessary
        while (!PlayFabDataManager.Instance.IsDataLoaded)
        {
            yield return null;
        }

        // Proceed with initializing the quiz
        InitializeQuiz();
    }

    private void InitializeQuiz()
    {
        currentObjectName = GameManager.Instance.LastClickedObjectName;
        int currentQuestionIndex = GameManager.Instance.GetCurrentQuestionIndex(currentObjectName);
        Question[] questionsForCurrentObject = quizDatabase.GetQuestionsForObjectName(currentObjectName);

        // Load and display the object model
        GameObject objectModel = objectPrefabDatabase.GetPrefabByName(currentObjectName);
        if (objectModel != null)
        {
            Instantiate(objectModel, modelSpawnLocation.position, Quaternion.identity, modelSpawnLocation);
        }
        else
        {
            Debug.LogError("Object model not found for: " + currentObjectName);
        }

        if (questionsForCurrentObject != null && currentQuestionIndex < questionsForCurrentObject.Length)
        {
            currentQuestion = questionsForCurrentObject[currentQuestionIndex];
            LoadQuestion(currentQuestion);
        }
        else
        {
            Debug.LogError("No questions found or index out of bounds.");
            SceneManager.LoadScene("WorldScene");
        }
    }

    void LoadQuestion(Question question)
    {
        questionTextUI.text = question.questionText;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.answerChoices[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(i));
        }
    }

    public void OnAnswerSelected(int selectedAnswerIndex)
    {
        if (selectedAnswerIndex == currentQuestion.correctAnswerIndex)
        {
            Debug.Log("Correct answer!");
            PlayFabDataManager.Instance.UpdateScore(currentQuestion.scoreValue);
            GameManager.Instance.UpdateQuestionIndex(currentObjectName, GameManager.Instance.GetCurrentQuestionIndex(currentObjectName) + 1);
        }
        else
        {
            Debug.Log("Incorrect Answer. Try again next time!");
        }

        StartCoroutine(LoadNextSceneAfterDelay());
    }

    IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(2);  // Delay for 2 seconds before switching scene
        SceneManager.LoadScene("WorldScene");
    }
}
