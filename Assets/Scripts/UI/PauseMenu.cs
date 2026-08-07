using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // if i dont end up using this anywhere else then make it private
    [SerializeField] GameObject pauseMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame() // change to public for testing
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
