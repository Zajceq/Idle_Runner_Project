using UnityEngine;
using UnityAtoms.BaseAtoms;

public class SaveManager : Singleton<SaveManager>
{
    private const string HIGH_SCORE_KEY = "HighScore";
    private const string VOLUME_KEY = "Volume";

    public void SaveHighScore(IntVariable score)
    {
        int currentHighScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);

        if (score.Value > currentHighScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, score.Value);
            PlayerPrefs.Save();
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    public void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat(VOLUME_KEY, 0.5f);
    }
}
