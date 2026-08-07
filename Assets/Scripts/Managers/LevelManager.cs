using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static event Action OnEnemyKilled;
    public UnityEvent OnLevelCleared;

    [SerializeField] private int targetKills = 10;
    private int currentKills = 0;
    private bool isLevelCleared = false;

    private void OnEnable()
    {
        //subscribe
    }

    private void HandeEnemyDeath()
    {
        currentKills++;
        Debug.Log("current kills " + currentKills);

        if (currentKills >= targetKills)
        {
            isLevelCleared = true;
            OnLevelCleared.Invoke();
        }
    }

    private void OnDisable()
    {
        //unsubscribe
    }
}
