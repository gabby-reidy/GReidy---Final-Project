using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool IsGameOver;
    public static int MaxLives = 3;
    public int CurrentLives = 3;

    private GameObject gameOverScreen;
    private GameObject victoryScreen;
    private GameObject loadingScreen;

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

    public void SetLoadingScreen(GameObject screen)
    {
        loadingScreen = screen;
    }

    public void SetGameOverScreen(GameObject screen)
    {
        gameOverScreen = screen;
    }

    public void SetVictoryScreen(GameObject screen)
    {
        victoryScreen = screen;
    }

    /// <summary>
    /// Sets timescale to prevent any issues with pausing, then loads the next scene in the build order with the coroutine
    /// </summary>
    public void LoadNextScene()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        StartCoroutine(LoadSceneAsynchronously(nextSceneIndex));
    }

    /// <summary>
    /// Loads scene asynchronously and enables loading screen
    /// </summary>
    /// <param name="sceneIndex">allows any method that calls this to load scene based on build index</param>
    /// <returns></returns>
    private IEnumerator LoadSceneAsynchronously(int sceneIndex)
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Sets active scene's UI LifeManager to this GameManager
    /// </summary>
    /// <param name="manager"></param>
    public void SetActiveLifeManager(PlayerLifeManager manager)
    {
        playerLifeManager = manager;
    }
    /// <summary>
    /// Clears reference to UI LifeManager
    /// </summary>
    /// <param name="manager"></param>
    public void ClearActiveLifeManager(PlayerLifeManager manager)
    {
        if (playerLifeManager == manager)
        {
            playerLifeManager = null;
        }
    }

    /// <summary>
    /// Decrements players current life count and then updates the UI
    /// Calls game over method if life count is 0
    /// </summary>
    public void PlayerLoseLife()
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

    /// <summary>
    /// Adds a life to player UI and current life total if they have less than max lives
    /// </summary>
    public void GainLife()
    {
        if (IsGameOver)
        {
            return;
        }

        if (CurrentLives < MaxLives)
        {
            CurrentLives++;
            if (playerLifeManager != null)
            {
                playerLifeManager.UpdateLifeCount(CurrentLives);
            }
        }
    }

    /// <summary>
    /// Resets players current lives to max lives
    /// </summary>
    public void ResetLives()
    {
        CurrentLives = MaxLives;
    }

    /// <summary>
    /// Ends the game and disables time scale
    /// Enables game over/lose screen
    /// </summary>
    public void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        StopAllCoroutines();
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    /// <summary>
    /// Ends the game and disables time scale
    /// Enables victory screen
    /// </summary>
    public void Victory()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        StopAllCoroutines();
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
        }
    }

    /// <summary>
    /// Sets timescale to 1 to prevent issues with pausing
    /// Resets gameover bool to false and player lives
    /// Loads main menu screen
    /// </summary>
    public void GoBackToMainMenu()
    {
        Time.timeScale = 1f;
        IsGameOver = false;
        ResetLives();
        StartCoroutine(LoadSceneAsynchronously(0));
    }
}
