using UnityEngine;
using TMPro; // Use TMPro if you are using Text Mesh Pro for your text

public class LeaderboardEntry : MonoBehaviour
{
    public TextMeshProUGUI RankText;
    public TextMeshProUGUI UserText;
    public TextMeshProUGUI ScoreText;

    public void SetDetails(int rank, string name, int score)
    {
        Debug.Log($"Updating Leaderboard Entry - Rank: {rank}, User: {name}, Score: {score}");
        RankText.text = rank.ToString();
        UserText.text = name;
        ScoreText.text = score.ToString();
    }

}
