using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    protected AttackData attackData;

    protected virtual void Start()
    {
        if (attackData == null)
        {
            Debug.LogError("Null attack data in " + gameObject.name);
        }
        Destroy(gameObject, attackData.lifespan);
    }

    public void SetAttackData(AttackData data)
    {
        attackData = data;
    }

    protected virtual void ReducePierce() 
    {
        attackData.pierce--;

        if (attackData.pierce <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (attackData.pierce > 0 && collider.CompareTag("EnemyHitbox"))
        {
            EnemyStats enemy = collider.GetComponentInParent<EnemyStats>();
            enemy.TakeDamage(attackData);
            ReducePierce();
        }
    }
}
