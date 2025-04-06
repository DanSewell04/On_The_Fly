using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject player;               // Reference to the player
    public GameObject projectilePrefab;     // Reference to the projectile prefab
    public Transform shootingPoint;         // Point from where the projectile will be shot
    public float shootCooldown = 2f;        // Cooldown between shots
    public int scoreValue = 150;            // Points awarded when the enemy is destroyed

    private float timeSinceLastShot = 0f;   // Timer for the shooting cooldown

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        // Check if the cooldown is over and the enemy can shoot
        if (timeSinceLastShot >= shootCooldown)
        {
            ShootProjectile();
            timeSinceLastShot = 0f;  // Reset cooldown timer
        }
    }

    // Method to shoot a projectile towards the player
    void ShootProjectile()
    {
        if (player != null && projectilePrefab != null && shootingPoint != null)
        {
            // Calculate the direction towards the player
            Vector3 direction = (player.transform.position - shootingPoint.position).normalized;

            // Instantiate the projectile and shoot it towards the player
            GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);

            // Get the Rigidbody of the projectile
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            // If the projectile has a Rigidbody, set its velocity to the direction
            if (projectileRb != null)
            {
                projectileRb.linearVelocity = direction * 10f; // 10f is the speed of the projectile
            }
        }
    }

    // When the enemy is destroyed, increase the score
    private void OnDestroy()
    {
        // Add score when the enemy is destroyed
        ScoreManager.Instance.IncreaseScore(scoreValue);
        Debug.Log("Shooting enemy destroyed, score increased!");
    }
}
