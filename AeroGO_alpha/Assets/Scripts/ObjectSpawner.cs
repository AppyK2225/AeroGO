using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    public AbstractMap map; // Reference to your Mapbox map
    public GameObject objectToSpawn; // The object you want to spawn

    public double latitude;
    public double longitude;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        
        SpawnObjectAtLocation(latitude, longitude);
    }

    public void SpawnObjectAtLocation(double latitude, double longitude)
    {
        Debug.Log("Latitude: " + latitude + ", Longitude: " + longitude);

        // Create a Vector2d from the separate latitude and longitude values
        Vector2d latLong = new Vector2d(latitude, longitude);

        // Convert geographic coordinates to Unity world position
        Vector3 spawnPosition = map.GeoToWorldPosition(latLong, true);
        Debug.Log("Spawn Position in Unity: " + spawnPosition);

        // Instantiate the object at the calculated position
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
