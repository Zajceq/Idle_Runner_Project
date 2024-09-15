using UnityEngine;

public class ActivateChildren : MonoBehaviour
{
    public void ActivateAllChildren()
    {
        // Loop through each child of the current object
        foreach (Transform child in transform)
        {
            // Activate each child GameObject
            child.gameObject.SetActive(true);
        }
    }
}
