using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCompass : MonoBehaviour
{
    public Transform Player;
    Vector3 vector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vector.z = Player.eulerAngles.y;
        transform.localEulerAngles = vector;
    }
}
