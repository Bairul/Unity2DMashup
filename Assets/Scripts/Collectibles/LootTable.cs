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

        WeightedObject.NormalizeWeights(lootItems);
    }
    
    // Call this when the enemy dies
    public void DropLoot()
    {
        if (!canDrop) return;

        WeightedObject loot = WeightedObject.GetRandomWeightedObject(lootItems);
        if (loot != null && loot.prefab != null)
        {
            Instantiate(loot.prefab, transform.position, Quaternion.identity);
        }
    }
}
