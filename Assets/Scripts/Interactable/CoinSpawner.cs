using System.Runtime.CompilerServices;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Coin Settings")]
    public GameObject coinPrefab;

    [Header("Spawn Area")]
    public Vector3 spawnCenter = Vector3.zero;
    public Vector3 spawnSize = new Vector3(10, 0, 10);

    [Header("Spawn Timing")]
    public float spawnInterval = 2f;
    private float timer;

    private void Start()
    {
        timer = spawnInterval;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnCoin();
            timer = spawnInterval;
        }
    }

    void SpawnCoin() 
    {
        Vector3 randomPosition = spawnCenter + new Vector3(Random.Range(-spawnSize.x / 2, spawnSize.x / 2), Random.Range(-spawnSize.y / 2, spawnSize.y / 2), Random.Range(-spawnSize.z / 2, spawnSize.z / 2));
        Instantiate(coinPrefab, randomPosition, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnCenter, spawnSize);
    }
}
