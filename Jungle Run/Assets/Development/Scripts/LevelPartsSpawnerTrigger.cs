using UnityEngine;
using UnityEngine.Pool;

public class LevelPartsSpawnerTrigger : MonoBehaviour
{
    [SerializeField] private Transform spawnTransform;

    [HideInInspector] public IObjectPool<GameObject> Pool;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LevelPartsSpawner.Instance.SpawnRandomLevelPart(spawnTransform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        if (Pool == null)
        {
            Destroy(transform.parent.gameObject);
            return;
        }

        Pool.Release(transform.parent.gameObject);
    }
}
