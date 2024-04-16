using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class ShowHideCycle : MonoBehaviour
{
    public GameObject[] objectsToCycle;
    private int currentIndex = 0;

    void Start()
    {
        // Initialize by hiding all objects except the first one
        for (int i = 0; i < objectsToCycle.Length; i++)
        {
            if (objectsToCycle[i] != null)
            {
                objectsToCycle[i].SetActive(i == 0);
            }
        }

        // Add click event listener to the button
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(CycleObjects);
        }
        else
        {
            Debug.LogError("CycleObjectsOnClick script is not attached to a GameObject with a Button component.");
        }
    }

    private void CycleObjects()
    {
        // Hide current object
        if (objectsToCycle[currentIndex] != null)
        {
            objectsToCycle[currentIndex].SetActive(false);
        }

        // Move to the next object, wrapping around if necessary
        currentIndex = (currentIndex + 1) % objectsToCycle.Length;

        // Show next object
        if (objectsToCycle[currentIndex] != null)
        {
            objectsToCycle[currentIndex].SetActive(true);
        }
    }
}
