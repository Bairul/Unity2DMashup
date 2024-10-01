using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemyWave[] waves; // List of waves
    public Transform[] spawnPoints; // Array of spawn points
    public float spawnInterval = 2f; // Time between enemy spawns
    private int currentWaveIndex = 0;
    private int killCount = 0;
    private float waveTimer = 0f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
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
        while (true)
        {
            EnemyWave currentWave = waves[currentWaveIndex];

            if (currentWave.totalEnemiesToSpawn > 0)
            {
                // Choose a random spawn point
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // Spawn an enemy based on weighted probabilities
                GameObject enemyToSpawn = GetRandomEnemyBasedOnWeight(currentWave);
                Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);

                currentWave.totalEnemiesToSpawn--;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
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
            Debug.Log("All waves complete!");
            // End game logic here, if necessary
        }
    }
}

