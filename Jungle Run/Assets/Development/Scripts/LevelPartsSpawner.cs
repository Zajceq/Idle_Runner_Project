using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelPartsSpawner : Singleton<LevelPartsSpawner>
{
    [SerializeField] List<GameObject> levelParts;
    [SerializeField] int defaultPoolCapacity = 3;
    [SerializeField] int maxPoolSize = 5;

    private List<IObjectPool<GameObject>> objectPools = new List<IObjectPool<GameObject>>();

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetPools();
    }

    private void Start()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (GameObject levelPart in levelParts)
        {
            if (levelPart != null)
            {
                objectPools.Add(new ObjectPool<GameObject>(() => Instantiate(levelPart), levelPart => levelPart.SetActive(true),
                    levelPart => levelPart.SetActive(false), levelPart => Destroy(levelPart), true, defaultPoolCapacity, maxPoolSize));
            }
        }
    }

    private void ResetPools()
    {
        foreach (var pool in objectPools)
        {
            pool.Clear();
        }
        objectPools.Clear();
        InitializePools();
    }

    public void SpawnRandomLevelPart(Transform spawnTransform)
    {
        int randomPartNumber = Random.Range(0, objectPools.Count);
        var poolForSpawning = objectPools[randomPartNumber];

        var partToSpawn = poolForSpawning.Get();
        partToSpawn.transform.position = spawnTransform.position;

        var trigger = partToSpawn.GetComponentInChildren<LevelPartsSpawnerTrigger>();
        trigger.Pool = poolForSpawning;
    }
}
