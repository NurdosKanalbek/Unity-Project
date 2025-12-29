using UnityEngine;
using KnightAdventure.Utils; // Используем Utils.cs для рандома

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float minSpawnTime = 1f; 
    [SerializeField] private float maxSpawnTime = 5f; 
    [SerializeField] private float spawnDistance = 10f;

    private float _timer;
    private float _currentSpawnRate;

    private void Start()
    {
        _currentSpawnRate = maxSpawnTime;
    }

    private void Update()
    {
        if (Player.Instance == null || !Player.Instance.IsAlive()) return;

        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            SpawnEnemy();
            UpdateSpawnRate();
            _timer = _currentSpawnRate;
        }
    }

    private void SpawnEnemy()
    {
        if (Player.Instance != null)
        {
            Vector3 randomDir = Utils.GetRandomDir(); //
            Vector3 spawnPos = Player.Instance.transform.position + randomDir * spawnDistance;

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    private void UpdateSpawnRate()
    {
        if (DifficultyManager.Instance != null)
        {
            int level = DifficultyManager.Instance.GetDifficultyLevel();
            float newRate = maxSpawnTime - (level * 0.2f);
        
            _currentSpawnRate = Mathf.Max(minSpawnTime, newRate);
        }
    }
}