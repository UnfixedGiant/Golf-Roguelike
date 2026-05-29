using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [Header("Spawn Time")]
    [SerializeField] private float minimumSpawnTime;
    [SerializeField] private float maximumSpawnTime;
    [Header("Spawn Distance")]
    [SerializeField] private float minimumSpawnDistance = 5f;
    [SerializeField] private float maximumSpawnDistance = 15f;
    [Header("Wall Check")]
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private float enemyRadius = 0.5f;

    private float timeUntilSpawn;

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0)
        {
            TrySpawnEnemy();
            SetTimeUntilSpawn();
        }
    }

    private void TrySpawnEnemy()
    {
        Vector3 spawnPosition = GetSpawnPosition();

        bool isInsideWall = Physics2D.OverlapCircle(spawnPosition, enemyRadius, wallLayerMask);

        if (!isInsideWall)
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            return;
        }

    }

    private Vector3 GetSpawnPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minimumSpawnDistance, maximumSpawnDistance);
        Vector2 spawnOffset = randomDirection * randomDistance;

        return transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0f);
    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}