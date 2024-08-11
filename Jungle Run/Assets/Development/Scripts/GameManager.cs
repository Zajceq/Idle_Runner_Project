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
