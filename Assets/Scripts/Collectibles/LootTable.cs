using UnityEngine;

// Define the Loot Item
[System.Serializable]
public class Loot
{
    [Header("The loot prefab (can be an empty)")]
    public GameObject item;  // The loot item (can be an empty game object for "no loot")
    public int weight;       // The weight of this loot
}

public class LootTable : MonoBehaviour
{
    [Header("Loot Table")]
    [SerializeField] private Loot[] lootItems;  // Array of loot items with weights
    
    // Method to roll for loot
    GameObject GetRandomLootBasedOnWeight()
    {
        // Calculate the total weight of all loot items
        int totalWeight = 0;
        foreach (Loot loot in lootItems)
        {
            totalWeight += loot.weight;
        }

        // Generate a random number from 0 to totalWeight - 1
        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        // Determine which loot to drop based on random value
        foreach (Loot loot in lootItems)
        {
            cumulativeWeight += loot.weight;
            if (randomValue < cumulativeWeight)
            {
                return loot.item; // Return the loot item or null if it's "empty"
            }
        }

        Debug.LogError("Bad loot weight");
        return null; // Fallback to no loot, this line should never be reached
    }
    
    // Call this when the enemy dies
    public void DropLoot()
    {
        GameObject loot = GetRandomLootBasedOnWeight();
        if (loot != null)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }
}
