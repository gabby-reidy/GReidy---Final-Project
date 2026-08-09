using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetVictoryScreen(gameObject);
        }
        gameObject.SetActive(false);
    }
}
