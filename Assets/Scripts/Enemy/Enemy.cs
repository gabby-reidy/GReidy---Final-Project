using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform PlayerTransform;

    [Header("Patrolling Info")]
    [SerializeField] private NavMeshAgent agent;
    private Transform[] patrolPoints;
    private int patrolPointIndex = 0;

    [Header("Attack Info")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpawnRate = 3f;

    private float delay = 0.5f;
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
    }

    /// <summary>
    /// Allows enemy spawner script to initialize the instantiated enemy's patrol points and spawn position using nav agent
    /// </summary>
    /// <param name="points"></param>
    /// <param name="spawnPosition"></param>
    public void InitializePatrol(Transform[] points, Vector3 spawnPosition)
    {
        this.patrolPoints = points;

        if (agent != null)
        {
            agent.Warp(spawnPosition);
        }

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            patrolPointIndex = 0;
            StartCoroutine(PatrolRoutine());
            StartCoroutine(ShootProjectilesAtPlayer());
        }
    }

    /// <summary>
    /// While game is not over and after a delay, sets enemy to the next patrol waypoint
    /// This will keep the enemy constantly moving while the game is active
    /// </summary>
    /// <returns></returns>
    private IEnumerator PatrolRoutine()
    {
        while (!GameManager.IsGameOver)
        {
            yield return new WaitForSeconds(delay);

            if (agent.isOnNavMesh && !agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f))
                {
                    SetNextPoint();
                }
            }
        }
    }

    /// <summary>
    /// Moves enemy back and forth between patrol waypoints in the array
    /// </summary>
    private void SetNextPoint()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            return;
        }
        if (!agent.isOnNavMesh)
        {
            return;
        }

        agent.SetDestination(patrolPoints[patrolPointIndex].position);
        patrolPointIndex = (patrolPointIndex + 1) % patrolPoints.Length;
    }

    /// <summary>
    /// While game is not over, shoot projectiles at player on a timer
    /// Also sets enemy attack animation & SFX
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator ShootProjectilesAtPlayer()
    {
        while (!GameManager.IsGameOver)
        {
            yield return new WaitForSeconds(delay);

            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
            SpawnProjectile();

            if (AudioManager.Instance != null) // need to add these everywhere
            {
                AudioManager.Instance.EnemyAttackSFX();
            }
            yield return new WaitForSeconds(projectileSpawnRate);
        }
    }
    /// <summary>
    /// spawns a projectile at enemy fire point and passes player transform info down to it
    /// </summary>
    protected virtual void SpawnProjectile()
    {
        if (projectilePrefab != null && firePoint != null) // make sure null checks are everywhere too
        {
            GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, projectilePrefab.transform.rotation);
            EnemyProjectile projectileScript = newProjectile.GetComponent<EnemyProjectile>();
            if (projectileScript != null)
            {
                projectileScript.Player = PlayerTransform;
            }
        }
    }
}
