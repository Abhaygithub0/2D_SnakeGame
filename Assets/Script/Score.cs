using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI textUGI;
    private int score = 0;
    int highscore = 0;

    private void Awake()
    {
        textUGI = GetComponent<TextMeshProUGUI>();
        LoadHighScore();
    }

    
    public void incrementvalue(int increment)
    {
        score += increment;
        UpdateScoreValue();
        SaveHighScore();
    }

    public int getscore()
    {
        return score;
    }

    private void UpdateScoreValue()
    {
        textUGI.text = " " + score;
    }

    private void SaveHighScore()
    {
        highscore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highscore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    private void LoadHighScore()
    {
         highscore = PlayerPrefs.GetInt("HighScore", 0);
       
    }
}
