using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Transform[] patrolPoints;
    private int patrolPointIndex = 0;
    private bool isInitialized;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInitialized)
        {
            return;
        }

        Patrol();
    }

    public void InitializePatrol(Transform[] points)
    {
        this.patrolPoints = points;
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            patrolPointIndex = 0;
            isInitialized = true;
            SetNextPoint();
        }
    }

    private void Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f))
        {
            SetNextPoint();
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

        agent.SetDestination(patrolPoints[patrolPointIndex].position);
        patrolPointIndex = (patrolPointIndex + 1) % patrolPoints.Length;
    }
}
