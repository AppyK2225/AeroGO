using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GPSSystem : MonoBehaviour
{
    Vector2 RInit;
    Vector2 RCurrentPos;
    Vector2 FInit;
    Vector2 FCurrentPos;

    public float Scale;
    public bool FakingLocation;

    // Start is called before the first frame update
    void Start()
    {
        Input.location.Start(0.5f);
        Input.compass.enabled = true;

        FInit = Vector2.zero;
    }

    IEnumerator UpdatePosition()
    {
        if (FakingLocation == false)
        {
            if (Input.location.isEnabledByUser == false)
            {
                Debug.Log("Failed because user did not enable location");
            }

            int maxwait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxwait > 0)
            {
                yield return new WaitForSeconds(1);
                maxwait--;
            }
            if (maxwait < 1)
            {
                Debug.Log("Initializing failed, try again");
                yield return null;
            }
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Location service status failed");
                yield return null;
            }
            else
            {
                SetLocation(Input.location.lastData.latitude, Input.location.lastData.longitude);
            }
        }
        else
        {
            SetLocation(100 + Time.time, 100 + Time.time);
        }
    }

    void SetLocation(float latitude, float longitude)
    {
        RCurrentPos = new Vector2(latitude, longitude);
        Vector2 delta = new Vector2(RCurrentPos.x = RInit.x, RCurrentPos.y = RInit.y);
        FCurrentPos = delta * Scale;
        this.transform.position = new Vector3(FCurrentPos.x, 0, FCurrentPos.y);
    }

    void Update()
    {
        StartCoroutine(UpdatePosition());
    }
}
