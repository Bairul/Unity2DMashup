using UnityEngine;

/// <summary>
/// To be placed as a new child object in the Player object
/// </summary>
public class TargetAtMouseController : AttackController
{
    [SerializeField]
    private MouseIndicator mouseIndicator;

    protected override void LaunchAttack()
    {
        GameObject attack = Instantiate(attackStats.BaseStats.Prefab);
        attack.transform.position = transform.position;
        
        Vector3 toMouse = new(mouseIndicator.mouseDir.x, mouseIndicator.mouseDir.y);
        ProjectileBehavior projectileBehavior = attack.GetComponent<StraightProjectileBehavior>();
        
        projectileBehavior.SetAttackData(attackStats.ToAttackData());
        projectileBehavior.DirectionChecker(toMouse.normalized);
        projectileBehavior.RotateToDirection(mouseIndicator.rotationToMouse);
    }
}