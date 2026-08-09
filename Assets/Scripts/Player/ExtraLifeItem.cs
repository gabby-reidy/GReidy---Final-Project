using UnityEngine;

public class ExtraLifeItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.CurrentLives < GameManager.MaxLives)
                {
                    GameManager.Instance.GainLife();
                    //sfx?
                    Destroy(gameObject);
                }
            }
        }
    }
}
