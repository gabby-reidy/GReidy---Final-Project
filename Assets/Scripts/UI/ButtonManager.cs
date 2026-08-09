using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    /// <summary>
    /// enables the use of the singleton game manager function so that it does not break the link between levels
    /// </summary>
   public void TriggerGoBackToMainMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GoBackToMainMenu();
        }
    }

    /// <summary>
    /// enables the use of the singleton game manager function so that it does not break the link between levels
    /// </summary>
    public void TriggerLoadNextScene()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadNextScene();
        }
    }
}
