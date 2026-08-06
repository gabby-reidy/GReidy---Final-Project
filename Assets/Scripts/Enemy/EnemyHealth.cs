using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int enemyHP = 5;
    public int EnemyHP => enemyHP;

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
            Destroy(gameObject);
            //play sfx
            //play anim?
        }
    }
}
