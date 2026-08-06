using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerupPrefabs;

    private float minX = -23f;
    private float maxX = 23f;
    private float minZ = -46f;
    private float maxZ = 46f;
    private float yPos = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SpawnRandomPowerupInRandomPlace()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 spawnPos = new Vector3(randomX, yPos, randomZ);
        int powerupIndex = Random.Range(0, powerupPrefabs.Length);
        GameObject randomPrefab = powerupPrefabs[powerupIndex];
        Instantiate(randomPrefab, spawnPos, randomPrefab.transform.rotation);
    }
}
