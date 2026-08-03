using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Transform[] patrolPoints;
    private int patrolPointIndex = 0;


    void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }


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
        }
    }

    IEnumerator PatrolRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        SetNextPoint();

        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (agent.isOnNavMesh && !agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f)
                    {
                        SetNextPoint();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Moves enemy between waypoints in the array
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
