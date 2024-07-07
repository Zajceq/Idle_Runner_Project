using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleSO obstacleData;
    [SerializeField] private UnityEvent onTriggerEnter;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = obstacleData.ObstacleSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Damageable>().TakeDamage(obstacleData.ObstacleDamage);
            onTriggerEnter?.Invoke();
        }
    }
}