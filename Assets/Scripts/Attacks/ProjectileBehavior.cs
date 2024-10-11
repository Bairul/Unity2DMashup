using UnityEngine;

/// <summary>
/// Put behaviors in the attack's prefab
/// </summary>
public class ProjectileBehavior : MonoBehaviour
{
    protected AttackStats attackStats;
    protected Vector3 direction;

    protected virtual void Awake()
    {
        attackStats = GetComponent<AttackStats>();
    }

    protected virtual void Start()
    {
        Destroy(gameObject, attackStats.BaseData.Lifespan);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("EnemyHitbox"))
        {
            EnemyStats enemy = collider.GetComponentInParent<EnemyStats>();
            enemy.TakeDamage(attackStats.currentDamage);
            attackStats.ReducePierce();
        }
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
    }

    public void RotateToDirection(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void DamageMultiplier()
    {

    }
}
