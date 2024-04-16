using UnityEngine;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour
{
    // Create a public UnityEvent
    public UnityEvent onClick;

    void OnMouseDown()
    {
        // Invoke the UnityEvent
        Debug.Log("GameObject Clicked");
        onClick.Invoke();
    }

    private void Reset()
    {
        // Initialize the UnityEvent if it's null
        if (onClick == null)
        {
            onClick = new UnityEvent();
        }
    }
}
