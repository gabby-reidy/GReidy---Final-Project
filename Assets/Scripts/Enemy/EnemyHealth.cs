using UnityEngine;
using System;

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
    /// 
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        enemyHP = Mathf.Max(0, enemyHP - damage);
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }
        //play SFX?
        if(enemyHP <= 0)
        {
            AudioManager.Instance.EnemyDeathSFX();
            Die();
        }
    }

    private void Die()
    {
        LevelManager.AddKill();
        Destroy(gameObject, deathDelay);
    }
}
