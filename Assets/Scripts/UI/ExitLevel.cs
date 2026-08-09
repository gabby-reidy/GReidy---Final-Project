using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Public method connected to Unity event OnLevelCleared
    /// Enables the exit door in hierarchy
    /// </summary>
    public void OpenExit()
    {
        gameObject.SetActive(true);
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.LevelExitOpenSFX();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayerExitLevelSFX();
            }
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoadNextScene();
            }
        }
    }
}
