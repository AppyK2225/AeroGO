using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    public List<GameObject> characterPrefabs;
    public GameObject characterDisplayLocation;
    private List<GameObject> instantiatedCharacterModels = new List<GameObject>();
    private int currentIndex = 0;

    private void Start()
    {
        InitializeCharacterModels();
        LoadCharacterSelection();
    }

    private void InitializeCharacterModels()
    {
        foreach (var prefab in characterPrefabs)
        {
            GameObject instance = Instantiate(prefab, characterDisplayLocation.transform);
            instance.SetActive(false);
            instantiatedCharacterModels.Add(instance);
        }
    }

    public void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % characterPrefabs.Count;
        UpdateCharacterDisplay();
    }

    public void PreviousCharacter()
    {
        if (currentIndex == 0) currentIndex = characterPrefabs.Count;
        currentIndex--;
        UpdateCharacterDisplay();
    }

    private void UpdateCharacterDisplay()
    {
        if (currentIndex >= 0 && currentIndex < instantiatedCharacterModels.Count)
        {
            foreach (var model in instantiatedCharacterModels)
            {
                model.SetActive(false);
            }
            instantiatedCharacterModels[currentIndex].SetActive(true);
        }
        else
        {
            Debug.LogError("Current index is out of valid range. Resetting index.");
            currentIndex = 0; // Reset to safe default
            UpdateCharacterDisplay(); // Recursive call with corrected index
        }
    }

    public void SelectCharacter()
    {
        SaveCharacterSelection();
        SceneManager.LoadScene("WorldScene");
    }

    private void LoadCharacterSelection()
    {
        Debug.Log("Loading character selection from PlayFab.");
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data != null && result.Data.ContainsKey("CharacterSelected"))
            {
                if (int.TryParse(result.Data["CharacterSelected"].Value, out int savedIndex) && savedIndex >= 0 && savedIndex < instantiatedCharacterModels.Count)
                {
                    currentIndex = savedIndex;
                    Debug.Log($"Character previously selected: {currentIndex}. Loading character.");
                    UpdateCharacterDisplay();
                }
                else
                {
                    Debug.LogError("Stored character index is out of range or invalid. Falling back to default.");
                    currentIndex = 0; // Reset to default if out-of-range or parse fail
                    UpdateCharacterDisplay();
                }
            }
            else
            {
                Debug.Log("No character previously selected. Displaying character selection.");
                UpdateCharacterDisplay();
            }
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }


    private void LoadWorldScene()
    {
        SceneManager.LoadScene("WorldScene");
    }

    private void SaveCharacterSelection()
    {
        Debug.Log("Saving character selection to PlayFab.");
        var data = new Dictionary<string, string> { { "CharacterSelected", currentIndex.ToString() } };
        var request = new UpdateUserDataRequest { Data = data };

        PlayFabClientAPI.UpdateUserData(request, result =>
        {
            Debug.Log("Character selection saved successfully.");
            SceneManager.LoadScene("WorldScene"); // Consider moving scene transition here to ensure it happens after save confirmation.
        }, error =>
        {
            Debug.LogError($"Failed to save character selection: {error.GenerateErrorReport()}");
        });
    }

}
