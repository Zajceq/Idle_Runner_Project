using UnityEngine;

[CreateAssetMenu(fileName = "New Collectable", menuName = "Collectable/Create New Collectable")]
public class CollectableSO : ScriptableObject
{
    public string CollectableName = "New Collectable";
    public int CollectablePoints = 10;
    public Sprite CollectableSprite;
}