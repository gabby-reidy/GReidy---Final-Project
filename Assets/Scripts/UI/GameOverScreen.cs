using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetGameOverScreen(gameObject);
        }
        gameObject.SetActive(false);
    }
}
