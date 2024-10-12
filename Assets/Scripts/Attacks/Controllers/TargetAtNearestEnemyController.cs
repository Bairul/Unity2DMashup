using UnityEngine;

public class TargetAtNearestEnemyController : AttackController
{
    protected override void LaunchAttack()
    {
        GameObject nearestEnemy = GameWorld.Instance.GetNearestEnemyWithinRange(attackStats.currentRange);
        if (nearestEnemy != null)
        {
            GameObject attack = Instantiate(attackStats.BaseStats.Prefab);
            attack.transform.position = transform.position;

            Vector2 toEnemy = nearestEnemy.transform.position - attack.transform.position;
            float rotZ =  Mathf.Atan2(toEnemy.y, toEnemy.x) * Mathf.Rad2Deg;

            ProjectileBehavior projectileBehavior = attack.GetComponent<ProjectileBehavior>();
        
            projectileBehavior.SetAttackData(attackStats.ToAttackData());
            projectileBehavior.DirectionChecker(toEnemy.normalized);
            projectileBehavior.RotateToDirection(Quaternion.Euler(0, 0, rotZ));
        }
    }
}
