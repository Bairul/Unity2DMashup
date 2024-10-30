using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public SkillPool skillPool;
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

        playerStats = GetComponent<PlayerStats>();

        skillPool.InitAvailableSkills(playerStats.BaseStats.ElementalAffinities);
        ObtainStarterSkill(playerStats.BaseStats.StarterSkill);
        ObtainDashSkill(playerStats.BaseStats.DashSkill);
    }

    public void ObtainRandomSkills()
    {
        // temporary code for testing without UI
        if (attackSlotIndex >= maxSkillSlots)
        {
            Debug.LogWarning("Inventory is full, cannot add skill");
            return;
        }

        // List of skills randomly selected from the pool to offer to player
        List<GameObject> prefabs = skillPool.GetSkills(1);

        // player has chosen skill 1
        GameObject chosenSkill = prefabs[0];

        ApplyChosenSkill(chosenSkill);
    }

    void ObtainStarterSkill(GameObject startSkill)
    {
        if (starterSlot == null && startSkill != null)
        {
            starterSlot = Instantiate(startSkill, transform);
        }
    }

    void ObtainDashSkill(GameObject dashSkill)
    {
        if (dashSlot == null && dashSkill != null)
        {
            dashSlot = Instantiate(dashSkill, transform);
        }
    }

    public void ApplyChosenSkill(GameObject chosenSkill)
    {
        // TODO: check for the skill type
        SkillType skillData = chosenSkill.GetComponent<SkillType>();

        // TODO: check if the skill is already in inventory, 
        //       if it is replace it with its upgrade if not at max, 
        //       else add it as a new slot if slots not full

        bool foundSkill = false;
        if (skillData.IsTypeEquals(SkillOfType.Attribue))
        {
            for (int i = 0; i < attributeSlotIndex && !foundSkill; i++)
            {
                SkillType skillData2 = attributeSlots[i].GetComponent<SkillType>();

                if (skillData.IsSameName(skillData2))
                {
                    foundSkill = true;
                    // level up existing skill by replacing it with the new skill
                }
            }

            if (!foundSkill) attributeSlots[attributeSlotIndex++] = Instantiate(chosenSkill, transform);
        }
        else
        {
            for (int i = 0; i < attackSlotIndex && !foundSkill; i++)
            {
                SkillType skillData2 = attackSlots[i].GetComponent<SkillType>();

                if (skillData.IsSameName(skillData2))
                {
                    foundSkill = true;
                    // level up existing skill by replacing it with the new skill
                }
            }

            if (!foundSkill) attackSlots[attackSlotIndex++] = Instantiate(chosenSkill, transform);
        }

        skillPool.LevelUpSkillInPool(skillData.SkillName);
    }
}
