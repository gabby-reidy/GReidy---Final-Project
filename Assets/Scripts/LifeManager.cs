using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private List<Image> lifeIcons = new List<Image>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.SetActiveLifeManager(this);
        UpdateLifeCount(GameManager.Instance.CurrentLives);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ClearActiveLifeManager(this);
        }
    }

    public void UpdateLifeCount(int currentLives)
    {
        for (int i = 0; i < lifeIcons.Count; i++)
        {
            if (lifeIcons[i] != null)
            {
                lifeIcons[i].enabled = (i < currentLives);
            }
        }
    }
}
