using UnityEngine;

namespace Highscores
{
    internal sealed class HighScore
    {
        // Check if the given score is higher than any of the top three stored,
        // overwrite the position if it is 
        //
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
}
