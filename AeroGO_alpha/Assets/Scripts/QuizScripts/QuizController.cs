using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizController : MonoBehaviour
{
    private QuestionCollection questionCollection;
    private QuizQuestion currentQuestion;
    private QuizManager quizManager;

    private int totalScore; // Store total score

    [SerializeField]
    private float delayBetweenQuestions = 3f;

    private void Awake()
    {
        questionCollection = FindObjectOfType<QuestionCollection>();
        quizManager = FindObjectOfType<QuizManager>();
    }

    private void Start()
    {
        PresentQuestion();
    }

    private void PresentQuestion()
    {
        currentQuestion = questionCollection.GetUnaskedQuestion();
        quizManager.SetupUIForQuestion(currentQuestion);
    }

    public void SubmitAnswer(int answerNumber)
    {
        bool isCorrect = answerNumber == currentQuestion.CorrectAnswer;
        quizManager.HandleSubmittedAnswer(isCorrect);

        if (isCorrect)
        {
            UpdateScore(currentQuestion.Score); // Update score if correct
        }

        // Instead of calling ShowNextQuestionAfterDelay, we now call a new coroutine to handle the scene switch
        StartCoroutine(SwitchToSceneAfterDelay("World", delayBetweenQuestions));
    }

    // This coroutine waits for the specified delay, then switches to the specified scene
    private IEnumerator SwitchToSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }


    private void UpdateScore(int scoreToAdd)
    {
        // Retrieve the current accumulated score, default to 0 if not found
        int accumulatedScore = PlayerPrefs.GetInt("PlayerScore", 0);

        // Add the new score to the accumulated score
        accumulatedScore += scoreToAdd;

        // Save the updated score back to PlayerPrefs
        PlayerPrefs.SetInt("PlayerScore", accumulatedScore);
        PlayerPrefs.Save(); // Ensure PlayerPrefs is saved

        // Update the UI with the new score (if applicable)
        quizManager.UpdateScoreUI(accumulatedScore);
    }


    private IEnumerator ShowNextQuestionAfterDelay()
    {
        yield return new WaitForSeconds(delayBetweenQuestions);
        PresentQuestion();
    }
}