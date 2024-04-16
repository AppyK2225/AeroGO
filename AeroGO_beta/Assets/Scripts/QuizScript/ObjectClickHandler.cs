using UnityEngine;
using UnityEngine.EventSystems; // Required for event handling interfaces
using UnityEngine.SceneManagement;

public class ObjectClickHandler : MonoBehaviour, IPointerClickHandler
{
    public string objectName; // Set this in the inspector for each object

    // This method is called when the object is clicked or tapped
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.LastClickedObjectName = objectName;
        int nextQuestionIndex = GameManager.Instance.GetCurrentQuestionIndex(objectName);
        GameManager.Instance.UpdateQuestionIndex(objectName, nextQuestionIndex);
        SceneManager.LoadScene("QuizScene");
    }
}
