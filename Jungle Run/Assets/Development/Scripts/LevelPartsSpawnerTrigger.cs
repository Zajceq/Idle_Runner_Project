using UnityEngine;
using UnityEngine.Pool;

public class LevelPartsSpawnerTrigger : MonoBehaviour
{
    [SerializeField] private Transform spawnTransform; // Position where the new level part will spawn

    [HideInInspector] public IObjectPool<GameObject> Pool; // Reference to the object pool this part belongs to

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player enters the trigger zone, spawn a new level part
        if (collision.tag == "Player")
        {
            LevelPartsSpawner.Instance.SpawnRandomLevelPart(spawnTransform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Only proceed if the player exits the trigger zone
        if (collision.tag != "Player") return;

        // If the pool is not set, deactivate the parent object
        if (Pool == null)
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }

        // Return the level part to the pool for reuse
        Pool.Release(transform.parent.gameObject);
    }
}