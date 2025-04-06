using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton to access ScoreManager globally
    public int currentScore = 0;         // Current score
    public TextMeshProUGUI scoreText;    // Reference to the TextMeshProUGUI component to display the score

    private void Awake()
    {
        // Ensure there's only one instance of ScoreManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Update the score text in the UI
        scoreText.text = "Score: " + currentScore;
    }

    // Method to increase score
    public void IncreaseScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Score: " + currentScore); // For debugging, you can see score changes in the console
    }
}
