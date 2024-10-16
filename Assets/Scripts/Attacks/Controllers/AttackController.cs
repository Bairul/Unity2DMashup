using UnityEngine;

/// <summary>
/// Base script for all attack controllers
/// </summary>

[RequireComponent(typeof(AttackStats))]

public abstract class AttackController : MonoBehaviour
{
    private PlayerStats playerStats;

    protected AttackStats attackStats;

    public void SetPlayerStats(PlayerStats stats)
    {
        playerStats = stats;
    }

    protected virtual void Awake()
    {
        attackStats = GetComponent<AttackStats>();
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

    protected abstract void LaunchAttack();
}
