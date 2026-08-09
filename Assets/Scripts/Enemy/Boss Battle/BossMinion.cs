using UnityEngine;
using UnityEngine.AI;

public class BossMinion : Enemy
{
    [SerializeField] private GameObject minionProjectilePrefab;
    [SerializeField] private NavMeshAgent minionAgent;

    private void Awake()
    {
        if (minionAgent == null)
        {
            minionAgent = GetComponent<NavMeshAgent>();
        }
        BossEnemy.OnBossDeath += DestroyOnBossDeath;
    }

    private void Start()
    {
        StartCoroutine(ShootProjectilesAtPlayer());
    }

    protected override void SpawnProjectile()
    {
        base.SpawnProjectile();
    }

    private void DestroyOnBossDeath()
    {
        StopAllCoroutines();

        if (minionAgent.hasPath)
        {
            minionAgent.ResetPath();
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        BossEnemy.OnBossDeath -= DestroyOnBossDeath;
    }
}
