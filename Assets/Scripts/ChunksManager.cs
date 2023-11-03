using System.Collections.Generic;
using UnityEngine;

public class ChunksManager : MonoBehaviour
{
    [Header("Chunks generation settings")]
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private int chunkSize = 5;
    [SerializeField] private int viewDistance = 1;
    [SerializeField] private Transform player;
    [SerializeField] private List<int> chunkPossibleRotations = new List<int> { 0, 90, 180 };

    private Vector2Int playerChunkPos;
    private Dictionary<Vector2Int, GameObject> activeChunks = new Dictionary<Vector2Int, GameObject>();

    private void Start()
    {
        playerChunkPos = new Vector2Int(0, 0);
        GenerateChunksAroundPlayer(playerChunkPos);
    }

    private void Update()
    {
        Vector2Int currentPlayerChunkPos = new Vector2Int(Mathf.FloorToInt(player.position.x / chunkSize), Mathf.FloorToInt(player.position.z / chunkSize));

        if (currentPlayerChunkPos != playerChunkPos)
        {
            playerChunkPos = currentPlayerChunkPos;
            GenerateChunksAroundPlayer(playerChunkPos);
        }
    }

    private void GenerateChunksAroundPlayer(Vector2Int playerChunkPos)
    {
        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int z = -viewDistance; z <= viewDistance; z++)
            {
                Vector2Int chunkPos = new Vector2Int(playerChunkPos.x + x, playerChunkPos.y + z);

                if (!activeChunks.ContainsKey(chunkPos))
                {
                    // Create a new chunk if there's no one
                    CreateChunk(chunkPos);
                }
                else
                {
                    // Activate the chunk if this already exists
                    activeChunks[chunkPos].SetActive(true);
                }
            }
        }

        // Remove chunks that are no longer in the view distance
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();
        foreach (var kvp in activeChunks)
        {
            Vector2Int chunkPos = kvp.Key;
            if (Mathf.Abs(chunkPos.x - playerChunkPos.x) > viewDistance || Mathf.Abs(chunkPos.y - playerChunkPos.y) > viewDistance)
            {
                chunksToRemove.Add(chunkPos);
            }
        }

        foreach (Vector2Int chunkPos in chunksToRemove)
        {
            DeactivateChunk(chunkPos);
        }
    }

    private void CreateChunk(Vector2Int chunkPos)
    {
        int randomIndex = Random.Range(0, chunkPossibleRotations.Count);

        float spawnRotationY = chunkPossibleRotations[randomIndex];
        Vector3 spawnPos = new Vector3(chunkPos.x * chunkSize, 0, chunkPos.y * chunkSize);

        GameObject chunk = Instantiate(chunkPrefab, spawnPos, Quaternion.Euler(0f, spawnRotationY, 0f));
        activeChunks.Add(chunkPos, chunk);
    }

    private void DeactivateChunk(Vector2Int chunkPos)
    {
        if (activeChunks.TryGetValue(chunkPos, out GameObject chunk))
        {
            chunk.SetActive(false);
        }
    }
}
