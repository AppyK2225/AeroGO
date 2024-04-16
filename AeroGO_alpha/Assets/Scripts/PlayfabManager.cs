using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    // Update is called once per frame
    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }
    public void SubmitScore(int playerScore)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore",
                    Value = playerScore
                }
            }
        }, OnScoreSubmitSuccess, OnScoreSubmitError);
    }
    public void GetScore()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            result => OnGetScoreSuccess(result),
            error => OnError(error)
        );
    }

    private void OnGetScoreSuccess(GetPlayerStatisticsResult result)
    {
        foreach (var eachStat in result.Statistics)
        {
            if (eachStat.StatisticName == "HighScore")
            {
                Debug.Log("Score: " + eachStat.Value);
                // Update your game UI with the fetched score
            }
        }
    }

    void OnScoreSubmitSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfully submitted high score.");
    }

    void OnScoreSubmitError(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with submitting the score. Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

}