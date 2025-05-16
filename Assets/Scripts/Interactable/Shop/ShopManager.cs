using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // Player's coin system and upgrade scripts
    public CoinCounter coinCounter;  // Reference to the coin counter
    public PlayerHealth playerHealth;  // Reference to the player's health
    public Bullet playerBullet;  // Reference to the player's bullet/damage system

    // Price parameters for upgrades
    public int healthUpgradePrice = 50;  // Starting price for health upgrade
    public int damageUpgradePrice = 50;  // Starting price for damage upgrade
    public int priceIncreaseFactor = 10;  // Price increase after each upgrade

    // UI References
    public GameObject shopUI;  // Reference to the Shop UI Panel
    public TextMeshProUGUI healthPriceText;
    public TextMeshProUGUI damagePriceText;
    public TextMeshProUGUI playerCoinsText;

    // Function to display the shop UI and pause the game
    public void OpenShop()
    {
        Time.timeScale = 0f;  // Pause the game
        shopUI.SetActive(true);  // Activate the shop UI
        UpdateUpgradePrices();

        // Show and unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Function to close the shop UI and resume the game
    public void CloseShop()
    {
        Time.timeScale = 1f;  // Resume the game
        shopUI.SetActive(false);  // Deactivate the shop UI

        // Re-lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update the UI with the current prices for upgrades and the available coins
    private void UpdateUpgradePrices()
    {
        healthPriceText.text = "Upgrade Health: " + healthUpgradePrice + " Coins";
        damagePriceText.text = "Upgrade Damage: " + damageUpgradePrice + " Coins";
        playerCoinsText.text = "Coins: " + coinCounter.totalCoins;
    }

    // Upgrade the player's health
    public void UpgradeHealth()
    {
        if (coinCounter.totalCoins >= healthUpgradePrice)
        {
            // Deduct the cost of the upgrade
            coinCounter.totalCoins -= healthUpgradePrice;

            // Increase max health by a fixed amount (e.g., +50 health)
            playerHealth.maxHealth += 50;
            playerHealth.currentHealth = playerHealth.maxHealth;  // Restore health to max


            // Increase the price for the next health upgrade
            healthUpgradePrice += priceIncreaseFactor;

            // Update the UI
            UpdateUpgradePrices();
            coinCounter.UpdateCoinText();
        }
        else
        {
            Debug.Log("Not enough coins for health upgrade!");
        }
    }

    // Upgrade the player's damage
    public void UpgradeDamage()
    {
        // Check for null references
        if (coinCounter == null)
        {
            Debug.LogError("CoinCounter is not assigned in the ShopManager.");
            return;
        }

        if (PlayerStats.instance == null)
        {
            Debug.LogError("PlayerStats.instance is not assigned.");
            return;
        }

        if (coinCounter.totalCoins >= damageUpgradePrice)
        {
            coinCounter.totalCoins -= damageUpgradePrice;
            PlayerStats.instance.bulletDamage += 5;
            damageUpgradePrice += priceIncreaseFactor;
            UpdateUpgradePrices();
            coinCounter.UpdateCoinText();
        }
        else
        {
            Debug.Log("Not enough coins for damage upgrade!");
        }
    }
}
