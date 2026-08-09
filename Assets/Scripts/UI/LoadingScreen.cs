using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetLoadingScreen(gameObject);
        }
        gameObject.SetActive(false);
    }
}
