using UnityEngine;

public class BossMinionHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    [SerializeField] private Animator animator; // implement later

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void MinionTakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
