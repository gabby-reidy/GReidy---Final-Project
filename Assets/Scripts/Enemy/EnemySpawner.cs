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
    [SerializeField] private int maxEnemies = 5;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private SpawnLocations[] spawnZones;
    private List<GameObject> activeEnemies = new List<GameObject>();

    [SerializeField] private int targetKills = 10;
    private int currentKills = 0;
    private bool levelCleared = false;

    public UnityEvent OnEnemiesCleared; // need to implement 

    private void OnEnable()
    {
        EnemyHealth.OnDeath += HandleEnemyDeath;
    }
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
        while (!GameManager.IsGameOver)
        {
            if (activeEnemies.Count < maxEnemies && spawnZones.Length > 0 && enemyPrefabs.Length > 0)
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
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            enemyScript.PlayerTransform = PlayerTransform;
            enemyScript.InitializePatrol(randomZone.PatrolPoints, randomZone.SpawnPoint.position);
        }
    }

    private void HandleEnemyDeath(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }

        currentKills++;

        if (currentKills >= targetKills)
        {
            levelCleared = true;
            OnEnemiesCleared.Invoke();
        }
    }

    private void OnDisable()
    {
        EnemyHealth.OnDeath -= HandleEnemyDeath;
    }
}
