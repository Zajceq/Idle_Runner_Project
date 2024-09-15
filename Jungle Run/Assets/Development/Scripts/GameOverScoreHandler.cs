using UnityEngine;
using TMPro;
using UnityAtoms.BaseAtoms;

public class GameOverScoreHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText; // Reference to the TextMeshPro UI element for the current score
    [SerializeField] private TextMeshProUGUI bestScoreText; // Reference to the TextMeshPro UI element for the best score

    [SerializeField] private IntVariable score; // Current game score (UnityAtoms variable)

    public void UpdateScoreUI()
    {
        // Update the current score text UI with the current game score
        currentScoreText.text = "Current Score: " + score.Value.ToString();

        // Update the best score text UI with the highest score saved in the SaveManager
        bestScoreText.text = "Best Score: " + SaveManager.Instance.GetHighScore().ToString();
    }
}