using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnDelay = 3f;
    [SerializeField] private int _maxEnemies = 10;

    [Header("Difficulty Settings")]
    [SerializeField] private float _minSpawnDelay = 1f;
    [SerializeField] private int _maxEnemyMultiplier = 3;

    private int _activeEnemies = 0;
    private DifficultyManager _difficultyManager;
    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        _difficultyManager = FindAnyObjectByType<DifficultyManager>();
        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned in SpawnManager!");
            return;
        }

        StartCoroutine(EnemySpawnCoroutine());
    }

    private IEnumerator EnemySpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(GetAdjustedSpawnDelay());

            if (_activeEnemies < GetAdjustedMaxEnemies() && _spawnPoints.Length > 0)
            {
                Transform spawnPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
                int enemyIndex = UnityEngine.Random.Range(0, _enemyPrefab.Length);
                GameObject enemy = Instantiate(_enemyPrefab[enemyIndex], spawnPoint.position, spawnPoint.rotation);
                _spawnedEnemies.Add(enemy);
                _activeEnemies++;

                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    enemyComponent.OnDeath += () =>
                    {
                        _activeEnemies--;
                        _spawnedEnemies.Remove(enemy);
                    };
                }
            }
        }
    }

    private float GetAdjustedSpawnDelay()
    {
        if (_difficultyManager == null) return _spawnDelay;
        return Mathf.Lerp(_spawnDelay, _minSpawnDelay, _difficultyManager.currentDifficulty);
    }

    private int GetAdjustedMaxEnemies()
    {
        if (_difficultyManager == null) return _maxEnemies;
        return Mathf.RoundToInt(_maxEnemies * (1 + (_maxEnemyMultiplier - 1) * _difficultyManager.currentDifficulty)); 
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    public void CleanupAllEnemies()
    {
        foreach (var enemy in _spawnedEnemies)
        {
            if (enemy != null) Destroy(enemy);
        }
        _spawnedEnemies.Clear();
        _activeEnemies = 0;
    }

}
