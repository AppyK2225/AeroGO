using UnityEngine;
using UnityEngine.Events; // Required to use UnityEvent
using UnityEngine.SceneManagement; // Required for scene management

public class ClickEventer : MonoBehaviour
{
    public UnityEvent onMouseDownEvent;
    public string objectName; // Set this in the Inspector for each object

    void OnMouseDown()
    {
        Debug.Log("GameObject clicked!");
        onMouseDownEvent?.Invoke();
    }

    public void ClickScore()
    {
        GameObject scoreManager = GameObject.FindWithTag("PlayFabScoreManager");
        if (scoreManager != null)
        {
            var scoreScript = scoreManager.GetComponent<PlayFabScoreManager>(); // Assume the script is correctly named and attached
            if (scoreScript != null)
            {
                scoreScript.AddScore(10); // Call AddScore with a specific value
            }
            else
            {
                Debug.LogError("AddScore method not found on the PlayFabScoreManager GameObject!");
            }
        }
        else
        {
            Debug.LogError("PlayFabScoreManager GameObject not found!");
        }
    }

    public void SceneChange()
    {
        GameManager.Instance.LastClickedObjectName = objectName;
        int nextQuestionIndex = GameManager.Instance.GetCurrentQuestionIndex(objectName);
        GameManager.Instance.UpdateQuestionIndex(objectName, nextQuestionIndex);
        SceneManager.LoadScene("QuizScene");
        Debug.LogError("scen wants to change");
    }
}
