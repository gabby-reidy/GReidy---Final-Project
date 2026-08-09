using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer AudioMixer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float clipVolume = .4f;

    [Header("UI SFX")]
    [SerializeField] private AudioClip playButtonSFX;
    [SerializeField] private AudioClip settingsButtonSFX;
    [SerializeField] private AudioClip playerGainLifeSFX;

    [Header("Player SFX")]
    [SerializeField] private AudioClip playerProjectileSFX;
    [SerializeField] private AudioClip playerProjectileBurstSFX;
    [SerializeField] private AudioClip loseLifeSFX;
    [SerializeField] private AudioClip levelExitOpenSFX;
    [SerializeField] private AudioClip playerExitLevelSFX;

    [Header("Enemy SFX")]
    [SerializeField] private AudioClip[] enemyAttackSFX;
    [SerializeField] private AudioClip[] enemyDeathSFX;
    [SerializeField] private AudioClip enemyProjectileSFX;

    [Header("Boss SFX")]
    [SerializeField] private AudioClip[] bossAttackSFX;

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
        audioSource.PlayOneShot(playButtonSFX, clipVolume);
    }

    public void SettingsButtonSFX()
    {
        audioSource.PlayOneShot(settingsButtonSFX, clipVolume);
    }

    public void PlayerProjectileSFX()
    {
        audioSource.PlayOneShot(playerProjectileSFX);
    }

    public void PlayerProjectileBurstSFX()
    {
        AudioSource.PlayClipAtPoint(playerProjectileBurstSFX, Vector3.zero);
    }

    public void EnemyAttackSFX()
    {
        int enemyAttackSFXIndex = Random.Range(0, enemyAttackSFX.Length);
        audioSource.PlayOneShot(enemyAttackSFX[enemyAttackSFXIndex], clipVolume);
    }

    public void EnemyDeathSFX()
    {
        int enemyDeathSFXIndex = Random.Range(0, enemyDeathSFX.Length);
        audioSource.PlayOneShot(enemyDeathSFX[enemyDeathSFXIndex], clipVolume);
    }

    public void BossAttackSFX()
    {
        int bossAttackSFXIndex = Random.Range(0, bossAttackSFX.Length);
        audioSource.PlayOneShot(bossAttackSFX[bossAttackSFXIndex], clipVolume);
    }

    public void LevelExitOpenSFX()
    {
        audioSource.PlayOneShot(levelExitOpenSFX);
    }

    public void PlayerExitLevelSFX()
    {
        audioSource.PlayOneShot(playerExitLevelSFX);
    }
}
