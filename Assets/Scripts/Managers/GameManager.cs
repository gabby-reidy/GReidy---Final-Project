using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool GameOver;
    public static int MaxLives = 3;
    public int CurrentLives = 3;

    private PlayerLifeManager playerLifeManager;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOver = false;
        ResetLives();
    }

    /// <summary>
    /// Sets active scene's LifeManager to this GameManager
    /// </summary>
    /// <param name="manager"></param>
    public void SetActiveLifeManager(PlayerLifeManager manager)
    {
        playerLifeManager = manager;
    }
    /// <summary>
    /// Clears reference to LifeManager
    /// </summary>
    /// <param name="manager"></param>
    public void ClearActiveLifeManager(PlayerLifeManager manager)
    {
        if (playerLifeManager == manager)
        {
            playerLifeManager = null;
        }
    }

    public void LoseLife()
    {
        CurrentLives--;
        if (playerLifeManager != null)
        {
            playerLifeManager.UpdateLifeCount(CurrentLives);
        }

        if (CurrentLives <= 0)
        {
            CurrentLives = 0;
            //game over
        }
    }

    public void ResetLives()
    {
        CurrentLives = MaxLives;
    }

    public EnemyHealth GetEnemyHealth()
    {
        return enemyHealth;
    }
}
