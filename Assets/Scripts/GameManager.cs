using System;
using Extensions;
using Player;
using UI.Ability_Bar;
using UI.Menu;
using UI.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal sealed class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController player;
    public PlayerStats playerStats;
    public Text scoreText;
    public Text gameOverScoreText;
    public Text gameTimeText;
    public Button pauseButton;
    public ActiveAbilities activeAbilities;
    public SettingsManager settingsManager;
    public CanvasGroup gameOverUi;
    public GameObject playerPickupHand;
    public Animator playerAnim;
    public MouseSensitivity mouseSensitivity;

    public bool IsGamePaused => Math.Abs(Time.timeScale) < 0;
    public float PlayerScore => ScoreManager.PlayerTotalScore;
    public float MouseSensitivity => mouseSensitivity.Sensitivity;
    public bool IsGameRunning => SceneManager.GetActiveScene().name == "Level 1";

    [SerializeField]
    private float gameTimer = 300f;

    public float TimeRemaining { get; private set; }

    private void Start()
    {
        ScoreManager.Initialize();
        TimeRemaining = gameTimer;
    }

    // Ensure we only have one instance of GameManager
    //
    public void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    // Only update timer/text when the main level is active
    // Reset the timer when the timer runs out, or we die
    //
    private void Update()
    {
        if (IsGameRunning)
        {
            TimeRemaining -= Time.deltaTime;
            gameTimeText.text = FormatGameTime();
        }

        if (!(TimeRemaining < 1))
            return;

        TimeRemaining = gameTimer;
        GameOver();
    }

    /// <summary>
    /// Converts the time remaining into minutes and seconds,
    /// then returns it as a formatted string
    /// </summary>
    private string FormatGameTime()
    {
        var minutes = Mathf.FloorToInt(TimeRemaining / 60f);
        var seconds = Mathf.FloorToInt(TimeRemaining - minutes * 60);
        return $"{minutes:0}:{seconds:00}";
    }

    /// <summary>
    /// Stops the game running, and toggles the game over UI
    /// </summary>
    public void GameOver()
    {
        gameOverUi.ToggleGroup(true);
        pauseButton.enabled = false;
        Time.timeScale = 0;
        gameOverScoreText.text = scoreText.text;
    }

    /// <summary>
    /// Checks that our player is in within a specified range from us
    /// </summary>
    public bool IsPlayerInRange(Transform myTransform, float distance)
    {
        return Vector3.Distance(myTransform.position, player.transform.position) < distance;
    }
}
