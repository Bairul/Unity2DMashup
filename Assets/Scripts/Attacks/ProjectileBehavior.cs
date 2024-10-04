using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField]
    protected AttackStats attackStats;
    protected Vector3 direction;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, attackStats.baseData.Lifespan);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Enemy"))
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