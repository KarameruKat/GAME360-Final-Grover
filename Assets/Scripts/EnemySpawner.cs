using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject enemyPrefab;
    public float spawnRate = 4f;
    public Transform[] spawnPoints;

    public float nextSpawnTime = 0f;

    private void Start()
    {
        nextSpawnTime = 0f;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
        if (GameManager.Instance.score > 400 && GameManager.Instance.score < 900)
            spawnRate = 4f;
        if (GameManager.Instance.score > 900 && GameManager.Instance.score < 1400)
            spawnRate = 2f;
        if (GameManager.Instance.score > 1400 && GameManager.Instance.score < 4000)
            spawnRate = 1.4f;
        if (GameManager.Instance.score > 4000 && GameManager.Instance.score < 7000)
            spawnRate = 0.7f;
        if (GameManager.Instance.score > 7000 && GameManager.Instance.score < 10000)
            spawnRate = 0.45f;
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab && spawnPoints.Length > 0)
        {
            // Check if game is still running through Singleton
            if (GameManager.Instance.lives > 0)
            {
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
            }
        }
    }
}