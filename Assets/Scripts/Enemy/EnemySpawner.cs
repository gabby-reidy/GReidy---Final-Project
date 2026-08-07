using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public Transform PlayerTransform;

    [System.Serializable]
    public struct SpawnLocations
    {
        public Transform SpawnPoint;
        public Transform[] PatrolPoints;
    }
    [Header("Spawn Info")]
    [SerializeField] private float spawnRate = 6f;
    [SerializeField] private int maxEnemiesInScene = 5;
    [SerializeField] private int maxLevelSpawns = 10;
    private int totalSpawned = 0;

    [Header("Zones and Prefabs")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private SpawnLocations[] spawnZones;

    private List<GameObject> activeEnemies = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerTransform == null)
        {
            PlayerTransform = GameObject.FindWithTag("Player").transform;
        }
        StartCoroutine(SpawnEnemiesOnTimer());
    }

    private IEnumerator SpawnEnemiesOnTimer()
    {
        while (!GameManager.IsGameOver && !LevelManager.isLevelCleared && totalSpawned < maxLevelSpawns)
        {
            activeEnemies.RemoveAll(e => e == null);

            if (activeEnemies.Count < maxEnemiesInScene && spawnZones.Length > 0 && enemyPrefabs.Length > 0)
            {
                SpawnRandomEnemyAtRandomLocation();
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnRandomEnemyAtRandomLocation()
    {
        int spawnZoneIndex = Random.Range(0, spawnZones.Length);
        SpawnLocations randomZone = spawnZones[spawnZoneIndex];

        if (randomZone.SpawnPoint == null)
        {
            return;
        }

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject randomPrefab = enemyPrefabs[enemyIndex];

        GameObject newEnemy = Instantiate(randomPrefab, Vector3.zero, randomPrefab.transform.rotation);
        activeEnemies.Add(newEnemy);

        totalSpawned++;

        Enemy enemyScript = newEnemy.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            enemyScript.PlayerTransform = PlayerTransform;
            enemyScript.InitializePatrol(randomZone.PatrolPoints, randomZone.SpawnPoint.position);
        }
    }
}
