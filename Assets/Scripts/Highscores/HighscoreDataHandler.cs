using UnityEngine;
using Utilities;

namespace Highscores
{
    internal static class HighscoreDataHandler
    {
        private static string m_FilePath = Application.streamingAssetsPath + "/highscores.json";

        /// <summary>
        /// Searches for file, if it doesn't exist create a new one
        /// </summary>
        public static HighscoresJsonBinding RetrieveHighscores()
        {
            return JsonUtil.GetOrCreateJsonFile<HighscoresJsonBinding>(m_FilePath);
        }

        /// <summary>
        /// If score is higher than one of the top three,
        /// replace it and move the others position accordingly
        /// </summary>
        public static void HandleNewScore(float score)
        {
            var binding = JsonUtil.GetOrCreateJsonFile<HighscoresJsonBinding>(m_FilePath);

            if (score > binding.Highscore1)
            {
                binding.Highscore3 = binding.Highscore2;
                binding.Highscore2 = binding.Highscore1;
                binding.Highscore1 = score;
            }
            else if (score > binding.Highscore2)
            {
                binding.Highscore3 = binding.Highscore2;
                binding.Highscore2 = score;
            }
            else if (score > binding.Highscore3)
            {
                binding.Highscore3 = score;
            }

            JsonUtil.SaveJson(binding, m_FilePath);
        }
    }
}
