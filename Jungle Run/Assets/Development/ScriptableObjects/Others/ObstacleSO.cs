using UnityEngine;

[CreateAssetMenu(fileName = "New Obstacle", menuName = "Obstacle/Create New Obstacle")]
public class ObstacleSO : ScriptableObject
{
    public string ObstacleName = "New Obstacle";
    public int ObstacleDamage = 1;
    public Sprite ObstacleSprite;
}
