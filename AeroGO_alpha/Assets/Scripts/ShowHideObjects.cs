using UnityEngine;
using UnityEngine.UI; // Required for UI components
using UnityEngine.EventSystems; // Required for event handling

public class ShowHideObjects : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;

    void Start()
    {
        // Check if the GameObject has a Button component
        Button button = GetComponent<Button>();
        if (button != null)
        {
            // If it's a Button, use the onClick event
            button.onClick.AddListener(OnPanelClicked);
        }
        // If no Button component is found, the IPointerClickHandler interface will handle clicks
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // This function will be called when the GameObject is clicked, if it's not a Button
        OnPanelClicked();
    }

    private void OnPanelClicked()
    {
        // Enable the specified GameObjects
        foreach (var obj in objectsToEnable)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        // Disable the specified GameObjects
        foreach (var obj in objectsToDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }
}
