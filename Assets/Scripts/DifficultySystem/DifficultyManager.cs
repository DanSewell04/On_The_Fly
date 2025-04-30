using TMPro;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{

    // UI Elements (using TMPro instead of Unity UI Text)
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI kdText;

    // Player performance metrics
    private float playerHealthPercentage = 1f;
    private float playerAccuracy = 0f;
    private float killDeathRatio = 1f;
    private float timeSinceLastDeath = 0f;

    // Difficulty parameters (0-1 scale)
    [Range(0f, 1f)] public float currentDifficulty = 0.5f;

    // Enemy adjustment parameters
    public float minEnemyHealth = 50f;
    public float maxEnemyHealth = 150f;
    public float minEnemyAccuracy = 0.2f;
    public float maxEnemyAccuracy = 0.8f;
    public float minEnemyAggression = 0.3f;
    public float maxEnemyAggression = 0.9f;
    public int minEnemiesPerWave = 3;
    public int maxEnemiesPerWave = 10;

    // Tracking variables
    private int playerKills = 0;
    private int playerDeaths = 0;
    private int shotsFired = 0;
    private int shotsHit = 0;
    private float maxPlayerHealth;

    // Update intervals
    private float updateInterval = 5f;
    private float lastUpdateTime = 0f;

    private void Start()
    {
        // Initialize with player's starting health
        maxPlayerHealth = PlayerHealth.instance.maxHealth;

        // Optionally, set the UI elements on start
        UpdateUI();
    }

    private void Update()
    {
        timeSinceLastDeath += Time.deltaTime;

        // Update difficulty at intervals
        if (Time.time - lastUpdateTime > updateInterval)
        {
            UpdatePlayerMetrics();
            CalculateDifficulty();
            lastUpdateTime = Time.time;

            // Update UI every update interval
            UpdateUI();
        }
    }

    private void UpdatePlayerMetrics()
    {
        // Get current player health percentage
        playerHealthPercentage = PlayerHealth.instance.currentHealth / maxPlayerHealth;

        // Calculate accuracy if shots have been fired
        if (shotsFired > 0)
        {
            playerAccuracy = (float)shotsHit / shotsFired;
        }

        // Calculate K/D ratio (avoid division by zero)
        killDeathRatio = playerDeaths > 0 ? (float)playerKills / playerDeaths : playerKills;
    }

    private void CalculateDifficulty()
    {
        // Base difficulty factors (all normalized 0-1)
        float healthFactor = 1f - playerHealthPercentage; // Lower health = higher difficulty
        float accuracyFactor = playerAccuracy;
        float kdFactor = 1f - Mathf.Clamp01(killDeathRatio / 5f); // Assuming 5 is "good" K/D
        float survivalFactor = 1f - Mathf.Clamp01(timeSinceLastDeath / 300f); // 5 minutes

        // Weighted average of factors
        currentDifficulty = (healthFactor * 0.4f) +
                           (accuracyFactor * 0.3f) +
                           (kdFactor * 0.2f) +
                           (survivalFactor * 0.1f);

        // Clamp final value
        currentDifficulty = Mathf.Clamp01(currentDifficulty);
    }

    // Public methods to update tracking variables
    public void RegisterPlayerDeath()
    {
        playerDeaths++;
        timeSinceLastDeath = 0f;
    }

    public void RegisterPlayerKill()
    {
        playerKills++;
    }

    public void RegisterShotFired()
    {
        shotsFired++;
    }

    public void RegisterShotHit()
    {
        shotsHit++;
    }

    // Methods to get adjusted enemy parameters
    public float GetAdjustedEnemyHealth()
    {
        return Mathf.Lerp(minEnemyHealth, maxEnemyHealth, currentDifficulty);
    }

    public float GetAdjustedEnemyAccuracy()
    {
        return Mathf.Lerp(minEnemyAccuracy, maxEnemyAccuracy, currentDifficulty);
    }

    public float GetAdjustedEnemyAggression()
    {
        return Mathf.Lerp(minEnemyAggression, maxEnemyAggression, currentDifficulty);
    }

    public int GetAdjustedEnemyCount()
    {
        return Mathf.RoundToInt(Mathf.Lerp(minEnemiesPerWave, maxEnemiesPerWave, currentDifficulty));
    }

    // Method to update the UI
    private void UpdateUI()
    {
        // Update UI elements with current player metrics and difficulty
        if (difficultyText != null) difficultyText.text = "Difficulty: " + (currentDifficulty * 100).ToString("F1") + "%";
        if (healthText != null) healthText.text = "Health: " + (playerHealthPercentage * 100).ToString("F1") + "%";
        if (accuracyText != null) accuracyText.text = "Accuracy: " + (playerAccuracy * 100).ToString("F1") + "%";
        if (kdText != null) kdText.text = "K/D Ratio: " + killDeathRatio.ToString("F2");
    }

}
