using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Properly defined event
    public event Action OnDeath;

    [Header("Enemy Stats")]
    public float health = 100f;
    public float speed = 3f;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
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
        // Invoke the event before destroying
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
