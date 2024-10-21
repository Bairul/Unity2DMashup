using System.Collections.Generic;
using SuperTiled2Unity.Editor.LibTessDotNet;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public int maxSkillSlots = 5;
    public GameObject[] attackSlots;
    public GameObject[] attributeSlots;
    public GameObject starterSlot;
    public GameObject dashSlot;

    private int attackSlotIndex;
    private int attributeSlotIndex;
    private PlayerStats playerStats;

    void Awake()
    {
       attackSlots = new GameObject[maxSkillSlots];
       attributeSlots = new GameObject[maxSkillSlots];
    }

    public void SetPlayerStats(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
    }

    GameObject GetRandomSkillBasedOnWeight(List<WeightedObject> skillPool)
    {
        // Calculate the total weight of all loot items
        int totalWeight = 0;
        foreach (WeightedObject skill in skillPool)
        {
            totalWeight += skill.weight;
        }

        // Generate a random number from 0 to totalWeight - 1
        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        // Determine which loot to drop based on random value
        foreach (WeightedObject skill in skillPool)
        {
            cumulativeWeight += skill.weight;
            if (randomValue < cumulativeWeight)
            {
                return skill.prefab; 
            }
        }

        Debug.LogError("Bad skill weight");
        return null; // this line should never be reached
    }

    public void ObtainRandomSkill(List<WeightedObject> skillPool)
    {
        if (attackSlotIndex >= maxSkillSlots)
        {
            Debug.LogWarning("Inventory is full, cannot add skill");
            return;
        }

        GameObject prefab = GetRandomSkillBasedOnWeight(skillPool);

        if (prefab != null)
        {
            attackSlots[attackSlotIndex] = Instantiate(prefab, transform);
            AttackController ctrl = attackSlots[attackSlotIndex++].GetComponent<AttackController>();
            ctrl.SetPlayerStats(playerStats);
        }
    }

    public void ObtainStarterSkill(GameObject startSkill)
    {
        if (starterSlot == null && startSkill != null)
        {
            starterSlot = Instantiate(startSkill, transform);
            AttackController ctrl = starterSlot.GetComponent<AttackController>();
            ctrl.SetPlayerStats(playerStats);
        }
    }

    public void ObtainDashSkill(GameObject dashSkill)
    {
        if (dashSlot == null && dashSkill != null)
        {
            dashSlot = Instantiate(dashSkill, transform);
        }
    }
}
