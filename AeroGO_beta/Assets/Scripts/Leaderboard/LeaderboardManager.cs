using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderboardEntryPrefab; // Set this in the Inspector
    public Transform leaderboardContent; // Assign the Content panel from the Inspector

    void OnEnable()
    {
        RequestLeaderboard();
    }

    public void RequestLeaderboard()
    {
        Debug.Log("Requesting Leaderboard...");
        var request = new GetLeaderboardRequest
        {
            StartPosition = 0,
            StatisticName = "GlobalHighScore",
            MaxResultsCount = 100 // Adjust the number of results as necessary
        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        // Clear existing leaderboard entries to prevent duplication
        foreach (Transform child in leaderboardContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newEntry = Instantiate(leaderboardEntryPrefab, leaderboardContent);
            LeaderboardEntry entry = newEntry.GetComponent<LeaderboardEntry>();
            if (entry != null)
            {
                entry.SetDetails(item.Position + 1, item.DisplayName, item.StatValue);
            }
            else
            {
                Debug.LogError("Failed to get LeaderboardEntry component on the instantiated prefab.");
            }
        }
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError("Error retrieving leaderboard: " + error.GenerateErrorReport());
    }

    public void RefreshLeaderboard()
    {
        RequestLeaderboard();
    }

}
