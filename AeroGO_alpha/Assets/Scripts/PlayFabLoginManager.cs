using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayFabLoginManager : MonoBehaviour
{
    [Header("Screens")]
    public GameObject LoginPanel;
    public GameObject RegisterPanel;

    [Header("Login Screen")]
    public TMP_InputField LoginEmailField;
    public TMP_InputField LoginPasswordField;
    public Button LoginBtn;
    public Button RegisterBtn;

    [Header("Register Screen")]
    public TMP_InputField RegisterEmailField;
    public TMP_InputField RegisterDisplayNameField;
    public TMP_InputField RegisterPasswordwordField;
    public Button RegisterAccountBtn;
    public Button BackBtn;

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

    public void OnTryLogin()
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
            UserStatus.Instance.LoadUserData();
            SceneManager.LoadScene(1);
        },
        err =>
        {
            Debug.Log("Error: " + err.ErrorMessage);
            LoginBtn.interactable = true;
        });
    }

    public void OnTryRegisterNewAccount()
    {
        BackBtn.interactable = false;
        RegisterAccountBtn.interactable = false;

        string email = RegisterEmailField.text;
        string displayName = RegisterDisplayNameField.text;
        string password = RegisterPasswordwordField.text;

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
}