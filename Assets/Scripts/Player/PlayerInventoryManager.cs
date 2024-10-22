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

    public void ObtainRandomSkill()
    {
        if (attackSlotIndex >= maxSkillSlots)
        {
            Debug.LogWarning("Inventory is full, cannot add skill");
            return;
        }

        List<GameObject> prefabs = skillPool.GetSkills(1);

        if (prefabs.Count > 0)
        {
            attackSlots[attackSlotIndex++] = Instantiate(prefabs[0], transform);
        }
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
}
