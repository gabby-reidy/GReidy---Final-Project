using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float fireRate = .5f;

    private Vector3 lookDirection;
    private float nextFireTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // change to raycast if i have time at the end 
        mouseWorld.y = transform.position.y;

        lookDirection = mouseWorld - transform.position;

        HandleFire();
    }

    private void HandleFire()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + fireRate;
            AudioManager.Instance.PlayerProjectileSFX();
        }
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (lookDirection.sqrMagnitude < 0.001f)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.up, lookDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    private void FireProjectile()
    {
        if (projectilePrefab == null)
        {
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(lookDirection.x * speed, 0f, lookDirection.z * speed);
    }

}
