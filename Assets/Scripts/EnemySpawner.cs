using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _spawnTarget;
    [SerializeField] private int _spawnCount;

    public void SpawnEnemies()
    {
        if (_spawnTarget is null) return;

        for (int i = 0; i < _spawnCount; i++)
        {
            Instantiate(_spawnTarget).transform.position = _spawnPoint.position;
        }
    }
}
