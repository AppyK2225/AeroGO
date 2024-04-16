using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class SelectCharacter : MonoBehaviour
{
    public GameObject[] models;

    void Awake()
    {
        FetchActiveModelIndexFromPlayFab();
    }

    private void FetchActiveModelIndexFromPlayFab()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    private void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("activeModelIndex"))
        {
            UpdateModelVisibility(result.Data["activeModelIndex"].Value);
        }
        else
        {
            // Handle case where no model index is set
            Debug.Log("No active model index set. Please select a character.");
        }
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError("Error fetching player data: " + error.GenerateErrorReport());
    }

    public void UpdateModelVisibility(string activeModelIndex)
    {
        for (int i = 0; i < models.Length; i++)
        {
            if (models[i] != null)
            {
                models[i].SetActive(models[i].name == activeModelIndex);

                Debug.Log(i + "th turn");
                Debug.Log("model " + i + ": " + models[i].name);
                Debug.Log("activeModelIndex: " + activeModelIndex);
            }
        }
    }
}
