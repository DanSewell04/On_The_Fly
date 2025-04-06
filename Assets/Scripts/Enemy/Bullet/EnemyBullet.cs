using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 20;  // Damage to deal on collision

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Check if the projectile hits the player
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player hit by projectile!");
            }
        }
        Destroy(gameObject);  // Destroy the projectile after it collides
    }
}
