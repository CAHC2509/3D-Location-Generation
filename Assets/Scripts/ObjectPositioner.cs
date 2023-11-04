using System.Collections.Generic;
using UnityEngine;

public class ObjectPositioner : MonoBehaviour
{
    [Header("Object Settings")]
    [SerializeField] private List<GameObject> objectPrefabs = new List<GameObject>(); // List of object prefabs to choose from.

    [Header("Zone Settings")]
    [SerializeField] private Vector2 zoneSize = new Vector2(19, 19); // Size of the 19x19 zone.
    [SerializeField] private float minSeparation = 1.0f; // Minimum separation between objects.
    [SerializeField] private int maxObjects = 5; // Maximum number of objects.

    private List<Vector3> occupiedPositions = new List<Vector3>();

    void Start()
    {
        for (int i = 0; i < maxObjects; i++)
        {
            // Try to position an object in the center of the zone.
            Vector3 randomPosition = GetRandomPositionInZone();

            // Randomly select an object prefab from the list.
            GameObject selectedObjectPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Count)];

            // Create a new object at the random position while maintaining its Y position and adding a random Y rotation.
            GameObject newObj = Instantiate(selectedObjectPrefab, randomPosition, Quaternion.Euler(0, Random.Range(0, 360), 0));
            newObj.transform.position = new Vector3(newObj.transform.position.x, selectedObjectPrefab.transform.position.y, newObj.transform.position.z);
            newObj.transform.parent = transform;

            // Add the object's position to the list of occupied positions.
            occupiedPositions.Add(randomPosition);
        }
    }

    Vector3 GetRandomPositionInZone()
    {
        int maxAttempts = 75; // Prevent infinite loops in case of placement issues.
        int currentAttempt = 0;

        while (currentAttempt < maxAttempts)
        {
            Vector3 randomPosition = new Vector3(
                transform.position.x + Random.Range(-zoneSize.x / 2, zoneSize.x / 2),
                objectPrefabs[0].transform.position.y,
                transform.position.z + Random.Range(-zoneSize.y / 2, zoneSize.y / 2)
            );

            // Check if the position is sufficiently separated from other occupied positions.
            bool isValidPosition = true;
            foreach (Vector3 occupiedPos in occupiedPositions)
            {
                float distance = Vector3.Distance(occupiedPos, randomPosition);
                if (distance < minSeparation)
                {
                    isValidPosition = false;
                    break;
                }
            }

            // If the position is valid, return it.
            if (isValidPosition)
            {
                return randomPosition;
            }

            currentAttempt++;
        }

        Debug.LogWarning("Unable to find a valid position after " + maxAttempts + " attempts.");
        return Vector3.zero;
    }

    // Draw the zone in Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 zoneCenter = transform.position;
        Gizmos.DrawWireCube(zoneCenter, new Vector3(zoneSize.x, 0.1f, zoneSize.y));
    }
}
