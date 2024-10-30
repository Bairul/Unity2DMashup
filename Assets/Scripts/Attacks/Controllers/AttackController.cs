using UnityEngine;

/// <summary>
/// Base script for all attack controllers
/// </summary>

[RequireComponent(typeof(AttackStats), typeof(SkillType))]

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
        return new AttackData(attackStats, playerStats);
    }

    public ElementalType PreGetElementalType()
    {
        return GetComponent<AttackStats>().BaseStats.Element;
    }

    protected virtual void InstantiateAttack(Vector3 direction, Quaternion rotation)
    {
        GameObject attack = Instantiate(attackStats.BaseStats.Prefab);
        attack.transform.position = transform.position;

        ProjectileBehavior projectileBehavior = attack.GetComponent<ProjectileBehavior>();
        
        projectileBehavior.SetAttackData(GetAttackData());
        projectileBehavior.DirectionChecker(direction);
        projectileBehavior.RotateToDirection(rotation);
    }

    protected abstract void LaunchAttack();
}
