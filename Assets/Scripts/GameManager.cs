using System;
using System.Collections.Generic;
using Extensions;
using Player;
using UI.Ability_Bar;
using UI.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerController player;
    public PlayerStats playerStats;
    public Text scoreText;
    public Text gameOverScoreText;
    public Text gameTimeText;
    public Button pauseButton;
    public ActiveAbilities activeAbilities;
    public CanvasGroup gameOverUi;
    public GameObject playerPickupHand;
    public GameObject playerShootingPosition;
    public Animator playerAnim;
    public MouseSensitivity mouseSensitivity;
    public Collider playerRightHand;
    public Collider playerLeftHand;
    public GameObject playerPrefab;
    public Image gameOverBackground;
    public GameObject minimap;

    public Dictionary<string, KeyCode> KeyCodes =>
        GameObject.FindGameObjectWithTag("KeyBind").GetComponent<KeyBinding>().m_KeyCodes;

    public void CameraShake() => StartCoroutine(Camera.main.Shake(0.5F, 2));
    public bool IsGamePaused => Math.Abs(Time.timeScale) < 0;
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
        if (Instance != null)
            Destroy(this);

        if (Instance == null)
            Instance = this;
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

        if (TimeRemaining <= 0 || playerStats.CurrentHealth <= 0)
        {
            gameOverBackground.fillAmount += Time.unscaledDeltaTime / 2.5F;
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
        gameOverScoreText.text = scoreText.text;
        minimap.SetActive(false);
        Time.timeScale = 0;
    }

    /// <summary>
    /// Checks that our player is in within a specified range from us
    /// </summary>
    public bool IsPlayerInRange(Transform myTransform, float distance)
    {
        if (player == null)
        {
            throw new NullReferenceException("Player was null");
        }

        return Vector3.Distance(myTransform.position, player.transform.position) < distance;
    }
}
