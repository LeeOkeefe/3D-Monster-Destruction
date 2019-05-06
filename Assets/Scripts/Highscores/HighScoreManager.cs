using UnityEngine;
using UnityEngine.UI;

namespace Highscores
{
    internal sealed class HighScoreManager : MonoBehaviour
    {
        [SerializeField]
        private Text rank1, rank2, rank3;

        // Load top three high scores from Json binding
        // and set the text components to their value
        //
        private void Start ()
        {
            var binding = HighscoreDataHandler.RetrieveHighscores();

            rank1.text = $"Rank 1: {binding.Highscore1}";
            rank2.text = $"Rank 2: {binding.Highscore2}";
            rank3.text = $"Rank 3: {binding.Highscore3}";
        }
    }
}
