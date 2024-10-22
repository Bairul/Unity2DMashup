using UnityEngine;

/// <summary>
/// Base script for all attack controllers
/// </summary>

[RequireComponent(typeof(AttackStats))]

public abstract class AttackController : MonoBehaviour
{
    protected PlayerStats playerStats;

    protected AttackStats attackStats;
    protected PlayerController playerController;

    protected virtual void Awake()
    {
        attackStats = GetComponent<AttackStats>();
        
        playerStats = GetComponentInParent<PlayerStats>();
        playerController = GetComponentInParent<PlayerController>();
    }

    // updates after all updates
    protected virtual void LateUpdate()
    {
        if (attackStats.CanAttack())
        {
            LaunchAttack();
        }
    }

    protected virtual AttackData GetAttackData()
    {
        AttackData attackData = attackStats.ToAttackData();
        // player atk scaling
        attackData.damage += playerStats.currentDamage * attackStats.currentAttackMultiplier;

        // crit mechanic
        if (Random.value < playerStats.currentCritRate)
        {
            attackData.critDmg = playerStats.currentCritDmg;
        }

        return attackData;
    }

    public ElementalType PreGetElementalType()
    {
        return GetComponent<AttackStats>().BaseStats.Element;
    }

    protected abstract void LaunchAttack();
}
