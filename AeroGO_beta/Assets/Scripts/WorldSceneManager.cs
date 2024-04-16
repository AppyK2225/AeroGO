using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class WorldCharacterManager : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Assign the same character prefabs in the inspector as in the Character Selection Scene
    public Transform spawnPoint; // Define a spawn point for the character in the scene

    void Start()
    {
        LoadCharacterSelection();
    }

    void LoadCharacterSelection()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data != null && result.Data.ContainsKey("CharacterSelected"))
            {
                int selectedCharacterIndex = int.Parse(result.Data["CharacterSelected"].Value);
                InstantiateSelectedCharacter(selectedCharacterIndex);
            }
            else
            {
                Debug.LogError("No character selection found.");
                // Handle case where no character is selected, perhaps by loading a default character
            }
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    void InstantiateSelectedCharacter(int index)
    {
        if (index < 0 || index >= characterPrefabs.Length)
        {
            Debug.LogError("Character index out of bounds.");
            return;
        }

        // Instantiate the selected character at the spawn point
        GameObject characterInstance = Instantiate(characterPrefabs[index], spawnPoint.position, Quaternion.identity);
        // Activate the character if it's not active by default, and set up any initial configuration here
        characterInstance.SetActive(true);
    }
}
