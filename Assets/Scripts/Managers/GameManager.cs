using UnityEngine;
using UnityEngine.UI;
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

    public void ResetLives()
    {
        CurrentLives = MaxLives;
    }

    public void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
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

    public void GoBackToMainMenu()
    {
        Time.timeScale = 1f;
        IsGameOver = false;
        ResetLives();
        StartCoroutine(LoadSceneAsynchronously(0));
    }
}
