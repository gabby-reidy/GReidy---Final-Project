using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifetime = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.AddRelativeForce(Vector3.down * speed, ForceMode.Impulse);
        //Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
