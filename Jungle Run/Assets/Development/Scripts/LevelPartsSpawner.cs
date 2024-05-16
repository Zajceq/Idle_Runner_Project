using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class LevelPartsSpawner : Singleton<LevelPartsSpawner>
{
    [SerializeField] List<GameObject> levelParts;
    [SerializeField] int defaultPoolCapacity = 3;
    [SerializeField] int maxPoolSize = 5;

    private List<IObjectPool<GameObject>> objectPools = new List<IObjectPool<GameObject>>();

    private void Start()
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

    public void SpawnRandomLevelPart(Transform spawnTransform)
    {
        int randomPartNumber = Random.Range(0, objectPools.Count - 1);
        var poolForSpawning = objectPools[randomPartNumber];
        var partToSpawn = poolForSpawning.Get();

        partToSpawn.transform.position = spawnTransform.position;

        if (partToSpawn.GetComponentInChildren<LevelPartsSpawnerTrigger>().Pool == null) 
        { 
            partToSpawn.GetComponentInChildren<LevelPartsSpawnerTrigger>().Pool = poolForSpawning;
        }
    }
}
