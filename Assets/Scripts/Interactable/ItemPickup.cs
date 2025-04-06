using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int amount = 1;  // The number of coins to add to the player's total

    // This is the method we will call from PlayerInteract
    public void Pickup(GameObject player)
    {
        // Get the CoinCounter component from the player
        CoinCounter coinCounter = player.GetComponent<CoinCounter>();
        if (coinCounter != null)
        {
            // Add the coins to the player's total
            coinCounter.AddCoins(amount);
            Debug.Log($"Picked up {amount} coin(s)");

            // Destroy the coin object after being picked up
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("CoinCounter not found on player.");
        }
    }
}
