using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnLocations
    {
        public Transform SpawnPoint;
        public Transform[] PatrolPoints;
    }
    [SerializeField] private float spawnTimer = 6f;
    [SerializeField] private int maxEnemies = 5;
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();  // could these be arrays? since im not resizing these?
    [SerializeField] private List<SpawnLocations> spawnZones = new List<SpawnLocations>(); // or should everything be a list to make it easier idk - look it up
    private List<GameObject> activeEnemies = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemiesOnTimer());
    }

    private IEnumerator SpawnEnemiesOnTimer()
    {
        while (!GameManager.GameOver)
        {
            if (activeEnemies.Count < maxEnemies && spawnZones.Count > 0 && enemyPrefabs.Count > 0)
            {
                SpawnRandomEnemy();
            }
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void SpawnRandomEnemy()
    {
        int spawnZoneIndex = Random.Range(0, spawnZones.Count);
        SpawnLocations randomZone = spawnZones[spawnZoneIndex];

        if (randomZone.SpawnPoint == null)
        {
            return;
        }

        int enemyIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject randomPrefab = enemyPrefabs[enemyIndex];

        GameObject newEnemy = Instantiate(randomPrefab, Vector3.zero, randomPrefab.transform.rotation);
        activeEnemies.Add(newEnemy);
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            enemyScript.InitializePatrol(randomZone.PatrolPoints, randomZone.SpawnPoint.position);
        }
    }
}
