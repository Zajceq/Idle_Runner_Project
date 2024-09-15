using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelPartsSpawner : Singleton<LevelPartsSpawner>
{
    [SerializeField] List<GameObject> levelParts; // List of level parts to spawn
    [SerializeField] int defaultPoolCapacity = 3; // Default number of objects in each pool
    [SerializeField] int maxPoolSize = 5; // Maximum number of objects in each pool

    private List<IObjectPool<GameObject>> objectPools = new List<IObjectPool<GameObject>>(); // List of object pools for each level part

    private void OnEnable()
    {
        // Subscribe to scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from scene load event to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset object pools when a new scene is loaded
        ResetPools();
    }

    private void Start()
    {
        InitializePools(); // Initialize object pools at the start of the game
    }

    private void InitializePools()
    {
        // For each level part, create an object pool
        foreach (GameObject levelPart in levelParts)
        {
            if (levelPart != null)
            {
                // Create an object pool for each level part
                objectPools.Add(new ObjectPool<GameObject>(
                    () => Instantiate(levelPart), // Create new instance
                    levelPart => levelPart.SetActive(true), // On get (activate object)
                    levelPart => levelPart.SetActive(false), // On release (deactivate object)
                    levelPart => Destroy(levelPart), // On destroy
                    true, defaultPoolCapacity, maxPoolSize)); // Pool settings
            }
        }
    }

    private void ResetPools()
    {
        // Clear all object pools and reinitialize them
        foreach (var pool in objectPools)
        {
            pool.Clear();
        }
        objectPools.Clear();
        InitializePools();
    }

    public void SpawnRandomLevelPart(Transform spawnTransform)
    {
        // Pick a random level part from the pools
        int randomPartNumber = Random.Range(0, objectPools.Count);
        var poolForSpawning = objectPools[randomPartNumber];

        // Get a part from the pool and set its position
        var partToSpawn = poolForSpawning.Get();
        partToSpawn.transform.position = spawnTransform.position;

        // Get the trigger component and assign the pool it came from
        var trigger = partToSpawn.GetComponentInChildren<LevelPartsSpawnerTrigger>();
        trigger.Pool = poolForSpawning;
    }
}