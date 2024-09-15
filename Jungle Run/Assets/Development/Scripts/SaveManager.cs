using UnityEngine;
using UnityAtoms.BaseAtoms;

public class SaveManager : Singleton<SaveManager>
{
    private const string HIGH_SCORE_KEY = "HighScore"; // Key for saving high score in PlayerPrefs
    private const string VOLUME_KEY = "Volume"; // Key for saving volume in PlayerPrefs

    public void SaveHighScore(IntVariable score)
    {
        // Get the current saved high score (default is 0 if not set)
        int currentHighScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);

        // If the current score is higher, update the high score
        if (score.Value > currentHighScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, score.Value); // Save the new high score
            PlayerPrefs.Save(); // Ensure PlayerPrefs are saved
        }
    }

    public int GetHighScore()
    {
        // Retrieve the high score (default is 0 if not set)
        return PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    public void SaveVolume(float volume)
    {
        // Save the volume level to PlayerPrefs
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        PlayerPrefs.Save(); // Ensure the changes are saved
    }

    public float GetVolume()
    {
        // Retrieve the saved volume (default is 0.5 if not set)
        return PlayerPrefs.GetFloat(VOLUME_KEY, 0.5f);
    }
}