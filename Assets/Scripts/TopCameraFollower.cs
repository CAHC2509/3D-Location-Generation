using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TopCameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;  // The target to follow on the X-axis
    [SerializeField] private float smoothSpeed = 5.0f;  // Smoothing speed of the follow

    private Vector3 offset;

    private void Start() => offset = transform.position - target.position; // Calculate the initial offset

    private void LateUpdate()
    {
        // Calculate the target position
        Vector3 targetPosition = target.position + offset;

        // Smoothly move the camera to the target's X position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Set the camera's position, preserving its Y and Z coordinates
        transform.position = new Vector3(smoothedPosition.x, transform.position.y, smoothedPosition.z);
    }
}
