using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    public CollectibleManager manager;
    public string ID;
    public void OnMouseDown()
    {
        manager.Collectible = ID;
        SceneManager.LoadScene(2);
    }
}
