using UnityEngine;

public class MeleeRangeEnemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private DifficultyManager difficultyManager;

    // Base stats (will be modified by difficulty)
    public float baseMoveSpeed = 3f;
    public float baseAttackRange = 2f;
    public float baseAttackCooldown = 1f;
    public int baseAttackDamage = 10;
    public int scoreValue = 100;

    // Current stats (after difficulty adjustment)
    private float currentMoveSpeed;
    private float currentAttackRange;
    private float currentAttackCooldown;
    private int currentAttackDamage;

    private float timeSinceLastAttack = 0f;
    private bool isPlayerInRange = false;

    private void Start()
    {
        difficultyManager = FindAnyObjectByType<DifficultyManager>();
        ApplyDifficultySettings();
    }

    private void ApplyDifficultySettings()
    {
        float difficulty = difficultyManager.currentDifficulty;

        // Adjust stats based on difficulty (linear interpolation)
        currentMoveSpeed = Mathf.Lerp(baseMoveSpeed * 0.8f, baseMoveSpeed * 1.5f, difficulty);
        currentAttackRange = Mathf.Lerp(baseAttackRange * 0.9f, baseAttackRange * 1.3f, difficulty);
        currentAttackCooldown = Mathf.Lerp(baseAttackCooldown * 1.2f, baseAttackCooldown * 0.7f, difficulty);
        currentAttackDamage = Mathf.RoundToInt(Mathf.Lerp(baseAttackDamage, baseAttackDamage * 2f, difficulty));
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        MoveTowardsPlayer();
        CheckForAttackRange();

        if (isPlayerInRange && timeSinceLastAttack >= currentAttackCooldown)
        {
            Attack();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * currentMoveSpeed * Time.deltaTime;
        }
    }

    private void CheckForAttackRange()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            isPlayerInRange = distance <= currentAttackRange;
        }
    }

    private void Attack()
    {
        timeSinceLastAttack = 0f;

        if (isPlayerInRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(currentAttackDamage);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(currentAttackDamage);
            }
        }
    }

    private void OnDestroy()
    {
        ScoreManager.Instance?.IncreaseScore(scoreValue);
        difficultyManager?.RegisterPlayerKill();
    }
}
