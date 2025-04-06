using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public int totalCoins = 0;           // Total coins
    public TextMeshProUGUI coinText;     // Reference to the TextMeshProUGUI component

    // Called to add coins
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log($"Total Coins: {totalCoins}");
        UpdateCoinText();
    }

    // Update the displayed coin count in the UI
    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + totalCoins.ToString();
        }
    }
}
