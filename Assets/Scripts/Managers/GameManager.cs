using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool IsGameOver;
    public static int MaxLives = 3;
    public int CurrentLives = 3;

    [SerializeField] private GameObject gameOverScreen;
    private PlayerLifeManager playerLifeManager;

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
        IsGameOver = false;
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
            GameOver();
        }
    }

    public void ResetLives()
    {
        CurrentLives = MaxLives;
    }

    public void GameOver()
    {
        IsGameOver = true;
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
