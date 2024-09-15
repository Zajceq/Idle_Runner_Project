using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance; // Static reference to the singleton instance

    public static T Instance
    {
        get
        {
            // If instance doesn't exist, find or create it
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>(); // Try to find an existing instance
                if (_instance == null)
                {
                    // If no instance exists, create a new GameObject for the singleton
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>(); // Add the singleton component
                    singletonObject.name = typeof(T).ToString() + " (Singleton)"; // Name it for clarity
                    DontDestroyOnLoad(singletonObject); // Prevent it from being destroyed when loading new scenes
                }
            }
            return _instance; // Return the singleton instance
        }
    }

    protected virtual void Awake()
    {
        // Ensure only one instance exists and persists between scenes
        if (_instance == null)
        {
            _instance = this as T; // Set the instance to this object
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
}