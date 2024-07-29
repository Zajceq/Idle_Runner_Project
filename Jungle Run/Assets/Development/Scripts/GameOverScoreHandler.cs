using UnityEngine;
using TMPro;
using UnityAtoms.BaseAtoms;

public class GameOverScoreHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    [SerializeField] private IntVariable score;

    public void UpdateScoreUI()
    {
        currentScoreText.text = "Current Score: " + score.Value.ToString();
        bestScoreText.text = "Best Score: " + SaveManager.Instance.GetHighScore().ToString();
    }
}
