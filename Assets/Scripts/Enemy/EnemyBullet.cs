using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 10f;  // The amount of damage the bullet does

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collides with the player
        if (collision.collider.CompareTag("Player"))
        {
            // Get the PlayerHealth component on the player
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Apply damage to the player
                playerHealth.TakeDamage((int)damage);
            }
            else
            {
                Debug.LogError("PlayerHealth component not found!");
            }

            // Destroy the bullet after it collides with the player
            Destroy(gameObject);
        }
    }
}
