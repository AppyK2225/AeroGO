using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayFabController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject LoginPanel;
    public GameObject RegisterPanel;

    [Header("Login Fields")]
    public TMP_InputField LoginEmailField;
    public TMP_InputField LoginPasswordField;
    public TMP_Text LoginErrorMessage;
    public Button LoginBtn;
    public Button RegisterBtn;

    [Header("Registration Fields")]
    public TMP_InputField RegisterEmailField;
    public TMP_InputField RegisterDisplayNameField;
    public TMP_InputField RegisterPasswordField;
    public TMP_InputField RegisterPasswordConfirmField; // Add this line
    public TMP_Text RegisterErrorMessage;
    public Button RegisterAccountBtn;
    public Button BackBtn;

    private void Start()
    {
        // Optionally, automatically attempt to login with a stored session or display login UI
    }

    #region Authentication Methods

    public void OpenLoginPanel()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
    }

    public void OpenRegistrationPanel()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }

    public void TryLogin()
    {
        string email = LoginEmailField.text;
        string password = LoginPasswordField.text;

        LoginBtn.interactable = false;

        LoginWithEmailAddressRequest req = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(req,
        res =>
        {
            Debug.Log("Login Success");
            SceneManager.LoadScene(1);
        },
        err =>
        {
            Debug.Log("Error: " + err.ErrorMessage);
            LoginBtn.interactable = true;
        });
    }

    public void TryRegister()
    {
        // Check if passwords match
        if (RegisterPasswordField.text != RegisterPasswordConfirmField.text)
        {
            RegisterErrorMessage.text = "Passwords do not match.";
            return;
        }

        BackBtn.interactable = false;
        RegisterAccountBtn.interactable = false;

        string email = RegisterEmailField.text;
        string displayName = RegisterDisplayNameField.text;
        string password = RegisterPasswordField.text;

        RegisterPlayFabUserRequest req = new RegisterPlayFabUserRequest
        {
            Email = email,
            DisplayName = displayName,
            Password = password,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(req,
        res =>
        {
            BackBtn.interactable = true;
            RegisterAccountBtn.interactable = true;
            OpenLoginPanel();
            Debug.Log(res.PlayFabId);
        },
        err =>
        {
            BackBtn.interactable = true;
            RegisterAccountBtn.interactable = true;
            Debug.Log("Error: " + err.ErrorMessage);
        });
    }

    #endregion

    #region Score Management Methods
    public void SubmitScore(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = "HighScore", Value = score } }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnScoreSubmitSuccess, OnScoreSubmitFailure);
    }

    private void OnScoreSubmitSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Score submitted successfully.");
        // Additional logic to handle successful score submission
    }

    private void OnScoreSubmitFailure(PlayFabError error)
    {
        Debug.LogError("Failed to submit score: " + error.GenerateErrorReport());
        // Handle score submission failure (e.g., display an error message)
    }
    #endregion
}
