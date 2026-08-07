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

    private void IncreaseKillCount()
    {
        currentKills++;
        Debug.Log("current kills " + currentKills);

        if (currentKills >= targetKills)
        {
            isLevelCleared = true;
            OnLevelCleared.Invoke();
        }
    }

    public static void AddKill()
    {
        OnEnemyKilled?.Invoke();
    }

    private void OnDisable()
    {
        OnEnemyKilled -= IncreaseKillCount;
    }
}
