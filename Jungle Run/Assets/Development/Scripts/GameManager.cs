using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms.BaseAtoms;

public class GameManager : MonoBehaviour
{
    [SerializeField] private IntVariable score;

    private void Start()
    {
        score.Value = 0;
        AudioListener.volume = SaveManager.Instance.GetVolume();

        float refreshRate = Screen.currentResolution.refreshRateRatio.numerator / (float)Screen.currentResolution.refreshRateRatio.denominator;
        Application.targetFrameRate = Mathf.RoundToInt(refreshRate);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void RestartCurrentScene()
    {
        var currentScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentScene);
        score.Value = 0;
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
