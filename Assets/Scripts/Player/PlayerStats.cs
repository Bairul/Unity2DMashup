using UnityEngine;

public class PlayerStats : GenericStats
{
    private CharacterScriptableObject baseStats;
    public CharacterScriptableObject BaseStats {get => baseStats;}

    // current stats
    [HideInInspector] public float currentMagnetRange;
    [HideInInspector] public float currentCritRate;
    [HideInInspector] public float currentCritDmg;
    [SerializeField] private PlayerCollector playerMagnet;
    [SerializeField] private PlayerInventoryManager playerInventory;

    // Experience
    public int currentExperience;
    public int currentExperienceCap;
    public int currentLevel = 1;
    private int currentRangeIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        baseStats = (CharacterScriptableObject) genericStats;
        currentMagnetRange = baseStats.MagnetRange;
        currentCritRate = baseStats.CritRate;
        currentCritDmg = baseStats.CritDamage;

        playerInventory.ObtainStarterSkill(baseStats.StarterSkill, this);
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
    void LevelUp()
    {
        currentLevel++;
        currentExperience -= currentExperienceCap;
        UpdateExperienceCap();

        playerInventory.ObtainRandomSkill(baseStats.SkillPool, this);
    }

    public void IncreaseExperience(int amount)
    {
        if (currentLevel >= baseStats.LastLevel) return;

        currentExperience += amount;
        if (currentExperience >= currentExperienceCap)
        {
            LevelUp();
        }
    }

    // terrible code lol
    private void UpdateExperienceCap()
    {
        if (baseStats.ExperienceCapMode == ExperienceCapMode.FixedByLevelRange)
        {
            if (currentRangeIndex >= baseStats.LevelRanges.Count) return;
            
            if (currentLevel > baseStats.LevelRanges[currentRangeIndex].maxLevel)
            {
                currentRangeIndex++;
            }

            if (currentRangeIndex >= baseStats.LevelRanges.Count) return;

            currentExperienceCap = baseStats.LevelRanges[currentRangeIndex].experienceCap;
        }
        else
        {
            currentExperienceCap = (int) (Mathf.Pow(currentLevel, baseStats.XpFunctionExponent) * baseStats.XpFunctionCoefficient + baseStats.XpFunctionConstant);
        }
    }

    protected override void Kill() 
    {
        Debug.Log("You Died");
    }
}
