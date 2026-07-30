using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform PlayerRef;

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

    /// <summary>
    /// Spawns projectiles from enemy location and sets player transform in the projectile script
    /// </summary>
    private void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, firePoint.position, projectile.transform.rotation);
        Projectile projectileScript = newProjectile.GetComponent<Projectile>();
        projectileScript.Player = PlayerRef;
    }

    IEnumerator ShootProjectilesAtPlayer()
    {
        while (!GameManager.GameOver)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnPowerup()
    {

    }
}
