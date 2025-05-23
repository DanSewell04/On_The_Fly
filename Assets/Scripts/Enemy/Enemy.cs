using System;
using UnityEngine;

public class Enemy : MonoBehaviour , iFreezable
{
    // Properly defined event
    public event Action OnDeath;

    [Header("Enemy Stats")]
    public float health = 100f;
    public float speed = 3f;
    private Transform player;

    private bool isFrozen = false;
    private Rigidbody rb;

    public GameObject particleEffectPrefab;
    public float destroyDelay = 2f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isFrozen) return;

        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            Vector3 look = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(look);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (particleEffectPrefab != null)
        {
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject, destroyDelay);
        }
        // Invoke the event before destroying
        OnDeath?.Invoke();
        Destroy(gameObject);
    }


    public void Freeze()
    {
        isFrozen = true;
        if (rb != null) rb.isKinematic = true;
    }

    public void UnFreeze()
    {
        isFrozen = false;
        if (rb != null) rb.isKinematic = false;
    }
}
