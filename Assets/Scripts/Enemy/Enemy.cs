using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Patrolling Info")]
    [SerializeField] private NavMeshAgent agent;
    private Transform[] patrolPoints;
    private int patrolPointIndex = 0;

    [Header("Attack Info")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;


    void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    /// <summary>
    /// Allows enemy spawner script to initialize the instantiated enemys patrol points and spawn position
    /// </summary>
    /// <param name="points"></param>
    /// <param name="spawnPosition"></param>
    public void InitializePatrol(Transform[] points, Vector3 spawnPosition)
    {
        this.patrolPoints = points;

        if (agent != null)
        {
            agent.Warp(spawnPosition); // using warp because setdestination was not working on instantiation
        }

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            patrolPointIndex = 0;
            StartCoroutine(PatrolRoutine());
        }
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

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
    /// Moves enemy between waypoints in the array - checks added during debugging, could not figure out what was causing errors
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
}
