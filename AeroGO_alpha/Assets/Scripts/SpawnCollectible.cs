using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Resources.Load(GameObject.Find("Manager").GetComponent<CollectibleManager>().Collectible), this.transform.position, this.transform.rotation);
    }
}
