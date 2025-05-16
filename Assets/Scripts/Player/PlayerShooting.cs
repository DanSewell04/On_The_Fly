using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject projectilePrefab;
    public AudioSource shootSound;
    public float projectileForce = 700f;
    public float projectileDamage = 25f;

    private DifficultyManager difficultyManager;

    private void Start()
    {
        difficultyManager = FindAnyObjectByType<DifficultyManager>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        difficultyManager.RegisterShotFired();

        if (rb != null)
        {
            rb.AddForce(shootPoint.forward * projectileForce);
        }
        projectile.GetComponent<AudioSource>().Play();

        Bullet bullet = projectile.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.damage = projectileDamage;
        }
    }
}
