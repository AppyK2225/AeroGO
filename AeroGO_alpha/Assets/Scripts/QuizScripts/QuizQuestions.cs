using UnityEditor;
using UnityEngine;

[CreateAssetMenuAttribute]
public class QuizQuestion : ScriptableObject
{
    [SerializeField]
    private string question;

    [SerializeField]
    private string[] answers;

    [SerializeField]
    private int correctAnswer;

    [SerializeField]
    private int score; // Points for the question

    public string Question { get { return question; } }
    public string[] Answers { get { return answers; } }
    public int CorrectAnswer { get { return correctAnswer; } }
    public int Score => score; // Public getter

    public bool Asked { get; internal set; }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (correctAnswer > answers.Length)
        {
            correctAnswer = 0;
        }

        RenameScriptableObjectToMatchQuestionAndAnswer();
    }

    private void RenameScriptableObjectToMatchQuestionAndAnswer()
    {
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        string desiredName = $"{question.Replace("?", "")} [{answers[correctAnswer]}]";
        string shouldEndWith = "/" + desiredName + ".asset";

        if (!assetPath.EndsWith(shouldEndWith))
        {
            AssetDatabase.RenameAsset(assetPath, desiredName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
#endif
}