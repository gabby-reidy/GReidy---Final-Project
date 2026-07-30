using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool GameOver;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
