using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private Image[] lifeIcons;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetActiveLifeManager(this);
            UpdateLifeCount(GameManager.Instance.CurrentLives);
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ClearActiveLifeManager(this);
        }
    }

    /// <summary>
    /// Enables UI life icons based on current lives parameter
    /// </summary>
    /// <param name="currentLives"></param>
    public void UpdateLifeCount(int currentLives)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (lifeIcons[i] != null)
            {
                lifeIcons[i].gameObject.SetActive(i < currentLives);
            }
        }
    }
}
