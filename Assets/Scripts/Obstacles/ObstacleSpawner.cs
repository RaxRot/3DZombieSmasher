using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] private GameObject[] obstacles;
    private int _obstacleIndex;
    [SerializeField] private GameObject[] enemies;
    private int _enemyIndex;
    

    [Header("Position")]
    [SerializeField] private Transform[] pointsToSpawn;
    private int _pointSpawnIndex;
    [SerializeField] private float zOffsetToSpawn = 100f;
    private Vector3 _posToSpawn;
    private GameObject _playerTarget;
    
    [Header("Time Spawn Settings")]
    [SerializeField] private float minTimeToSpawn = 3f;
    [SerializeField] private float maxTimeToSpawn = 6f;
    private float _timeToSpawn;

    private bool _startSpawn;

    
    private void Start()
    {
        _playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying() && !_startSpawn)
        {
            _startSpawn = true;
            StartSpawn();
        }
    }

    private void StartSpawn()
    {
        StartCoroutine(nameof(_StartSpawnCo));
    }

    private IEnumerator _StartSpawnCo()
    {
        _pointSpawnIndex = Random.Range(0, pointsToSpawn.Length);
        _posToSpawn = new Vector3(pointsToSpawn[_pointSpawnIndex].transform.position.x, transform.position.y,
            _playerTarget.transform.position.z + zOffsetToSpawn);

        if (Random.Range(0,100)>50)
        {
            
            _enemyIndex = Random.Range(0, enemies.Length);
            Instantiate(enemies[_enemyIndex], _posToSpawn, Quaternion.identity);
        }
        else
        {
            
            _obstacleIndex = Random.Range(0, obstacles.Length);
            Instantiate(obstacles[_obstacleIndex], _posToSpawn, Quaternion.identity);
        }

        _timeToSpawn = Random.Range(minTimeToSpawn, maxTimeToSpawn);

        yield return new WaitForSeconds(_timeToSpawn);

        StartCoroutine(nameof(_StartSpawnCo));
    }

}
