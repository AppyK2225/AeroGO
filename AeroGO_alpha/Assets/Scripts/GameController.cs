using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
// Make sure you include SceneManager if you're using LoadScene
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    void Start()
    {
        Login();
    }

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

    public void UpdatePlayFabData(Dictionary<string, string> dataToUpdate)
    {
        var request = new UpdateUserDataRequest { Data = dataToUpdate };
        PlayFabClientAPI.UpdateUserData(request, OnDataUpdateSuccess, OnError);
    }

    private void OnDataUpdateSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Successfully updated player data.");
    }

    public void SetActiveModelIndex(string value)
    {
        // Assuming FFCon is a placeholder for actual functionality
        // Update data in PlayFab instead
        Dictionary<string, string> dataToUpdate = new Dictionary<string, string>();
        dataToUpdate.Add("activeModelIndex", value);
        UpdatePlayFabData(dataToUpdate);

        Debug.Log("Active Model Index is now: " + value);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void AddItemToInventory(string item)
    {
        // Add implementation for adding an item to the player's inventory in PlayFab
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public QuizManager quizManager;

    // You might want to remove or modify this method if you're not using FFCon anymore
    public void UpdateData()
    {
        // Update this method to directly update PlayFab data or remove if not needed
    }
}
