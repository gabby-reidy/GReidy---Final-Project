using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlideshowManager : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;
    [SerializeField] private Image slide;

    private int currentIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))  // TODO: Add buttons to slides and do on click method instead
        {
            GoToNextSlide();
        }
    }

    private void GoToNextSlide()
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
