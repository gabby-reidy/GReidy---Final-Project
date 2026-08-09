using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OpenExit()
    {
        gameObject.SetActive(true);
        //sfx
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayerExitLevelSFX();
            }
            GameManager.Instance.LoadNextScene();
        }
    }
}
