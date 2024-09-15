using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleSO obstacleData; // Data about the obstacle from Scriptable Object
    [SerializeField] private UnityEvent onTriggerEnter; // Event triggered when the player hits the obstacle

    private SpriteRenderer _spriteRenderer; // Reference to the SpriteRenderer component

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    private void Start()
    {
        // Set the sprite of the obstacle based on its data
        _spriteRenderer.sprite = obstacleData.ObstacleSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collides with the obstacle
        if (collision.tag == "Player")
        {
            // Deal damage to the player based on the obstacle's damage value
            collision.gameObject.GetComponent<Damageable>().TakeDamage(obstacleData.ObstacleDamage);

            // Invoke onTriggerEnter event
            onTriggerEnter?.Invoke();
        }
    }
}