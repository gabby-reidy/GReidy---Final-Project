using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer AudioMixer;
    [SerializeField] private AudioSource audioSource;

    [Header("UI SFX")]
    [SerializeField] private AudioClip playButtonSFX;
    [SerializeField] private AudioClip settingsButtonSFX;

    [Header("SFX")]
    [SerializeField] private AudioClip playerProjectileSFX;
    [SerializeField] private AudioClip playerProjectileBurstSFX;
    [SerializeField] private AudioClip enemyProjectileSFX;
    [SerializeField] private AudioClip loseLifeSFX;
    [SerializeField] private AudioClip[] enemySFX;

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

    public void PlayerProjectileSFX()
    {
        audioSource.PlayOneShot(playerProjectileSFX);
    }

    public void PlayerProjectileBurstSFX()
    {
        AudioSource.PlayClipAtPoint(playerProjectileBurstSFX, Vector3.zero);
    }
}
