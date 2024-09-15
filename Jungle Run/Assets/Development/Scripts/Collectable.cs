using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableSO collectableData; // Reference to the collectable's data from the ScriptableObject
    [SerializeField] private IntVariable gameScore; // Reference to the game's score variable

    [SerializeField] private UnityEvent onTriggerEnter; // Event triggered when the collectable is picked up

    private SpriteRenderer _spriteRenderer; // Reference to the SpriteRenderer component

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    private void Start()
    {
        // Set the sprite of the collectable based on its data
        _spriteRenderer.sprite = collectableData.CollectableSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player has collided with the collectable
        if (collision.tag == "Player")
        {
            // Add points to the game score
            gameScore.Value += collectableData.CollectablePoints;

            // Invoke onTriggerEnter event
            onTriggerEnter?.Invoke();
        }
    }
}