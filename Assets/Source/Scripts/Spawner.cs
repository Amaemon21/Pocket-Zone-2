using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    [SerializeField] private PlayerController _target;
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private int _spawned;

    private int _currentSpawnPoint = 0;

    private void Awake()
    {
        SetWave(_currentWaveNumber);
        StartCoroutine(SpawnEnemy());
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Enemy,
            _spawnPoints[_currentSpawnPoint].position,
            _spawnPoints[_currentSpawnPoint].rotation);

        enemy.Init(_target);
        _spawned++;
        NextPoint();
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        _spawned = 0;
    }

    private IEnumerator SpawnEnemy()
    {
        while (_currentWave.MaxCount > _spawned)
        {
            InstantiateEnemy();
            
            yield return new WaitForSeconds(_currentWave.Delay);
        }
    }

    private void NextPoint()
    {
        if (_spawnPoints.Count == 0)
        {
            Debug.LogError("Список точек пуст!");
            return;
        }

        if (_spawnPoints.Count - 1 > _currentSpawnPoint)
            _currentSpawnPoint++;
        else
            _currentSpawnPoint = 0;
    }

    private void NextWave()
    {
        if (_waves.Count -1 > _currentWaveNumber)
        {
            SetWave(_currentWaveNumber++);
            StartCoroutine(SpawnEnemy());
        }
        else
        {
            StopCoroutine(SpawnEnemy());
        }
    }
}

[Serializable]
class Wave
{
    [field: SerializeField] public Enemy Enemy { get; private set; }
    [field: SerializeField] public float MaxCount { get; private set; }
    [field: SerializeField] public float Delay { get; private set; }
}