using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Playertime
{

    public class GameManager : MonoBehaviour
    {
        // Singleton instance
        public static GameManager Instance { get; private set; }

        [Header("Game Stats")]
        public int score = 0;
        public int lives = 5;
        public GameObject coinPrefab, newCoin;
        public int enemiesKilled = 0;
        public float coinSpawnRate = 2f;
        private float nextCoinTime = 0f;
        private Vector2 spawnPoint = new Vector2(-15f,-7f);

        [Header("UI References")]
        //public Text scoreText;
        public TMP_Text livesText;
        public TMP_Text enemiesKilledText;
        public GameObject gameOverPanel;
        public GameObject gameWonPanel;
        public TMP_Text scoreText;

        //private int score = 0;
        private float timeRemaining;
        private bool isGameActive = true;
        private bool isPaused = false;
        public GameObject Player;
        

        private void Awake()
        {
            // Singleton pattern implementation
            if (Instance == null)
            {
                Instance = this;
                //DontDestroyOnLoad(gameObject); // Persist across scenes
            }
            else
            {
                //Destroy(gameObject); // Destroy duplicate GameManagers
            }
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            RefreshUIReferences();
            UpdateUI();
        }

        private void Update()
        {
            //InitializeGame();
            // UpdateUI();
            if (isGameActive && !isPaused)
            {
                timeRemaining += Time.deltaTime;
            }

        }

        private void RefreshUIReferences()
        {
            scoreText = GameObject.Find("Score")?.GetComponent<TMP_Text>();
            livesText = GameObject.Find("Lives")?.GetComponent<TMP_Text>();
            enemiesKilledText = GameObject.Find("EnemiesKilled")?.GetComponent<TMP_Text>();
            gameOverPanel = GameObject.Find("GameEndPanel");
            gameWonPanel = GameObject.Find("GameWonPanel");
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(false);
            }
            if (gameWonPanel != null)
            {
                gameWonPanel.SetActive(false);
            }
        }

        public void AddScore(int points)
        {
            score += points;
            Debug.Log($"Score increased by {points}. Total: {score}");
            UpdateUI();

            if (score > 10000)
                PlayerWon();

            if (Time.time >= nextCoinTime)
            {
                GenerateCoins();
                nextCoinTime = Time.time + coinSpawnRate;
            }
        }

        public void PlayerWon()
        {
            gameWonPanel.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            Debug.Log("You win!");
        }

        public void LoseLife()
        {
            lives--;
            Debug.Log($"Life lost! Lives remaining: {lives}");
            UpdateUI();

            if (lives <= 0)
            {
                GameOver();
            }
        }

        public void EnemyKilled()
        {
            enemiesKilled++;
            AddScore(25); // 100 points per enemy
            Debug.Log($"Enemy killed! Total enemies defeated: {enemiesKilled}");
        }



        public void quitGame()
        {
            Application.Quit();
            Debug.Log("Bye Bye!!");
        }

        public void replayLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            print("Let's Go Again!");
        }


        public void CollectiblePickedUp(int value)
        {
            AddScore(value);
            Debug.Log($"Collectible picked up worth {value} points!");
        }

        private void UpdateUI()
        {
            if (scoreText) scoreText.text = "Score: " + score;
            if (livesText) livesText.text = "Lives: " + lives;
            if (enemiesKilledText) enemiesKilledText.text = "Wasps Defeated: " + enemiesKilled;
        }

        private void GameOver()
        {
            Debug.Log("GAME OVER!");
            if (gameOverPanel) gameOverPanel.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }

        public void RestartGame()
        {
            /* score = 0;
            lives = 3;
            enemiesKilled = 0;
            Time.timeScale = 1f;
            if (gameOverPanel) gameOverPanel.SetActive(false);
            UpdateUI();*/

            EventManager.ClearAllEvents();

            // CRITICAL: Unpause the game first!
            Time.timeScale = 1f;

            // Reset all game state
            score = 0;
            lives = 5;
            enemiesKilled = 0;

            // Hide game over panel
            //if (gameOverPanel) gameOverPanel.SetActive(false);

            // Destroy all enemies, bullets, and collectibles before reloading
            //DestroyAllGameObjects();

            // Reload the current scene
            SceneManager.LoadScene("PlayableLevel");
        }

        public void GenerateCoins()
        {
            //Permission received from Allie Metzger and instructor to look at and use code

            // int randomIndex = Random.Range(0, spawnPoints.Length);

            newCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            //newCoin.transform.position.x = (Random.Range(-8f, 8f), Random.Range(-2,4), transform.position.);
            newCoin.transform.position = new Vector2(Random.Range(-15f, 15f), Random.Range(-4, 8));
        }
        private void DestroyAllGameObjects()
        {
            // Destroy all enemies
            // GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            //foreach (GameObject enemy in enemies)
            //{
            //Destroy(enemy);
            //}

            // Destroy all bullets
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }

            // Destroy all collectibles
            GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");
            foreach (GameObject collectible in collectibles)
            {
                Destroy(collectible);
            }
        }

        public int GetScore() => score;
        public float GetTimeRemaining() => timeRemaining;
        public bool IsGameActive() => isGameActive;
        public bool IsPaused() => isPaused;
    }
}