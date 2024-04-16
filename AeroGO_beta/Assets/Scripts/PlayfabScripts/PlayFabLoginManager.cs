using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLoginManager : MonoBehaviour
{
    public static PlayFabLoginManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    public void Login(string email, string password, System.Action onSuccess, System.Action<string> onFailure)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, result =>
        {
            onSuccess?.Invoke();
        },
        error =>
        {
            onFailure?.Invoke(error.ErrorMessage);
        });
    }

    public void Register(string email, string password, string displayName, System.Action onSuccess, System.Action<string> onFailure)
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest
        {
            Email = email,
            DisplayName = displayName,
            Password = password,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, result =>
        {
            onSuccess?.Invoke();
        },
        error =>
        {
            onFailure?.Invoke(error.ErrorMessage);
        });
    }
}
