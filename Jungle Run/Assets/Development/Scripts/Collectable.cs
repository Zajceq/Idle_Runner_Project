using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableSO collectableData;

    void Start()
    {
        DebugCollectablePoints();
    }

    void DebugCollectablePoints()
    {
        Debug.Log(collectableData.CollectableName + " has " + collectableData.CollectablePoints + " points");
    }
}