using UnityEngine;

/// <summary>
/// Base script for all attack controllers
/// </summary>
public abstract class AttackController : MonoBehaviour
{
    [SerializeField]
    protected AttackStats attackStats;

    [SerializeField]
    protected PlayerStats playerStats;

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        if (attackStats.CanAttack())
        {
            LaunchAttack();
        }
    }

    protected abstract void LaunchAttack();
}
