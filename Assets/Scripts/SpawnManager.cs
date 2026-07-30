using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform PlayerRef;
    public bool isGameOver = false; // move this to gamemanager soon

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int spawnRate = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerRef == null)
        {
            PlayerRef = GameObject.FindWithTag("Player").transform;
        }

        StartCoroutine(ShootProjectilesAtPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, firePoint.position, projectile.transform.rotation);
        Projectile projectileScript = newProjectile.GetComponent<Projectile>();
        projectileScript.Player = PlayerRef;
    }

    IEnumerator ShootProjectilesAtPlayer()
    {
        while (!isGameOver)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
