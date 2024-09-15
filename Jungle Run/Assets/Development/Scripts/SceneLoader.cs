using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName; // The name of the scene to load

    public void LoadScene()
    {
        // Load the scene with the specified name
        SceneManager.LoadScene(sceneName);
    }
}