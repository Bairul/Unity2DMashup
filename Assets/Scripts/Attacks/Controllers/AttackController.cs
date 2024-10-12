using UnityEngine;

/// <summary>
/// Base script for all attack controllers
/// </summary>

[RequireComponent(typeof(AttackStats))]

public abstract class AttackController : MonoBehaviour
{
    [SerializeField]
    protected PlayerStats playerStats;

    protected AttackStats attackStats;

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

    protected abstract void LaunchAttack();
}
