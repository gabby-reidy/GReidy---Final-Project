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
    [SerializeField] private GameObject victoryScreen;

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

    void Start()
    {
        IsGameOver = false;
        ResetLives();
    }

    public void SetGameOverScreen(GameObject screen)
    {
        gameOverScreen = screen;
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
        Time.timeScale = 0f;
        if (gameOverScreen == null)
        {
            gameOverScreen = GameObject.FindWithTag("GameOverUI");
            gameOverScreen.SetActive(true);
        }
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void Victory()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void GoBackToMainMenu()
    {
        Time.timeScale = 1f;
        IsGameOver = false;
        ResetLives();
        SceneManager.LoadScene("MainMenu");
    }
}
