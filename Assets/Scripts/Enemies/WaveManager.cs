using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private EnemyWave[] waves;               // List of waves

    [SerializeField]
    private Tilemap restrictedTilemap;       // Tilemap of tiles to not spawn enemies

    [SerializeField]
    private float spawnInterval = 2f;        // Time between enemy spawns

    private int currentWaveIndex = 0;
    private int killCount = 0;
    private float waveTimer = 0f;
    private bool isSpawning = true;

    [SerializeField]
    private Camera mainCamera;               // Reference to the main camera

    [SerializeField]
    private float spawnOutsideDistance = 5f; // Distance from the camera's edge to spawn enemies

    private float cameraHeight;
    private float cameraWidth;
    private Vector3 minBounds;       // Bottom-left corner of the tilemap in world coordinates
    private Vector3 maxBounds;       // Top-right corner of the tilemap in world coordinates

    void Start()
    {
        // Calculate camera boundaries
        cameraHeight = 2f * mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;

        // Get the bounds of the Tilemap in cell coordinates
        BoundsInt bounds = restrictedTilemap.cellBounds;
        // Convert the bottom-left and top-right corners to world coordinates
        minBounds = restrictedTilemap.CellToWorld(bounds.min);
        maxBounds = restrictedTilemap.CellToWorld(bounds.max);

        if (waves.Length <= 0) {
            isSpawning = false;
        }

        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        if (!isSpawning) return;
        
        waveTimer += Time.deltaTime;

        // Check if the current wave is complete (either by kill count or time)
        if (killCount >= waves[currentWaveIndex].killCountThreshold 
            || waveTimer >= waves[currentWaveIndex].timeLimit)
        {
            NextWave();
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            EnemyWave currentWave = waves[currentWaveIndex];

            if (currentWave.totalEnemiesToSpawn > 0)
            {
                // Choose a random spawn point
                Vector2 spawnPoint = GetRandomPositionOutsideCamera();

                // If spawn position is on a restricted tile, find a new spawn point
                while (IsOnRestrictedTile(spawnPoint))
                {
                    spawnPoint = GetRandomPositionOutsideCamera();
                }

                // Spawn an enemy based on weighted probabilities
                GameObject enemyToSpawn = GetRandomEnemyBasedOnWeight(currentWave);
                Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);

                currentWave.totalEnemiesToSpawn--;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector2 GetRandomPositionOutsideCamera()
    {
        // Get the camera's center position
        Vector3 cameraCenter = mainCamera.transform.position;

        // Randomly decide which side of the camera the enemy should spawn on (up, down, left, right)
        int side = Random.Range(0, 4); // 0 = top, 1 = bottom, 2 = left, 3 = right

        var spawnPosition = side switch
        {
            // Top
            0 => new Vector2(
                                Random.Range(cameraCenter.x - cameraWidth / 2, cameraCenter.x + cameraWidth / 2),
                                cameraCenter.y + cameraHeight / 2 + spawnOutsideDistance),
            // Bottom
            1 => new Vector2(
                                Random.Range(cameraCenter.x - cameraWidth / 2, cameraCenter.x + cameraWidth / 2),
                                cameraCenter.y - cameraHeight / 2 - spawnOutsideDistance),
            // Left
            2 => new Vector2(
                                cameraCenter.x - cameraWidth / 2 - spawnOutsideDistance,
                                Random.Range(cameraCenter.y - cameraHeight / 2, cameraCenter.y + cameraHeight / 2)),
            // Right
            _ => new Vector2(
                                cameraCenter.x + cameraWidth / 2 + spawnOutsideDistance,
                                Random.Range(cameraCenter.y - cameraHeight / 2, cameraCenter.y + cameraHeight / 2)),
        };
        return spawnPosition;
    }

    GameObject GetRandomEnemyBasedOnWeight(EnemyWave wave)
    {
        // Sum up the total weights
        int totalWeight = 0;
        for (int i = 0; i < wave.spawnWeights.Length; i++)
        {
            totalWeight += wave.spawnWeights[i];
        }

        // Pick a random number between 0 and totalWeight - 1
        int randomWeight = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        // Select the enemy based on the random weight
        for (int i = 0; i < wave.enemyPrefabs.Length; i++)
        {
            cumulativeWeight += wave.spawnWeights[i];
            if (randomWeight < cumulativeWeight)
            {
                return wave.enemyPrefabs[i];
            }
        }

        return wave.enemyPrefabs[0]; // Default fallback
    }

    bool IsOnRestrictedTile(Vector2 position)
    {
        // Check for map bounds
        if (position.x < minBounds.x || position.x > maxBounds.x || position.y < minBounds.y || position.y > maxBounds.y)
        {
            return true;
        }
        // Convert world position to tilemap position
        Vector3Int tilePosition = restrictedTilemap.WorldToCell(position);

        // Check if there is a tile at the position (indicating it's a restricted area)
        TileBase tileAtPosition = restrictedTilemap.GetTile(tilePosition);
        return tileAtPosition != null;  // Returns true if there's a restricted tile
    }

    public void OnEnemyKilled()
    {
        killCount++;
    }

    void NextWave()
    {
        if (currentWaveIndex < waves.Length - 1)
        {
            currentWaveIndex++;
            killCount = 0;
            waveTimer = 0f;
        }
        else
        {
            // Debug.Log("All waves complete!");
            // End game logic here, if necessary
            isSpawning = false;
        }
    }
}

