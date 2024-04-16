using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }    // You can add more scene management functions here as needed
}
