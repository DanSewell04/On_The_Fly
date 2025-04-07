using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float speed = 20f;
    public float lifeTime = 5f;

    private DifficultyManager difficultyManager;
    private Rigidbody rb;

    private void Start()
    {
        damage = PlayerStats.instance.bulletDamage;

        difficultyManager = Object.FindAnyObjectByType<DifficultyManager>();

        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speed;
        }

        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is an enemy
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Deal damage to the enemy
            enemy.TakeDamage(damage);

            // Ensure difficultyManager is assigned before calling RegisterShotHit()
            if (difficultyManager != null)
            {
                difficultyManager.RegisterShotHit();
            }
            else
            {
                Debug.LogError("DifficultyManager is not assigned in Bullet.");
            }
        }

        // Destroy the bullet after collision
        Destroy(gameObject);
    }
}
