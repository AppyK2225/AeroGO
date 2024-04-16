using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showTriggerXP : MonoBehaviour
{
    private GameObject myPlayer;
    public GameObject XP_Child;
    public float activationDistance = 12.0f; // Adjustable distance threshold for activation in Inspector

    void Start()
    {
        // Automatically find the GameObject with the tag "Player"
        myPlayer = GameObject.FindWithTag("Player");
        if (myPlayer == null)
        {
            Debug.LogError("Player object not found!");
        }
        else
        {
            Debug.Log("myPlayer Assigned");
        }

        // Get the first child of this GameObject as the XP_Child
        XP_Child = this.gameObject.transform.GetChild(0).gameObject;
        Debug.Log("Child Got");
    }
    
    void Update()
    {
        if (myPlayer != null)
        {
            // Calculate the distance between this object and the player
            float distance = Vector3.Distance(myPlayer.transform.position, transform.position);
            //Debug.Log("Distance = " + distance);

            // Toggle the XP_Child's active state based on the distance
            if (distance <= activationDistance)
                XP_Child.SetActive(true);
            else
                XP_Child.SetActive(false);
        }
    }
}
