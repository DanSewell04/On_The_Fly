using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;      // Max health for the player.
    private int currentHealth;       // Current health of the player.

    public bool isDead = false;      // Flag to check if the player is dead.

    void Start()
    {
        currentHealth = maxHealth;   
        Debug.Log("Player Health Initialized: " + currentHealth);
    }

    // Function to take damage.
    public void TakeDamage(int damage)
    {
        if (isDead) return; // Prevent damage if player is already dead.

        currentHealth -= damage;  // Reduce health by damage amount.
        Debug.Log("Player took " + damage + " damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();  // Trigger death when health reaches 0.
        }
    }

    // Function to handle player death.
    private void Die()
    {
        isDead = true;
        Debug.Log("Player has died.");

        Destroy(gameObject);
    }

}
