using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management
using PlayFab;
using PlayFab.ClientModels;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string LastClickedObjectName;
    public Dictionary<string, int> ObjectQuestionIndex = new Dictionary<string, int>();

    // Event to notify other parts of the game when a question index is updated
    public event Action<string, int> OnQuestionIndexChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadProgress();  // Load saved progress when the game manager starts
    }

    public void UpdateQuestionIndex(string objectName, int currentIndex)
    {
        Debug.Log($"Updating question index for {objectName} to {currentIndex}.");

        // Update the index or add a new entry if it doesn't exist
        ObjectQuestionIndex[objectName] = currentIndex;

        // Invoke the event to notify subscribers
        OnQuestionIndexChanged?.Invoke(objectName, currentIndex);
        Debug.Log($"Question index for {objectName} updated to {currentIndex}.");

        // Save progress after updating the question index
        SaveProgress();
    }

    public int GetCurrentQuestionIndex(string objectName)
    {
        // Retrieve the current question index, defaulting to 0 if not found
        ObjectQuestionIndex.TryGetValue(objectName, out int currentIndex);
        return currentIndex;
    }

    // Save the current state of ObjectQuestionIndex to PlayFab
    private void SaveProgress()
    {
        var data = new Dictionary<string, string>();
        foreach (var pair in ObjectQuestionIndex)
        {
            data.Add("QuestionIndex_" + pair.Key, pair.Value.ToString());
            Debug.Log($"Saving {pair.Key}: {pair.Value}");
        }

        var request = new UpdateUserDataRequest { Data = data };
        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("Progress saved to PlayFab successfully."),
            error => Debug.LogError("Failed to save progress: " + error.GenerateErrorReport()));
    }

    // Load the state from PlayFab
    private void LoadProgress()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data != null)
            {
                foreach (var item in result.Data)
                {
                    string key = item.Key;
                    if (key.StartsWith("QuestionIndex_"))
                    {
                        string objectName = key.Substring("QuestionIndex_".Length);
                        if (int.TryParse(item.Value.Value, out int index))
                        {
                            ObjectQuestionIndex[objectName] = index;
                            Debug.Log($"Loaded {objectName}: {index}");
                        }
                    }
                }
                Debug.Log("Progress loaded from PlayFab.");
            }
        }, error => Debug.LogError("Failed to load progress: " + error.GenerateErrorReport()));
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
