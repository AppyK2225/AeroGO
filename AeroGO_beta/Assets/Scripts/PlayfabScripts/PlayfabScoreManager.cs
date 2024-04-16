using TMPro;
using UnityEngine;

public class PlayFabScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private PlayFabDataManager dataManager;

    void Start()
    {
        // Ensure the data manager is available
        dataManager = PlayFabDataManager.Instance;
        if (dataManager == null)
        {
            Debug.LogError("PlayFabDataManager instance not found. Make sure it is loaded in the scene.");
            return;
        }

        // Subscribe to data loaded event
        dataManager.OnDataLoaded += HandleDataLoaded;

        // Update UI if data is already loaded
        if (dataManager.IsDataLoaded)
        {
            UpdateScoreUI(dataManager.CurrentScore);
        }
    }

    private void HandleDataLoaded()
    {
        // Update UI when data is loaded
        UpdateScoreUI(dataManager.CurrentScore);
    }

    public void AddScore(int scoreToAdd)
    {
        if (dataManager != null)
        {
            dataManager.UpdateScore(scoreToAdd); // Update the score in DataManager
            UpdateScoreUI(dataManager.CurrentScore); // Reflect the new score in the UI
        }
        else
        {
            Debug.LogError("DataManager is not assigned in PlayFabScoreManager when trying to add score.");
        }
    }

    private void UpdateScoreUI(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
        else
        {
            Debug.LogError("ScoreText UI component is not assigned.");
        }
    }

    void OnDestroy()
    {
        // Clean up event subscription
        if (dataManager != null)
        {
            dataManager.OnDataLoaded -= HandleDataLoaded;
        }
    }
}
