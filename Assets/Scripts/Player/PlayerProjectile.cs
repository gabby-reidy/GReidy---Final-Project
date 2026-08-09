using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private int projectileDamage = 1;
    [SerializeField] private float lifetime = 10f;
    private EnemyHealth enemyHealth;
    private BossEnemy bossEnemy;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            AudioManager.Instance.PlayerProjectileBurstSFX();
            enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(projectileDamage);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            AudioManager.Instance.PlayerProjectileBurstSFX();
        }

        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            AudioManager.Instance.PlayerProjectileBurstSFX();
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
            AudioManager.Instance.PlayerProjectileBurstSFX();
            bossEnemy = other.gameObject.GetComponent<BossEnemy>();
            bossEnemy.BossTakeDamage(projectileDamage);
        }
    }
}
