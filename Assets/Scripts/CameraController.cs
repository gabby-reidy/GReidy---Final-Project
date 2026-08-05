using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;
    [SerializeField] private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }
    }


    void LateUpdate()
    {
        if (playerTransform == null)
        {
            return;
        }

        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float minCamX = minX + camWidth;
        float maxCamX = maxX - camWidth;
        float minCamZ = minZ + camHeight;
        float maxCamZ = maxZ - camHeight;

        Vector3 desiredPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);

        float clampedX = Mathf.Clamp(desiredPosition.x, minCamX, maxCamX);
        float clampedZ = Mathf.Clamp(desiredPosition.z, minCamZ, maxCamZ);

        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}
