using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text livesText;
    public TMP_Text enemiesKilledText;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public GameObject pauseMenu;

    private int enemiesKilledCount = 0;
    private int livesCount = 5;
    private int scoreCount = 0;

    void Start()
    {
        Debug.Log("UIManager subscribing to events...");

        // Subscribe to events (OBSERVER PATTERN)
        EventManager.Subscribe("OnScoreChanged", UpdateScore);
        EventManager.Subscribe("OnLivesChanged", UpdateLives);
        EventManager.Subscribe("OnGameOver", ShowGameOver); // GAME OVER
        EventManager.Subscribe("OnLevelComplete", ShowVictory); // VICTORY
        EventManager.Subscribe("OnEnemiesKilled", UpdateEnemiesKilled);
        EventManager.Subscribe("OnGamePaused", OnGamePaused);
        EventManager.Subscribe("OnGameResumed", OnGameResumed);

        // Initialize
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            Debug.Log("Game Over Panel disabled");
        }

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
            Debug.Log("Victory Panel disabled");
        }

        if (enemiesKilledText != null)
        {
            enemiesKilledText.text = "Wasps Defeated: 0";
        }

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
            Debug.Log("Pause Menu Disabled");
        }
    }

    void Update()
    {
        // Update timer
        if (timeText && GameManager.Instance != null)
        {
            float time = GameManager.Instance.GetTimeRemaining();
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent errors on scene change
        EventManager.Unsubscribe("OnScoreChanged", UpdateScore);
        EventManager.Unsubscribe("OnLivesChanged", UpdateLives);
        EventManager.Unsubscribe("OnGameOver", ShowGameOver);
        EventManager.Unsubscribe("OnLevelComplete", ShowVictory);
        EventManager.Unsubscribe("OnEnemiesKilled", UpdateEnemiesKilled);
    }

    void UpdateScore(object scoreData)
    {
        scoreCount++;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + scoreData.ToString();
        }
    }

    void UpdateLives(object stateData)
    {
        livesCount++;
        if (livesText != null)
        {
            livesText.text = "Lives: " + stateData.ToString();
        }
    }

    void UpdateEnemiesKilled(object coinValue)
    {
        enemiesKilledCount++;
        if (enemiesKilledText != null)
        {
            enemiesKilledText.text = "Wasps Defeated: " + enemiesKilledCount;
        }
    }

    void ShowGameOver(object finalScore)
    {
        Debug.Log("SHOWING GAME OVER PANEL");

        // Make sure victory panel is hidden
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        // Show game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("Game Over Panel activated");
        }
        else
        {
            Debug.LogError("Game Over Panel is NULL!");
        }
    }

    void ShowVictory(object finalScore)
    {
        Debug.Log("SHOWING VICTORY PANEL");

        // Make sure game over panel is hidden
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Show victory panel
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Debug.Log("Victory Panel activated");
        }
        else
        {
            Debug.LogError("Victory Panel is NULL!");
        }
    }

    void OnGamePaused()
    {
        Debug.Log("SHOWING PAUSE PANEL");

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            Debug.Log("Its paused now");
        }
    }

    void OnGameResumed()
    {
        Debug.Log("BYE BYE PAUSE PANEL");

        if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
            Debug.Log("Resumed");
        }
    }

    public void OnRestartButton()
    {
        if (GameManager.Instance != null)
        {

            //scoreCount = 0;
            //livesCount = 5;
            //enemiesKilledCount = 0;
            //GameManager.Instance.RestartGame();
        }
    }

    public int GetEnemiesKilled()
    {
        return enemiesKilledCount;
    }
}