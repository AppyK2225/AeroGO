using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    List<NewCollectible> collectible = new List<NewCollectible>();
    public string Collectible;

    void Start()
    {
        Object.DontDestroyOnLoad(this.gameObject);
    }

    public void GrabCollectible(int SP, string Type)
    {
        collectible.Add(new NewCollectible(SP, Type));
    }
}
