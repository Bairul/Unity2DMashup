using UnityEngine;

/// <summary>
/// Base script for all attack controllers
/// </summary>
public class AttackController : MonoBehaviour
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

    protected virtual private void LaunchAttack()
    {

    }
}
