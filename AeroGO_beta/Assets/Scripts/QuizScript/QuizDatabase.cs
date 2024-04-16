using UnityEngine;

[CreateAssetMenu(fileName = "QuizDatabase", menuName = "Quiz/New Quiz Database", order = 0)]
public class QuizDatabase : ScriptableObject
{
    public Quiz[] quizzes;

    // Returns all questions for a given object name
    public Question[] GetQuestionsForObjectName(string objectName)
    {
        foreach (var quiz in quizzes)
        {
            if (quiz.objectName.Equals(objectName, System.StringComparison.OrdinalIgnoreCase))
                return quiz.questions;
        }
        return null; // If no quiz found for the object
    }
}
