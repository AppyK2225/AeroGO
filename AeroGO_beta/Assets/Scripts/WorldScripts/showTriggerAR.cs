using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showTriggerAR : MonoBehaviour
{
    //Vector3 distance;
    public GameObject myPlayer;
    public GameObject AR_Child;

    void Start()
    {
        if (myPlayer == null)
            myPlayer = GameObject.FindWithTag("Player");
        Debug.Log("myPlayer Assigned");

        AR_Child = this.gameObject.transform.GetChild(0).gameObject;
        Debug.Log("Child Got");

    }
    

    void Update()
    {
        float distance = Vector3.Distance(myPlayer.transform.position, transform.position);
        //Debug.Log("Distance =" + distance);

        if (distance <= 10)
            AR_Child.SetActive(true);
        else
            AR_Child.SetActive(false);
        
    }

}

