using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private static bool isGamePaused = false;
    [SerializeField] GameObject pauseMenu;

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isGamePaused)
            {
                ResumeGame();
            } 
            else
            {
                PauseGame();
            }
        }
    }

    /// <summary>
    /// Sets pause menu UI to active, sets timescale to 0 so that nothing is happening while the game is paused
    /// </summary>
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    /// <summary>
    /// Sets pause menu to inactive, re-enables timescale to resume gameplay
    /// </summary>
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
}
