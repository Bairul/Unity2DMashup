using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public GameObject[] enemyPrefabs; // Array of enemy types to spawn (e.g., EnemyA, EnemyB)
    public int[] spawnWeights; // Array of spawn weights corresponding to each enemy
    public int killCountThreshold; // If kill count reaches this, move to the next wave
    public float timeLimit; // Maximum time to stay in this wave before moving on
    public int totalEnemiesToSpawn; // Number of enemies to spawn in this wave
}

