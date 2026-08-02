using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnLocations
    {
        public string Name;
        public Transform SpawnPoint;
        public Transform[] PatrolPoints;
    }
    [SerializeField] private float spawnTimer = 10f;
    [SerializeField] private int maxEnemies = 5;
    // List of enemy prefabs - need to add additional enemies to prefab folder 
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
    // List of spawn locations for randomization
    [SerializeField] private List<SpawnLocations> spawnZones = new List<SpawnLocations>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnEnemiesOnTimer()
    {
        yield return new WaitForSeconds(spawnTimer);
    }

    private void SpawnRandomEnemy()
    {
        int spawnZoneIndex = Random.Range(0, spawnZones.Count);
        SpawnLocations randomZone = spawnZones[spawnZoneIndex];

        int enemyIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject randomPrefab = enemyPrefabs[enemyIndex];

        GameObject newEnemy = Instantiate(randomPrefab, randomZone.SpawnPoint.position, randomPrefab.transform.rotation);
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.InitializePatrol(randomZone.PatrolPoints);
        }
    }
}
