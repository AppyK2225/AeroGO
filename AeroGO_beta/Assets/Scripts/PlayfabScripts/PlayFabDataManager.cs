using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabDataManager : MonoBehaviour
{
    public static PlayFabDataManager Instance;
    private int currentScore = 0;
    private string currentCharacterSelected = "DefaultCharacter";

    public int CurrentScore => currentScore;
    public string CurrentCharacterSelected => currentCharacterSelected;
    public bool IsDataLoaded { get; private set; } = false;

    public event System.Action OnDataLoaded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadPlayerData();
    }

    public void LoadPlayerData()
    {
        Debug.Log("Loading player data from PlayFab.");
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data != null && result.Data.Count > 0)
            {
                ProcessData(result.Data);
                IsDataLoaded = true;
                OnDataLoaded?.Invoke();
                Debug.Log("All player data loaded successfully from PlayFab.");
            }
            else
            {
                Debug.Log("No player data found, using default values.");
                SetDefaults();
            }
        }, error =>
        {
            Debug.LogError($"Error loading player data: {error.GenerateErrorReport()}");
            SetDefaults();
        });
    }

    private void ProcessData(Dictionary<string, UserDataRecord> data)
    {
        if (data.TryGetValue("PlayerScore", out var scoreValue) && int.TryParse(scoreValue.Value, out var score))
        {
            currentScore = score;
            Debug.Log($"Player Score loaded successfully: {currentScore}");
            UpdateLeaderboard(currentScore); // Ensure leaderboard is updated with latest score
        }
        else
        {
            Debug.LogWarning("Failed to parse PlayerScore, using default value.");
            currentScore = 0;
        }

        if (data.TryGetValue("CharacterSelected", out var characterValue))
        {
            currentCharacterSelected = characterValue.Value;
            Debug.Log($"Character Selected loaded: {currentCharacterSelected}");
        }
        else
        {
            Debug.LogWarning("No CharacterSelected found, using default.");
            currentCharacterSelected = "DefaultCharacter";
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        Debug.Log($"Updated score to {currentScore}, saving...");
        SavePlayerData();  // Also update leaderboard in SavePlayerData
    }

    public void SavePlayerData()
    {
        var data = new Dictionary<string, string>
        {
            {"PlayerScore", currentScore.ToString()},
            {"CharacterSelected", currentCharacterSelected}
        };
        UpdateUserData(new UpdateUserDataRequest { Data = data });
    }

    private void UpdateUserData(UpdateUserDataRequest request)
    {
        PlayFabClientAPI.UpdateUserData(request, result =>
        {
            Debug.Log("Player data saved successfully to PlayFab.");
            UpdateLeaderboard(currentScore);  // Update leaderboard after saving data
        }, error =>
        {
            Debug.LogError($"Failed to save player data: {error.GenerateErrorReport()}");
        });
    }

    private void UpdateLeaderboard(int score)
    {
        var updateRequest = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = "GlobalHighScore", Value = score } }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(updateRequest, result =>
        {
            Debug.Log("Leaderboard score updated successfully.");
        }, error =>
        {
            Debug.LogError($"Failed to update leaderboard: {error.GenerateErrorReport()}");
        });
    }

    private void SetDefaults()
    {
        currentScore = 0;
        currentCharacterSelected = "DefaultCharacter";
        IsDataLoaded = false;
    }
}
