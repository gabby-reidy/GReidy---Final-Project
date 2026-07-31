using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool GameOver;
    public static int MaxLives = 3;
    public int CurrentLives;

    private Health health;

    [SerializeField] private Image[] lifeIcons;

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
        CurrentLives = MaxLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLifeCount(int currentLives)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = (i < currentLives);
        }
    }
}
