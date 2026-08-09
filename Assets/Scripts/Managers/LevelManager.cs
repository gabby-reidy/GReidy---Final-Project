using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static event Action OnEnemyKilled;
    public UnityEvent OnLevelCleared;

    [SerializeField] private int targetKills = 10;
    private int currentKills = 0;
    public static bool isLevelCleared { get; private set; } = false;

    private void OnEnable()
    {
        OnEnemyKilled += IncreaseKillCount;
    }

    private void Awake()
    {
        isLevelCleared = false;
    }

    /// <summary>
    /// Tracks players current kills and checks if they have reached the target before invoking the onlevelcleared unity event
    /// </summary>
    private void IncreaseKillCount()
    {
        currentKills++;

        if (currentKills >= targetKills)
        {
            isLevelCleared = true;
            OnLevelCleared.Invoke();
        }
    }

    /// <summary>
    /// Allows enemy health script to report that it has died, and invoke the action event
    /// </summary>
    public static void AddKill()
    {
        OnEnemyKilled?.Invoke();
    }

    private void OnDisable()
    {
        OnEnemyKilled -= IncreaseKillCount;
    }
}
