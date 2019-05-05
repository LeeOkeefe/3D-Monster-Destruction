using UnityEngine;

internal sealed class HighScore
{
    public HighScore(float score)
    {
        if (score > PlayerPrefs.GetFloat("HighScore1"))
        {
            PlayerPrefs.SetFloat("HighScore1", ScoreManager.PlayerTotalScore);
        }
        else if (score > PlayerPrefs.GetFloat("HighScore2"))
        {
            PlayerPrefs.SetFloat("HighScore2", ScoreManager.PlayerTotalScore);
        }
        else if (score > PlayerPrefs.GetFloat("HighScore3"))
        {
            PlayerPrefs.SetFloat("HighScore3", ScoreManager.PlayerTotalScore);
        }

        PlayerPrefs.Save();
    }
}
