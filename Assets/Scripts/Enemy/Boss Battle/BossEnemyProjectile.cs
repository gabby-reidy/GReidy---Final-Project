using UnityEngine;

public class BossEnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 5f;
    private Rigidbody rb;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        Destroy(gameObject, lifetime);
    }

    /// <summary>
    /// Allows boss script to set projectile position
    /// </summary>
    /// <param name="targetPosition"></param>
    public void SetProjectilePosition(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerLoseLife();
            }
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
