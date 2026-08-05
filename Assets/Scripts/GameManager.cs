using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool GameOver;
    public static int MaxLives = 3;
    public int CurrentLives = 3;

    private LifeManager lifeManager;
    private Health health;

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
    public void SetActiveLifeManager(LifeManager manager)
    {
        lifeManager = manager;
    }
    /// <summary>
    /// Clears reference to LifeManager
    /// </summary>
    /// <param name="manager"></param>
    public void ClearActiveLifeManager(LifeManager manager)
    {
        if (lifeManager == manager)
        {
            lifeManager = null;
        }
    }

    public void LoseLife()
    {
        CurrentLives--;
        if (lifeManager != null)
        {
            lifeManager.UpdateLifeCount(CurrentLives);
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
}
