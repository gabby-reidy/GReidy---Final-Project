using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlideshowManager : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;
    [SerializeField] private Image slide;

    private int currentIndex = 0;

    public void GoToNextSlide()
    {
        currentIndex++;

        if (currentIndex < frames.Length)
        {
            ChangeSlideSprite();
        }
        else
        {
            SceneManager.LoadScene("LevelOne");
        }
    }

    private void ChangeSlideSprite()
    {
        slide.sprite = frames[currentIndex];
    }
}
