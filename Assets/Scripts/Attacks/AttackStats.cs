using UnityEngine;

public class AttackStats : MonoBehaviour
{
    [SerializeField] private AttackScriptableObject baseStats;
    public AttackScriptableObject BaseStats {get => baseStats;}

    // Current Stats
    [HideInInspector] public float currentSpeed;
    [HideInInspector] public float currentPierce;
    [HideInInspector] public float currentDamage;
    [HideInInspector] public float currentCooldown;
    [HideInInspector] public float currentRange;
    [HideInInspector] public float currentLifespan;

    void Awake()
    {
        currentSpeed = baseStats.Speed;
        currentPierce = baseStats.Pierce;
        currentDamage = baseStats.Damage;
        currentCooldown = baseStats.CooldownDuration;
        currentRange = baseStats.Range;
        currentLifespan = baseStats.Lifespan;
    }

    public bool CanAttack() 
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            ResetCooldown();
            return true;
        }
        return false;
    }

    public void ResetCooldown() 
    {
        currentCooldown = baseStats.CooldownDuration;
    }

    public AttackData ToAttackData()
    {
        return new AttackData(currentDamage, currentPierce, currentSpeed, currentLifespan);
    }
}
