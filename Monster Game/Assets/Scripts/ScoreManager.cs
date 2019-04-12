using System;
using UnityEngine.UI;

internal static class ScoreManager
{
    public static float PlayerTotalScore { get; private set; }
    private static Text ScoreText => GameManager.instance.scoreText;

    /// <summary>
    /// Set score and text value to be 0 to start
    /// </summary>
    public static void Initialize()
    {
        ResetScore();
        ScoreText.text = PlayerTotalScore.ToString();
    }

    /// <summary>
    /// Add specified amount of score to the overall player score
    /// </summary>
    public static void AddScore(float score)
    {
        if (score <= 0)
        {
            throw new ArgumentException("Score was equal to or less than 0.");
        }

        PlayerTotalScore += score;
        ScoreText.text = PlayerTotalScore.ToString();
    }

    /// <summary>
    /// If we are given 100 score and 10%, we generate a random number
    /// between 90 and 110 and add it to the PlayerTotalScore
    /// </summary>
    public static void AddScore(float score, float percentage)
    {
        if (score <= 0)
        {
            throw new ArgumentException("Score was equal to or less than 0.");
        }

        var minScore = score - (score / 100 * percentage);
        var maxScore = score + (score / 100 * percentage);

        var rand = new Random();
        var scoreAwarded = rand.Next((int) minScore, (int) maxScore);

        AddScore(scoreAwarded);
    }

    /// <summary>
    /// Subtract score amount from the overall player score
    /// </summary>
    public static void SubtractScore(int amount)
    {
        PlayerTotalScore -= amount;
        ScoreText.text = PlayerTotalScore.ToString();

        if (PlayerTotalScore <= 0)
            PlayerTotalScore = 0;
    }

    /// <summary>
    /// Checks whether the player has a specified amount of score
    /// </summary>
    public static bool HasScore(float amount)
    {
        return PlayerTotalScore >= amount;
    }

    /// <summary>
    /// Resets the player's score to 0
    /// </summary>
    public static void ResetScore()
    {
        PlayerTotalScore = 0;
    }
}