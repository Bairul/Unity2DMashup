using UnityEngine;

public class EnemyStats : GenericStats
{
    private EnemyScriptableObject baseData;
    public EnemyScriptableObject BaseData {get => baseData;}

    [SerializeField]
    private DamageIndicator damageIndicator;

    protected override void Awake()
    {
        base.Awake();
        baseData = (EnemyScriptableObject) genericData;
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
