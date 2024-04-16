using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class ShowHideToggle : MonoBehaviour
{
    public GameObject[] objectsToShow;
    public GameObject[] objectsToHide;
    private bool isToggled = false;

    void Start()
    {
        // Get the Button component and add a listener for the click event
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(Toggle);
        }
        else
        {
            Debug.LogError("ToggleShowHideButton script is not attached to a GameObject with a Button component.");
        }
    }

    private void Toggle()
    {
        isToggled = !isToggled;

        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(isToggled);
            }
        }

        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                obj.SetActive(!isToggled);
            }
        }
    }
}