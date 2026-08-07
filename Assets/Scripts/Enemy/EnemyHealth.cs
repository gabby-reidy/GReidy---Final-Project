using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    private int enemyHP = 5;
    public int EnemyHP => enemyHP;

    public static event Action<GameObject> OnDeath;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        enemyHP = Mathf.Max(0, enemyHP - damage);
        //play hit animation
        //play SFX?
        if(enemyHP <= 0)
        {
            Die();
            //play sfx
            //play anim?
        }
    }

    private void Die()
    {
        OnDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
