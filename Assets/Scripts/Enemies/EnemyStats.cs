using UnityEngine;

public class EnemyStats : GenericStats
{
    private EnemyScriptableObject baseData;
    public EnemyScriptableObject BaseData {get => baseData;}

    private DamageIndicator damageIndicator;

    protected override void Awake()
    {
        base.Awake();
        baseData = (EnemyScriptableObject) genericData;
        damageIndicator = GetComponent<DamageIndicator>();
    }

    protected override void Damage(float damage)
    {
        base.Damage(damage);
        damageIndicator.ShowDamage(damage);
    }

    protected override void Kill() 
    {
        GetComponentInParent<LootTable>().DropLoot();
        Destroy(gameObject);
    }
}
