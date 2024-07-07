using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableSO collectableData;
    [SerializeField] private IntVariable gameScore;

    [SerializeField] private UnityEvent onTriggerEnter;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = collectableData.CollectableSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameScore.Value += collectableData.CollectablePoints;
            onTriggerEnter?.Invoke();
        }
    }
}