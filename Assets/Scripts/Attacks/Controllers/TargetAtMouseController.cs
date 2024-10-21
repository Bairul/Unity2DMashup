using UnityEngine;

/// <summary>
/// To be placed as a new child object in the Player object
/// </summary>
public class TargetAtMouseController : AttackController
{
    private MouseIndicator mouseIndicator;

    void Start()
    {
        mouseIndicator = GameWorld.Instance.GetPlayerController.PlayerMouseIndicator;
    }

    protected override void LaunchAttack()
    {
        GameObject attack = Instantiate(attackStats.BaseStats.Prefab);
        attack.transform.position = transform.position;
        
        Vector3 toMouse = new(mouseIndicator.mouseDir.x, mouseIndicator.mouseDir.y);
        ProjectileBehavior projectileBehavior = attack.GetComponent<ProjectileBehavior>();
        
        projectileBehavior.SetAttackData(GetAttackData());
        projectileBehavior.DirectionChecker(toMouse.normalized);
        projectileBehavior.RotateToDirection(mouseIndicator.rotationToMouse);
    }
}
