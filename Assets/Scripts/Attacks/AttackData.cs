public class AttackData
{
    public float damage;
    public float pierce;
    public float speed;
    public float lifespan;
    public float critDmg;
    public float range;

    public AttackData(float damage, float pierce, float speed, float lifespan)
    {
        this.damage = damage;
        this.pierce = pierce;
        this.speed = speed;
        this.lifespan = lifespan;
        critDmg = 1;
        range = 0;
    }

    public AttackData(float damage, float pierce, float speed, float lifespan, float range)
    {
        this.damage = damage;
        this.pierce = pierce;
        this.speed = speed;
        this.lifespan = lifespan;
        critDmg = 1;
        this.range = range;
    }
}
