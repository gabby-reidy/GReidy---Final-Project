using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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
    [SerializeField] private float spawnRate;
    [SerializeField] private int maxEnemiesInScene;
    [SerializeField] private int maxLevelSpawns;
    private int totalSpawned = 0;
    private float spawnDelay = 3f;

    [Header("Zones and Prefabs")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private SpawnLocations[] spawnZones;

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        if (PlayerTransform == null)
        {
            PlayerTransform = GameObject.FindWithTag("Player").transform;
        }
        StartCoroutine(SpawnEnemiesOnTimer());
    }

    /// <summary>
    /// Spawns enemies on a timer as long as the game is active, the level is not cleared, and the total spawn count has not been reached
    /// also checks our active enemies list to make sure it does not spawn more than the max enemies allowed in the scene at once
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnEnemiesOnTimer()
    {
        yield return new WaitForSeconds(spawnDelay);

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

    /// <summary>
    /// First chooses a random zone from the spawn zone array
    /// Then chooses a random enemy prefab from enemy prefab array
    /// Then spawns enemy and adds it to the active enemy list and increments the total spawn count
    /// finally, passes player transform info down to the enemy script
    /// </summary>
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
