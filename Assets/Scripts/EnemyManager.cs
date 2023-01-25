using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Vector2 _spawnArea;
    [SerializeField] private float _spawnTimer;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private Transform _player;

    private float _timer;

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0f)
        {
            SpawnEnemy();
            _timer = _spawnTimer;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 position = new Vector3(
            UnityEngine.Random.Range(-_spawnArea.x, _spawnArea.y),
            UnityEngine.Random.Range(-_spawnArea.y, _spawnArea.x),
            0f
        );

        position += _player.transform.position;
        
        GameObject newEnemy = Instantiate(_enemy);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<Enemy>().Audio = _audio;
        newEnemy.GetComponent<Enemy>().TargetDestination = _player;
    }
}
