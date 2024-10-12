using UnityEngine;

public class EnemyStats : GenericStats
{
    private EnemyScriptableObject baseStats;
    public EnemyScriptableObject BaseStats {get => baseStats;}

    private DamageIndicator damageIndicator;

    protected override void Awake()
    {
        base.Awake();
        baseStats = (EnemyScriptableObject) genericStats;
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
