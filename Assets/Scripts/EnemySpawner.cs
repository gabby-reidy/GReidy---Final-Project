using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnLocations
    {
        public string Name; // just for organizing in inspector? maybe get rid of since its not actually being called
        public Transform SpawnPoint;
        public Transform[] PatrolPoints;
    }
    [SerializeField] private float spawnTimer = 10f;
    [SerializeField] private int maxEnemies = 5;
    // List of enemy prefabs - need to add additional enemies to prefab folder 
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
    // List of spawn locations for randomization
    [SerializeField] private List<SpawnLocations> spawnZones = new List<SpawnLocations>();
    // List to check for active enemies in the scene - maybe a better way to do this? idk
    private List<GameObject> activeEnemies = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemiesOnTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnEnemiesOnTimer()
    {
        while (!GameManager.GameOver)
        {
            activeEnemies.RemoveAll(enemy => enemy == null);

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
