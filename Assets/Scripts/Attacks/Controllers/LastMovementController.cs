using UnityEngine;

public class LastMovementController : AttackController
{
    protected override void LaunchAttack()
    {
        Vector3 dir = playerController.LastMovementDirection;
        float rotZ =  Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        InstantiateAttack(dir.normalized, Quaternion.Euler(0, 0, rotZ));
    }
}
