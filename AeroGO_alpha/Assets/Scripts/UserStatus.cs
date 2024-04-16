using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

[System.Serializable]
public class CharacterModel
{
    public string modelID;
    public string modelName;
}

public class UserStatus : MonoBehaviour
{
    public static UserStatus Instance { get; private set; }

    public string Username { get; private set; }
    public int CurrentLevel { get; private set; }
    public int CurrentScore { get; private set; }
    public CharacterModel SelectedCharacterModel { get; private set; }
    public int NumberOfItemsInInventory { get; private set; }

    private void Awake()
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

    public void SetUsername(string username)
    {
        Username = username;
        UpdateDisplayNameOnPlayFab(username);
    }

    public void SetLevel(int level)
    {
        CurrentLevel = level;
        SaveUserData();
    }

    public void SetScore(int score)
    {
        CurrentScore = score;
        SaveUserData();
    }

    public void SetCharacterModel(CharacterModel model)
    {
        SelectedCharacterModel = model;
        SaveUserData();
    }

    public void UpdateInventoryCount(int itemCount)
    {
        NumberOfItemsInInventory = itemCount;
        SaveUserData();
    }

    private void SaveUserData()
    {
        var data = new Dictionary<string, string>
        {
            {"CurrentLevel", CurrentLevel.ToString()},
            {"CurrentScore", CurrentScore.ToString()},
            {"SelectedCharacterModelID", SelectedCharacterModel.modelID},
            {"NumberOfItemsInInventory", NumberOfItemsInInventory.ToString()}
        };

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest { Data = data },
            result => Debug.Log("User data saved successfully."),
            error => Debug.LogError("An error occurred while saving user data: " + error.ErrorMessage));
    }

    private void UpdateDisplayNameOnPlayFab(string displayName)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = displayName },
            result => Debug.Log("Display name updated successfully."),
            error => Debug.LogError("An error occurred while updating display name: " + error.ErrorMessage));
    }

    // Call this method after successful login
    public void LoadUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
            result =>
            {
                if (result.Data != null)
                {
                    if (result.Data.ContainsKey("CurrentLevel"))
                        SetLevel(int.Parse(result.Data["CurrentLevel"].Value));

                    if (result.Data.ContainsKey("CurrentScore"))
                        SetScore(int.Parse(result.Data["CurrentScore"].Value));

                    if (result.Data.ContainsKey("SelectedCharacterModelID"))
                    {
                        // Assuming you have a way to get the CharacterModel from the modelID
                        CharacterModel characterModel = GetCharacterModelFromID(result.Data["SelectedCharacterModelID"].Value);
                        SetCharacterModel(characterModel);
                    }

                    if (result.Data.ContainsKey("NumberOfItemsInInventory"))
                        UpdateInventoryCount(int.Parse(result.Data["NumberOfItemsInInventory"].Value));
                }
            },
            error => Debug.LogError("An error occurred while loading user data: " + error.ErrorMessage));
    }

    // Implement this according to how you manage character models
    private CharacterModel GetCharacterModelFromID(string modelID)
    {
        // Placeholder: Return a CharacterModel based on the modelID
        return new CharacterModel { modelID = modelID };
    }
}
