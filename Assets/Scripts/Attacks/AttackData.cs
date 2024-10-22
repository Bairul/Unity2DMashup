
using UnityEngine;

public class AttackData
{
    public ElementalType element;
    public float damage;
    public float critDmg;
    public float critRate;
    public float pierce;
    public float speed;
    public float lifespan;
    public float range;

    public AttackData(AttackStats attackStats, PlayerStats playerStats)
    {
        damage = playerStats.currentDamage * attackStats.currentAttackMultiplier + attackStats.currentDamage;
        critRate = playerStats.currentCritRate;
        critDmg = playerStats.currentCritDmg;

        element = attackStats.BaseStats.Element;
        pierce = attackStats.currentPierce;
        speed = attackStats.currentPierce;
        speed = attackStats.currentSpeed;
        lifespan = attackStats.currentLifespan;
        range = attackStats.currentRange;
    }

    public float GetTotalDamage()
    {
        return damage * (Random.value < critRate ? critDmg : 1);
    }
}
