using UnityEngine;

[RequireComponent(typeof(PlayerInventoryManager))]
public class PlayerStats : GenericStats
{
    private CharacterScriptableObject baseStats;
    public CharacterScriptableObject BaseStats { get => baseStats; }

    private PlayerInventoryManager playerInventory;
    [SerializeField] private PlayerCollector playerMagnet;

    // current stats
    public HealthBar healthBar;
    [HideInInspector] public float currentMagnetRange;
    [HideInInspector] public float currentCritRate;
    [HideInInspector] public float currentCritDmg;

    // Experience
    public XPBar xpBar;
    public int currentExperience;
    private int currentExperienceCap;
    public int currentLevel = 1;
    private int currentRangeIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        baseStats = (CharacterScriptableObject)genericStats;
        currentMaxHealth = baseStats.MaxHealth;
        currentHealth = baseStats.MaxHealth;

        currentMagnetRange = baseStats.MagnetRange;
        currentCritRate = baseStats.CritRate;
        currentCritDmg = baseStats.CritDamage;

        playerInventory = GetComponent<PlayerInventoryManager>();
    }

    void Start()
    {
        UpdateExperienceCap();
        UpdateMagnetRange(currentMagnetRange);

        healthBar.SetMaxHealth(currentMaxHealth);
        xpBar.SetMaxXP(currentExperienceCap);
        xpBar.SetLevel(currentLevel);
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
        xpBar.SetXP(currentExperience);
        xpBar.SetLevel(currentLevel);

        UpdateExperienceCap();
        playerInventory.ObtainRandomSkills();
    }

    public void IncreaseExperience(int amount)
    {
        if (currentLevel >= baseStats.LastLevel) return;

        currentExperience += amount;
        xpBar.SetXP(currentExperience);

        if (currentExperience >= currentExperienceCap)
        {
            LevelUp();
        }
    }

    private void UpdateExperienceCap()
    {
        // terrible code lol
        if (currentRangeIndex >= baseStats.LevelRanges.Count) return;

        if (currentLevel > baseStats.LevelRanges[currentRangeIndex].maxLevel)
        {
            currentRangeIndex++;
        }

        if (currentRangeIndex >= baseStats.LevelRanges.Count) return;

        currentExperienceCap = baseStats.LevelRanges[currentRangeIndex].experienceCap;

        // if (baseStats.ExperienceCapMode == ExperienceCapMode.FixedByLevelRange)
        // {

        // }
        // else
        // {
        //     currentExperienceCap = (int) (Mathf.Pow(currentLevel, baseStats.XpFunctionExponent) * baseStats.XpFunctionCoefficient + baseStats.XpFunctionConstant);
        // }
    }

    protected override void DamageTaken(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    protected override void Kill()
    {
        Debug.Log("You Died");
    }
}
