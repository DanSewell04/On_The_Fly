using UnityEngine;

public class MeleeRangeEnemy : MonoBehaviour
{
    [SerializeField] private GameObject player;          // The player GameObject
    public float moveSpeed = 3f;       // Movement speed of the enemy
    public float attackRange = 2f;     // Range in which the enemy can attack the player
    public float attackCooldown = 1f;  // Cooldown time between attacks
    public int attackDamage = 10;      // Damage dealt by the melee attack
    public int scoreValue = 100;       // Points to add when the enemy is defeated

    private float timeSinceLastAttack = 0f; // Timer for attack cooldown
    private bool isPlayerInRange = false;  // Flag to check if player is in attack range

    private void Update()
    {
        // Decrease timeSinceLastAttack to track cooldown
        timeSinceLastAttack += Time.deltaTime;

        // Move towards the player
        MoveTowardsPlayer();

        // Check if the player is within attack range
        CheckForAttackRange();

        // If player is in range and cooldown has passed, perform attack
        if (isPlayerInRange && timeSinceLastAttack >= attackCooldown)
        {
            Attack();
        }
    }

    // Move the enemy towards the player
    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    // Check if the player is within attack range
    private void CheckForAttackRange()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            isPlayerInRange = distance <= attackRange;
        }
    }

    // Perform the attack (on collision)
    private void Attack()
    {
        // Reset the attack cooldown timer
        timeSinceLastAttack = 0f;

        // Display a simple attack message or implement damage logic
        Debug.Log("Attacking Player!");

       
        if (isPlayerInRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }

    // Detect collision with the player and apply damage
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Check if the colliding object is the player
        {
           
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Player hit by enemy!");
            }
        }
    }

    // When enemy is defeated, add score
    private void OnDestroy()
    {
       
        ScoreManager.Instance.IncreaseScore(scoreValue);
        Debug.Log("Enemy destroyed, score increased!");
    }
}
