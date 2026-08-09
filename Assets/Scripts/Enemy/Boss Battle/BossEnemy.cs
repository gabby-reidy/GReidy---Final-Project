using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour
{
    public static event Action OnBossDeath;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform playerTransform;

    [Header("Health Info")]
    [SerializeField] private float maxHealth = 50f;
    private float currentHealth;
    private bool isDead = false;

    [Header("Movement Info")]
    [SerializeField] private float idealDistance = 5f;
    [SerializeField] private float movementOffset = 1f;

    [Header("Attack Info")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpawnRate = 2f;

    [Header("Enemy Minion Info")]
    [SerializeField] private GameObject minionPrefab;
    [SerializeField] private Transform[] enemyMinionSpawnPoints;
    [SerializeField] private float enemyMinionSpawnRate = 10f;

    [SerializeField] private Animator animator;

    void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        currentHealth = maxHealth;
    }

    private void Start()
    {
        StartCoroutine(ShootProjectilesAtPlayer());
        StartCoroutine(SpawnMinionsOnTimer());
    }

    void Update()
    {
        if (playerTransform == null || isDead)
        {
            return;
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        Vector3 awayFromPlayer = (transform.position - playerTransform.position).normalized;

        if (distance > idealDistance + movementOffset)
        {
            Vector3 targetPos = playerTransform.position + (awayFromPlayer * idealDistance);
            agent.SetDestination(targetPos);
        }
        else if (distance < idealDistance - movementOffset)
        {
            Vector3 targetPos = playerTransform.position + (awayFromPlayer * idealDistance);
            agent.SetDestination(targetPos);
        }
        else
        {
            agent.ResetPath();
        }
    }

    private IEnumerator ShootProjectilesAtPlayer()
    {
        while (!GameManager.IsGameOver && !isDead)
        {
            yield return new WaitForSeconds(projectileSpawnRate);

            if (projectilePrefab != null && firePoint != null && playerTransform != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, projectilePrefab.transform.rotation);
                BossEnemyProjectile projectileScript = projectile.GetComponent<BossEnemyProjectile>();
                if (projectileScript != null)
                {
                    projectileScript.SetProjectilePosition(playerTransform.position);
                }

                if (animator != null)
                {
                    animator.SetTrigger("Attack");
                }
            }
         
        }
    }

    private IEnumerator SpawnMinionsOnTimer()
    {
        while (!GameManager.IsGameOver && !isDead)
        {
            yield return new WaitForSeconds(enemyMinionSpawnRate);
            
            if (minionPrefab != null && enemyMinionSpawnPoints.Length > 0)
            {
                foreach (Transform spawnPoint in enemyMinionSpawnPoints)
                {
                    if (spawnPoint != null)
                    {
                        GameObject minion = Instantiate(minionPrefab, spawnPoint.position, minionPrefab.transform.rotation);
                        BossMinion minionScript = minion.GetComponent<BossMinion>();
                        if (minionScript != null && playerTransform != null)
                        {
                            minionScript.PlayerTransform = playerTransform;
                        }
                    }
                }
            }
        }
    }

    public void BossTakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damage;
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }
        if (currentHealth <= 0)
        {
            DieAndEndGame();
        }
    }

    private void DieAndEndGame()
    {
        isDead = true;
        if (agent.hasPath)
        {
            agent.ResetPath();
        }
        agent.enabled = false;

        StopAllCoroutines();
        //play death sfx

        OnBossDeath?.Invoke();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.Victory();
        }
        Destroy(gameObject, 1f);
    }
}
