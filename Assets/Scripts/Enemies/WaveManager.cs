using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemyWave[] waves;               // List of waves
    [SerializeField] private Tilemap restrictedTilemap;       // Tilemap of tiles to not spawn enemies

    private int currentWaveIndex;
    private int killCount;
    private float waveTimer;
    private bool isSpawning;

    [SerializeField] private Camera mainCamera;               // Reference to the main camera
    [SerializeField] private float spawnOutsideDistance; // Distance from the camera's edge to spawn enemies

    private float cameraHeight;
    private float cameraWidth;
    private Vector3 minBounds;       // Bottom-left corner of the tilemap in world coordinates
    private Vector3 maxBounds;       // Top-right corner of the tilemap in world coordinates
    private const int MAX_RETRIES = 12;

    private List<GameObject> enemies;
    void Awake()
    {
        enemies = new List<GameObject>();
        isSpawning = true;
        // Error checking
        CheckValidWaves();
    }

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

        StartCoroutine(SpawnEnemies());
    }

    void CheckValidWaves()
    {
        if (waves.Length == 0)
        {
            Debug.LogWarning("Wave length is 0");
            isSpawning = false;
        }
        foreach (EnemyWave wave in waves)
        {
            if (wave.enemySpawns.Length == 0)
            {
                Debug.LogWarning("A wave have no enemy spawns");
                isSpawning = false;
            }
            foreach (EnemySpawn enemySpawn in wave.enemySpawns)
            {
                if (enemySpawn.spawnWeight <= 0)
                {
                    Debug.LogError("An enemy spawn have non-positive weights");
                    isSpawning = false;
                }
            }
        }
    }

    void Update()
    {
        if (!isSpawning) return;
        
        waveTimer += Time.deltaTime;
        EnemyWave currentWave = waves[currentWaveIndex];

        // Check if the current wave is complete (either by kill count or time or total enemies spawned)
        if (killCount >= currentWave.killCountThreshold 
            || waveTimer >= currentWave.timeLimit
            || currentWave.totalEnemiesToSpawn <= 0)
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
                int retryCount = 0;

                // Choose a random spawn point not on a restricted tile
                Vector2 spawnPoint = GetRandomPositionOutsideCamera();
                while (IsOnRestrictedTile(spawnPoint) && retryCount < MAX_RETRIES)
                {
                    spawnPoint = GetRandomPositionOutsideCamera();
                    retryCount++;
                }

                // Cap retries due to Unity crashing
                if (retryCount >= MAX_RETRIES)
                {
                    Debug.LogWarning("Failed to find a valid spawn point outside restricted tiles given " + MAX_RETRIES + " tries. Skipping this spawn");
                }
                else
                {
                    // Spawn an enemy based on weighted probabilities
                    GameObject enemyToSpawn = GetRandomEnemyBasedOnWeight(currentWave);
                    Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
                    currentWave.totalEnemiesToSpawn--;

                    enemies.Add(enemyToSpawn);
                }
            }

            yield return new WaitForSeconds(currentWave.spawnInterval);
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
        foreach (EnemySpawn enemySpawn in wave.enemySpawns)
        {
            totalWeight += enemySpawn.spawnWeight;
        }

        // Pick a random number between 0 and totalWeight - 1
        int randomWeight = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        // Select the enemy based on the random weight
        foreach (EnemySpawn enemySpawn in wave.enemySpawns)
        {
            cumulativeWeight += enemySpawn.spawnWeight;
            if (randomWeight < cumulativeWeight)
            {
                return enemySpawn.enemyPrefab;
            }
        }

        Debug.LogError("Bad spawn weight");
        return wave.enemySpawns[0].enemyPrefab; // Default fallback, this line should never be reached
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
        currentWaveIndex++;
        killCount = 0;
        waveTimer = 0f;

        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("All waves complete!");
            isSpawning = false;
        }
    }
}

