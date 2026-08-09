using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;

    private Vector3 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = -Input.GetAxisRaw("Vertical");
        movement.y = 0f;
        movement.z = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// handles player movement based on input vector set in update. checks for movement for walk animation
    /// </summary>
    private void Move()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
        if (movement.x != 0 || movement.z != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else if (movement.x == 0 && movement.z == 0)
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoseLife();
            }
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            if (GameManager.Instance != null) 
            {
                GameManager.Instance.LoseLife();
            }
        }
    }
}
