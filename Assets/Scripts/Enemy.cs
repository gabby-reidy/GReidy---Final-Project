using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectile;

    [SerializeField] private Transform[] patrolPoints;
    private int patrolPointIndex = 0;
    [SerializeField] private int spawnRate =3;

    private bool gameStart = true; // need to remove - this is to test coroutine

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        StartCoroutine(ShootAtPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f))
        {
            SetNextPoint();
        }

    }

    private void SetNextPoint()
    {
        agent.SetDestination(patrolPoints[patrolPointIndex].position);
        patrolPointIndex = (patrolPointIndex + 1) % patrolPoints.Length;
    }

    IEnumerator ShootAtPlayer()
    {
        while (gameStart)
        {
            Instantiate(projectile, firePoint.position, projectile.transform.rotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
