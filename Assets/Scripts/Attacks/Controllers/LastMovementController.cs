using UnityEngine;

public class LastMovementController : AttackController
{
    protected override void LaunchAttack()
    {
        GameObject attack = Instantiate(attackStats.BaseStats.Prefab);
        attack.transform.position = transform.position;
        
        Vector3 dir = GameWorld.Instance.GetPlayerController.LastMovementDirection;
        float rotZ =  Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        ProjectileBehavior projectileBehavior = attack.GetComponent<ProjectileBehavior>();
        
        projectileBehavior.SetAttackData(GetAttackData());
        projectileBehavior.DirectionChecker(dir.normalized);
        projectileBehavior.RotateToDirection(Quaternion.Euler(0, 0, rotZ));
    }
}
