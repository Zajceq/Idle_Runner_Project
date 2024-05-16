using UnityEngine;

public class ActivateChildren : MonoBehaviour
{
    public void ActivateAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
