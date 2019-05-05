using UnityEngine;
using UnityEngine.UI;

namespace Highscores
{
    internal sealed class HighScoreManager : MonoBehaviour
    {
        [SerializeField]
        private Text rank1, rank2, rank3;

        // Load top three high scores and set the text components to their value
        //
        private void Start ()
        {
            var score1 = PlayerPrefs.GetFloat("HighScore1");
            var score2 = PlayerPrefs.GetFloat("HighScore2");
            var score3 = PlayerPrefs.GetFloat("HighScore3");

            rank1.text = $"Rank1: {score1}";
            rank2.text = $"Rank2: {score2}";
            rank3.text = $"Rank3: {score3}";
        }
    }
}
