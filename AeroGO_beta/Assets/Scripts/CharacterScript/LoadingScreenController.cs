using UnityEngine;
using System.Collections;

public class LoadingScreenController : MonoBehaviour
{
    public GameObject loadingScreenPanel; // Assign this in the Inspector

    void Start()
    {
        // Optionally start loading process here if needed
        StartCoroutine(HideLoadingScreenAfterDelay());
    }

    IEnumerator HideLoadingScreenAfterDelay()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Hide the loading screen
        loadingScreenPanel.SetActive(false);

        // Here you can also trigger any actions that should happen after the loading screen is hidden
    }
}
