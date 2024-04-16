using UnityEngine;

[CreateAssetMenu(fileName = "CharacterPrefabLibrary", menuName = "Character/CharacterPrefabLibrary", order = 1)]
public class CharacterPrefabLibrary : ScriptableObject
{
    public GameObject[] characterPrefabs; // Assign your character prefabs in the inspector
}
