public class AttackData
{
    public float damage;
    public float pierce;
    public float speed;
    public float lifespan;
    public float critDmg;
    public float range;
    public ElementalType element;

    public AttackData(ElementalType element, float damage, float pierce, float speed, float lifespan, float range)
    {
        this.damage = damage;
        this.pierce = pierce;
        this.speed = speed;
        this.lifespan = lifespan;
        this.element = element;
        this.range = range;
        critDmg = 1;
    }
}
