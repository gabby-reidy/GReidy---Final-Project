using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Projectile projectile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //// TODO: Move projectile spawning to this script and fix error with player gameobject on the prefab
}
