using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPrefabDatabase", menuName = "Quiz/Object Prefab Database", order = 1)]
public class ObjectPrefabDatabase : ScriptableObject
{
    public ObjectPrefabData[] objects;

    public GameObject GetPrefabByName(string name)
    {
        foreach (var objectData in objects)
        {
            if (objectData.objectName.Equals(name, StringComparison.OrdinalIgnoreCase))
                return objectData.prefab;
        }
        return null; // Return null if no matching object is found
    }
}
