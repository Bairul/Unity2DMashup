using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    [Header("Types of enemies")]
    public WeightedObject[] enemySpawns; // Different enemy spawns

    [Header("Wave specifics")]
    public float spawnInterval;        // Time between enemy spawns
    public int killCountThreshold; // If kill count reaches this, move to the next wave
    public float timeLimit; // Maximum time to stay in this wave before moving on
    public int totalEnemiesToSpawn; // Number of enemies to spawn in this wave
}