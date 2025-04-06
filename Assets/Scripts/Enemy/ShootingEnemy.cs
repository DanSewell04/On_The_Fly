using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;  // Bullet prefab to instantiate
    public Transform shootPoint;     // Point from where the bullet will shoot
    public float shootForce = 10f;   // Force applied to the bullet
    public float shootCooldown = 2f; // Cooldown between shots
    public float shootingRange = 15f; // Range at which the enemy can shoot

    private Transform player;        // Player's transform reference
    private float nextShootTime;     // Time for the next possible shot

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;

        // Check if the player is within shooting range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > shootingRange) return;

        // Aim at the player
        AimAtPlayer();

        // Shoot if cooldown is over
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootCooldown; // Reset the cooldown timer
        }
    }

    private void AimAtPlayer()
    {
        // Get the direction from the enemy to the player and normalize it
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Make sure the enemy doesn't rotate on the y-axis (vertical axis)
        transform.rotation = Quaternion.LookRotation(direction); // Rotate the enemy to face the player
    }

    private void Shoot()
    {
        // Instantiate the bullet at the shoot point with the correct rotation
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // Ensure the bullet has a Rigidbody for movement
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse); // Apply force to the bullet
        }

        // Destroy the bullet after 3 seconds to avoid clutter
        Destroy(bullet, 3f);

        Debug.Log("Enemy fired a bullet!");
    }
}
