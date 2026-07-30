using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform Player;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifetime = 5f;

    private Vector3 direction;
    private SpawnManager spawnManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        direction = (Player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //TODO: makes player take damage/lose life?
        }
    }
}
