using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int EnemyHP = 5;

    public void TakeDamage(int damage)
    {
        EnemyHP -= damage;
        //play hit animation
        //play SFX?
        if(EnemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
