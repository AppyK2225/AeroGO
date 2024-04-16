using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "Quiz/Question", order = 1)]
public class Question : ScriptableObject
{
    public string questionText;
    public string[] answerChoices;
    public int correctAnswerIndex;
    public int scoreValue;
}