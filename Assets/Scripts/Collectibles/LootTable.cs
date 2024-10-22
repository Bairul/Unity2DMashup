using UnityEngine;

public class LootTable : MonoBehaviour
{
    [Header("Loot Table")]
    [SerializeField] private WeightedObject[] lootItems;  // Array of loot items with weights

    private bool canDrop;

    void Awake()
    {
        canDrop = true;
        CheckValidLoot(); 
    }

    private void CheckValidLoot()
    {
        if (lootItems.Length == 0)
        {
            canDrop = false;
        }
        foreach (WeightedObject loot in lootItems)
        {
            if (loot.weight <= 0)
            {
                Debug.LogError("An enemy loot have non-positive weights");
                canDrop = false;
            }
        }
    }

    // Method to roll for loot
    GameObject GetRandomLootBasedOnWeight()
    {
        // Calculate the total weight of all loot items
        int totalWeight = 0;
        foreach (WeightedObject loot in lootItems)
        {
            totalWeight += (int) loot.weight;
        }

        // Generate a random number from 0 to totalWeight - 1
        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        // Determine which loot to drop based on random value
        foreach (WeightedObject loot in lootItems)
        {
            cumulativeWeight += (int) loot.weight;
            if (randomValue < cumulativeWeight)
            {
                return loot.prefab; // Return the loot item or null if it's "empty"
            }
        }

        Debug.LogError("Bad loot weight");
        return null; // Fallback to no loot, this line should never be reached
    }
    
    // Call this when the enemy dies
    public void DropLoot()
    {
        if (!canDrop) return;

        GameObject loot = GetRandomLootBasedOnWeight();
        if (loot != null)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }
}
