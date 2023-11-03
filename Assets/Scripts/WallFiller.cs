using UnityEngine;

public class WallFiller : MonoBehaviour
{
    [SerializeField] private GameObject backWall;
    [SerializeField] private Vector3 raycastOrigin;
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private LayerMask wallLayer; // Layer mask to filter which objects to hit with the raycast

    private void Start()
    {
        // Define the ray's direction
        Vector3 raycastDirection = Vector3.forward;

        // Prepare the raycast
        RaycastHit hit;

        // Activate the back wall if there's a open way at the other side, but is closed by the current wall 
        if (!Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastDistance, wallLayer))
            backWall.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        // Draw a raycast in the Scene view to visualize the ray.
        Gizmos.color = Color.red;
        Vector3 raycastDirection = Vector3.forward * raycastDistance; // Extend the ray for visualization.
        Gizmos.DrawRay(raycastOrigin, raycastDirection);
    }
}
