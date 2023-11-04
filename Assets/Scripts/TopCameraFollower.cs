using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TopCameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;  // The target to follow on the X-axis
    [SerializeField] private float smoothSpeed = 5.0f;  // Smoothing speed of the follow
    [SerializeField] private Vector3 offset = new Vector3(0f, 35f, 0f);

    private void Start() => target = PlayerController.Instance.transform;

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
