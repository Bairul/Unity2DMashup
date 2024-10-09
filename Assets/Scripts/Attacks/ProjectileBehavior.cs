using UnityEngine;

/// <summary>
/// Put behaviors in the attack's prefab
/// </summary>
public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField]
    protected AttackStats attackStats;
    protected Vector3 direction;

    protected virtual void Start()
    {
        Destroy(gameObject, attackStats.baseData.Lifespan);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("EnemyHitbox"))
        {
            EnemyStats enemy = collider.GetComponentInParent<EnemyStats>();
            enemy.TakeDamage(attackStats.currentDamage);
            attackStats.ReducePierce();
        }
    }

    public void DirectionChecker(Vector3 dir) {
        direction = new (dir.x, dir.y, dir.z);
    }

    public void DamageMultiplier() {

    }
}
