using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer AudioMixer;
    [SerializeField] private AudioSource audioSource;

    [Header("SFX")]
    [SerializeField] private AudioClip playButtonSFX;
    [SerializeField] private AudioClip settingsButtonSFX;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void SetVolume(float volume)
    {
        AudioMixer.SetFloat("masterVolume", volume);
    }
    public void PlayButtonSFX()
    {
        audioSource.PlayOneShot(playButtonSFX);
    }

    public void SettingsButtonSFX()
    {
        audioSource.PlayOneShot(settingsButtonSFX);
    }
}
