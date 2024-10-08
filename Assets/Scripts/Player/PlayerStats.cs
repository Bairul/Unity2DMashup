using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : GenericStats
{
    private CharacterScriptableObject baseData;
    public CharacterScriptableObject BaseData {get => baseData;}

    // current stats
    [HideInInspector] public float currentMagnetRange;
    [SerializeField] private PlayerCollector playerMagnet;

    // Experience
    public int currentExperience;
    public int currentExperienceCap;
    public int currentLevel = 1;
    private int currentRangeIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        baseData = (CharacterScriptableObject) genericData;
        currentMagnetRange = baseData.MagnetRange;
    }

    void Start()
    {
        UpdateExperienceCap();
        UpdateMagnetRange(currentMagnetRange);
    }

    public void UpdateMagnetRange(float radius)
    {
        currentMagnetRange = radius;
        playerMagnet.Range.radius = currentMagnetRange;
    }

    public void IncreaseExperience(int amount)
    {
        if (currentLevel >= baseData.LastLevel) return;

        currentExperience += amount;
        if (currentExperience >= currentExperienceCap)
        {
            currentLevel++;
            currentExperience -= currentExperienceCap;
            UpdateExperienceCap();
        }
    }

    // terrible code lol
    private void UpdateExperienceCap()
    {
        if (baseData.ExperienceCapMode == ExperienceCapMode.FixedByLevelRange)
        {
            if (currentRangeIndex >= baseData.LevelRanges.Count) return;
            
            if (currentLevel > baseData.LevelRanges[currentRangeIndex].maxLevel)
            {
                currentRangeIndex++;
            }

            if (currentRangeIndex >= baseData.LevelRanges.Count) return;

            currentExperienceCap = baseData.LevelRanges[currentRangeIndex].experienceCap;
        }
        else
        {
            currentExperienceCap = (int) (Mathf.Pow(currentLevel, baseData.XpFunctionExponent) * baseData.XpFunctionCoefficient + baseData.XpFunctionConstant);
        }
    }

    protected override void Kill() 
    {
        Debug.Log("You Died");
    }
}
