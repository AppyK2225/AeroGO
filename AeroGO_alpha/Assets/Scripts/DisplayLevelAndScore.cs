using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;


public class DisplayPlayerLevelAndScore : MonoBehaviour
{
    public TextMeshProUGUI levelText; // For TextMeshPro
    public TextMeshProUGUI scoreRemainderText; // For TextMeshPro
    public PlayFabController playFabController;


    private void Start()
    {
        UpdateScoreDisplay();
    }

    public void UpdateScoreDisplay()
    {
        int playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        int playerLevel = playerScore / 100;
        int scoreRemainder = playerScore % 100;

        levelText.text = "Level: " + playerLevel.ToString();
        scoreRemainderText.text = "Score: " + scoreRemainder.ToString();

        // Ensure playFabController is not null
        if (playFabController != null)
        {
            playFabController.SubmitScore(playerScore);
        }
        else
        {
            Debug.LogError("PlayFabController reference not set in DisplayPlayerLevelAndScore.");
        }
    }


}
