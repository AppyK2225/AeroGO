using System.Linq;
using UnityEngine;

public class QuestionCollection : MonoBehaviour
{
    private QuizQuestion[] allQuestions;

    private void Awake()
    {
        LoadAllQuestions();
    }

    private void LoadAllQuestions()
    {
        allQuestions = Resources.LoadAll<QuizQuestion>("Questions");
    }

    public QuizQuestion GetUnaskedQuestion()
    {
        ResetQuestionsIfAllHaveBeenAsked();

        var question = allQuestions
            .Where(t => t.Asked == false)
            .OrderBy(t => UnityEngine.Random.Range(0, int.MaxValue))
            .FirstOrDefault();

        question.Asked = true;
        return question;
    }

    private void ResetQuestionsIfAllHaveBeenAsked()
    {
        if (allQuestions.Any(t => t.Asked == false) == false)
        {
            ResetQuestions();
        }
    }

    private void ResetQuestions()
    {
        foreach (var question in allQuestions)
            question.Asked = false;
    }
}