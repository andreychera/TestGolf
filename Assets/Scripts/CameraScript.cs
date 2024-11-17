using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    public float rotationSpeed;
    public float distance;
    public float height;
    public float minDistance;
    public float maxDistance;
    public float scrollSpeed;

    private float currentAngle;
    private float initialHeight;

    void Start()
    {
        initialHeight = height;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1)) 
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            currentAngle -= horizontalInput * rotationSpeed;
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        distance -= scrollInput * scrollSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        height = initialHeight * (distance / maxDistance);

        float x = target.position.x + Mathf.Cos(currentAngle * Mathf.Deg2Rad) * distance;
        float z = target.position.z + Mathf.Sin(currentAngle * Mathf.Deg2Rad) * distance;
        float y = target.position.y + height;

        Vector3 desiredPosition = new Vector3(x, y, z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}