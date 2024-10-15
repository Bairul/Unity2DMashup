using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolvePlayerController : AttackController
{
    private RevolveAttackStats revolveAttackStats;
    private float lifespanTimer;

    protected override void Awake()
    {
        base.Awake();
        revolveAttackStats = (RevolveAttackStats) attackStats;
        lifespanTimer = revolveAttackStats.currentLifespan;
    }

    protected override void LateUpdate()
    {
        lifespanTimer -= Time.deltaTime;

        if (lifespanTimer <= 0 && attackStats.CanAttack())
        {
            LaunchAttack();
            lifespanTimer = revolveAttackStats.currentLifespan;
        }
    }

    protected override AttackData GetAttackData()
    {
        AttackData attackData = base.GetAttackData();
        attackData.range = attackStats.currentRange;
        return attackData;
    }

    protected override void LaunchAttack()
    {
        float ang = 2 * Mathf.PI / revolveAttackStats.revolveCount;

        for (int i = 0; i < revolveAttackStats.revolveCount; i++)
        {
            GameObject attack = Instantiate(attackStats.BaseStats.Prefab);
            attack.transform.position = transform.position;

            float angle = i * ang;
            Vector3 displace = new(Mathf.Cos(angle), Mathf.Sin(angle));
            displace *= revolveAttackStats.currentRange;

            attack.transform.position += displace;

            RevolvingAttackBehavior attackBehavior = attack.GetComponent<RevolvingAttackBehavior>();
        
            attackBehavior.SetAttackData(GetAttackData());
            attackBehavior.currentAngle = angle;
        }
    }
}
