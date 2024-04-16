using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CollectionItem
{
    public string objectName;
    public Image objectImage;
    public Image[] progressIcons; // Assume two icons for simplicity
}

public class CollectionManager : MonoBehaviour
{
    public CollectionItem[] collectionItems; // Assign this in the inspector

    void Start()
    {
        UpdateCollectionDisplay();
    }

    void UpdateCollectionDisplay()
    {
        foreach (var item in collectionItems)
        {
            int progress = GameManager.Instance.GetCurrentQuestionIndex(item.objectName);

            // Activate icons based on progress
            for (int i = 0; i < item.progressIcons.Length; i++)
            {
                item.progressIcons[i].gameObject.SetActive(i < progress);
            }
        }
    }
}
