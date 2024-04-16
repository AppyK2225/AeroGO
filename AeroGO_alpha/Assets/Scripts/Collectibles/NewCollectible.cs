using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCollectible : MonoBehaviour
{
    public string type;
    public int SP;
    // Start is called before the first frame update
    public NewCollectible(int newSP, string Type)
    {
        SP = newSP;
    }
}
