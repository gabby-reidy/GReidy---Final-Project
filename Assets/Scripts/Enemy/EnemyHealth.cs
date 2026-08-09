using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int enemyHP = 5;
    public int EnemyHP => enemyHP;

    [SerializeField] private Animator animator;
    private float deathDelay = .4f;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    /// <summary>
    /// Allows other scripts to deal damage to enemy and plays enemy hit animation
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        enemyHP = Mathf.Max(0, enemyHP - damage);
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }
        if(enemyHP <= 0)
        {
            AudioManager.Instance.EnemyDeathSFX();
            Die();
        }
    }

    /// <summary>
    /// Adds a kill to the level kill count
    /// destroys the enemy after a slight delay to allow SFX and animations to play
    /// </summary>
    private void Die()
    {
        LevelManager.AddKill();
        Destroy(gameObject, deathDelay);
    }
}
