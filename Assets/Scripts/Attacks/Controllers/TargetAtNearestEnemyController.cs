using UnityEngine;

public class TargetAtNearestEnemyController : AttackController
{
    protected override void LaunchAttack()
    {
        GameObject nearestEnemy = GameWorld.Instance.GetNearestEnemyWithinRange(attackStats.currentRange);
        if (nearestEnemy != null)
        {
            Vector2 toEnemy = nearestEnemy.transform.position - transform.position;
            float rotZ =  Mathf.Atan2(toEnemy.y, toEnemy.x) * Mathf.Rad2Deg;

            InstantiateAttack(toEnemy, Quaternion.Euler(0, 0, rotZ));
        }
    }
}
