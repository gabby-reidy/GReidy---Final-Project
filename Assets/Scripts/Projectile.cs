using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifetime = 5f;

    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
        //rb.AddRelativeForce(Vector3.down * speed, ForceMode.Impulse);
        //Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        direction = (Player.transform.position - transform.position).normalized;
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
