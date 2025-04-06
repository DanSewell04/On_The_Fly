using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [Header("Base Settings")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float baseShootForce = 10f;
    public float baseShootCooldown = 2f;
    public float baseShootingRange = 15f;
    public float baseAccuracy = 0.7f; // 70% base accuracy
    public int scoreValue = 150;

    [Header("Difficulty Settings")]
    public float maxAccuracyBonus = 0.3f; // Can go up to 100% accuracy at max difficulty
    public float maxRangeBonus = 10f;
    public float maxForceBonus = 5f;

    private Transform player;
    private float nextShootTime;
    private DifficultyManager difficultyManager;

    // Current stats after difficulty adjustment
    private float currentShootForce;
    private float currentShootCooldown;
    private float currentShootingRange;
    private float currentAccuracy;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        difficultyManager = FindAnyObjectByType<DifficultyManager>();
        ApplyDifficultySettings();
    }

    private void ApplyDifficultySettings()
    {
        float difficulty = difficultyManager.currentDifficulty;

        currentShootForce = baseShootForce + (maxForceBonus * difficulty);
        currentShootCooldown = baseShootCooldown * (1 - (0.5f * difficulty)); // Faster shooting at higher difficulty
        currentShootingRange = baseShootingRange + (maxRangeBonus * difficulty);
        currentAccuracy = Mathf.Clamp01(baseAccuracy + (maxAccuracyBonus * difficulty));
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > currentShootingRange) return;

        AimAtPlayer();

        if (Time.time >= nextShootTime)
        {
            if (Random.value <= currentAccuracy) // Only shoot if random value is within accuracy
            {
                Shoot();
            }
            nextShootTime = Time.time + currentShootCooldown;
        }
    }

    private void AimAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            // Add slight inaccuracy based on difficulty
            Vector3 shotDirection = shootPoint.forward;
            float inaccuracy = 0.1f * (1 - difficultyManager.currentDifficulty);
            shotDirection += Random.insideUnitSphere * inaccuracy;
            shotDirection.Normalize();

            bulletRb.AddForce(shotDirection * currentShootForce, ForceMode.Impulse);
        }

        Destroy(bullet, 3f);
    }

    private void OnDestroy()
    {
        ScoreManager.Instance?.IncreaseScore(scoreValue);
        difficultyManager?.RegisterPlayerKill();
    }
}
