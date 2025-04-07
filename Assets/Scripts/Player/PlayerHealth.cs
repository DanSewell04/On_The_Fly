using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;  // Singleton instance
    public int maxHealth = 100;      // Max health for the player.
    public int currentHealth;       // Current health of the player.

    public bool isDead = false;      // Flag to check if the player is dead.

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Player Health Initialized: " + currentHealth);
    }

    private void Awake()
    {
        // Set the singleton instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player has died.");
        Destroy(gameObject);
    }

}
