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
    public TMP_InputField RegisterPasswordConfirmField;
    public TMP_Text RegisterErrorMessage;
    public Button RegisterAccountBtn;
    public Button BackBtn;

    private void Start()
    {
        ShowCorrectPanelOnStart();
    }

    private void ShowCorrectPanelOnStart()
    {
        // Determine whether to show login or registration panel based on previous session or other criteria
        OpenLoginPanel(); // Default action
    }

    #region Authentication Methods

    public void OpenLoginPanel()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
        ClearAuthenticationFields();
    }

    public void OpenRegistrationPanel()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
        ClearAuthenticationFields();
    }

    private void ClearAuthenticationFields()
    {
        // Clears all text fields and error messages
        LoginEmailField.text = "";
        LoginPasswordField.text = "";
        LoginErrorMessage.text = "";
        RegisterEmailField.text = "";
        RegisterDisplayNameField.text = "";
        RegisterPasswordField.text = "";
        RegisterPasswordConfirmField.text = "";
        RegisterErrorMessage.text = "";
    }

    public void TryLogin()
    {
        SetLoginInteractable(false); // Disable login button to prevent multiple clicks
        PlayFabLoginManager.Instance.Login(LoginEmailField.text, LoginPasswordField.text,
            () => SceneManager.LoadScene("CharacterSelection"), // Success
            (error) =>
            {
                LoginErrorMessage.text = "Login failed: " + error;
                SetLoginInteractable(true);
            });
    }

    private void SetLoginInteractable(bool interactable)
    {
        LoginBtn.interactable = interactable;
        RegisterBtn.interactable = interactable;
    }

    public void TryRegister()
    {
        if (RegisterPasswordField.text != RegisterPasswordConfirmField.text)
        {
            RegisterErrorMessage.text = "Passwords do not match.";
            return;
        }

        SetRegistrationInteractable(false); // Disable registration UI elements
        PlayFabLoginManager.Instance.Register(RegisterEmailField.text, RegisterPasswordField.text, RegisterDisplayNameField.text,
            () => OpenLoginPanel(), // Success
            (error) =>
            {
                RegisterErrorMessage.text = "Registration failed: " + error;
                SetRegistrationInteractable(true);
            });
    }

    private void SetRegistrationInteractable(bool interactable)
    {
        BackBtn.interactable = interactable;
        RegisterAccountBtn.interactable = interactable;
    }

    #endregion

    #region Score Management Methods
    // Score submission methods remain unchanged
    #endregion
}
