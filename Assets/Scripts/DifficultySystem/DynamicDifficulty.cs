using UnityEngine;

public class DynamicDifficulty : MonoBehaviour
{
    public float playerScore; 
    public float minScore = 0;
    public float maxScore = 10;

    public float baseEnemyHealth = 100;
    public float baseEnemySpeed = 3f;
    public float baseSpawnRate = 2f;

    public float minDifficulty = 0.5f;
    public float maxDifficulty = 2f;

    public float smoothingFactor = 0.1f;
    private float currentDifficulty = 1f;

    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    private float nextSpawnTime = 0f;

    void Update()
    {
        // Normalize player performance
        float normalizedPerformance = Mathf.Clamp01((playerScore - minScore) / (maxScore - minScore));

        // Calculate target difficulty
        float targetDifficulty = Mathf.Lerp(minDifficulty, maxDifficulty, normalizedPerformance);

        // Smoothly adjust difficulty
        currentDifficulty = Mathf.Lerp(currentDifficulty, targetDifficulty, Time.deltaTime * smoothingFactor);

        // Apply difficulty to game parameters
        float enemyHealth = baseEnemyHealth * currentDifficulty;
        float enemySpeed = baseEnemySpeed * currentDifficulty;
        float spawnRate = baseSpawnRate / currentDifficulty;

        // Spawn enemies
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy(enemyHealth, enemySpeed);
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy(float health, float speed)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.health = health;
        enemyScript.speed = speed;
    }

    // Call this method when the player kills an enemy
    public void IncreasePlayerScore()
    {
        playerScore++;
    }
}
