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

    public void TakeDamage(AttackData attackData) 
    {
        damageIndicator.isCrit = attackData.critDmg > 1;
        float damage = attackData.damage * attackData.critDmg;
        TakeDamage(damage);
    }

    protected override void DamageTaken(float damage)
    {
        base.DamageTaken(damage);
        damageIndicator.ShowDamage(damage);
    }

    protected override void Kill() 
    {
        GetComponentInParent<LootTable>().DropLoot();
        Destroy(gameObject);
    }
}
