using UnityEngine;

public class EnemyStats : GenericStats
{
    public EnemyScriptableObject BaseData {get => (EnemyScriptableObject) genericData; }

    [SerializeField]
    private DamageIndicator damageIndicator;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Damage(float damage)
    {
        base.Damage(damage);
        damageIndicator.ShowDamage(damage);
    }

    protected override void Kill() 
    {
        Destroy(gameObject);
    }
}
