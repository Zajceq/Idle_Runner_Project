using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms.BaseAtoms;

public class GameManager : MonoBehaviour
{
    [SerializeField] private IntVariable score; // Game score variable from UnityAtoms

    private void Start()
    {
        // Initialize the score to 0 when the game starts
        score.Value = 0;

        // Set the audio volume based on the saved value in SaveManager
        AudioListener.volume = SaveManager.Instance.GetVolume();

        // Get the refresh rate of the current screen and set it as the target frame rate
        float refreshRate = Screen.currentResolution.refreshRateRatio.numerator / (float)Screen.currentResolution.refreshRateRatio.denominator;
        Application.targetFrameRate = Mathf.RoundToInt(refreshRate);
    }

    public void PauseGame()
    {
        // Pause the game by stopping the time
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        // Resume the game by setting time scale back to normal
        Time.timeScale = 1f;
    }

    public void RestartCurrentScene()
    {
        // Get the name of the current scene
        var currentScene = SceneManager.GetActiveScene().name;

        // Reload the current scene
        SceneManager.LoadScene(currentScene);

        // Reset the score
        score.Value = 0;
    }

    public void OnApplicationQuit()
    {
        // Quit the application
        Application.Quit();
    }
}