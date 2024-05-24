using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    private TextMeshProUGUI textMeshUI;

    private void Start()
    {
        textMeshUI = GetComponent<TextMeshProUGUI>();
        DisplayHighScore();
    }

    private void DisplayHighScore()
    {
        int highscore = PlayerPrefs.GetInt("HighScore", 0);
        textMeshUI.text = "High Score: " + highscore;
    }
}
