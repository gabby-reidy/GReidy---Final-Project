using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float fireRate = .5f;

    private Vector3 lookDirection;
    private float nextFireTime;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 mouseWorld = hit.point;
            mouseWorld.y = transform.position.y;
            lookDirection = mouseWorld - transform.position;
        }

        HandleFire();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    /// <summary>
    /// Handles input for firing projectiles and adds a timer to prevent player spamming
    /// </summary>
    private void HandleFire()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + fireRate;
            AudioManager.Instance.PlayerProjectileSFX();
        }
    }

    /// <summary>
    /// Rotates the (empty) child object on the player to the mouse direction set in Update
    /// </summary>
    private void Rotate()
    {
        if (lookDirection.sqrMagnitude < 0.001f)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.up, lookDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Fires projectile prefab and sets its velocity on instantiation
    /// </summary>
    private void FireProjectile()
    {
        if (projectilePrefab == null)
        {
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, projectilePrefab.transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(lookDirection.x * speed, 0f, lookDirection.z * speed);
    }
}
