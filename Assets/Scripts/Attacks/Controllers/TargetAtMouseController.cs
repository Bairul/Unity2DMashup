using UnityEngine;

/// <summary>
/// To be placed as a new child object in the Player object
/// </summary>
public class TargetAtMouseController : AttackController
{
    private MouseIndicator mouseIndicator;

    void Start()
    {
        mouseIndicator = playerController.PlayerMouseIndicator;
    }

    protected override void LaunchAttack()
    {
        Vector3 toMouse = new(mouseIndicator.mouseDir.x, mouseIndicator.mouseDir.y);

        InstantiateAttack(toMouse.normalized, mouseIndicator.rotationToMouse);
    }
}
