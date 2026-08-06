using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    [SerializeField] private int projectileDamage = 1;

    private void Start()
    {
        
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
    }
}
