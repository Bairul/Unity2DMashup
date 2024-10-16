using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolvePlayerController : AttackController
{
    private float lifespanTimer;

    protected override void Awake()
    {
        base.Awake();
        lifespanTimer = attackStats.currentLifespan;
    }

    protected override void LateUpdate()
    {
        lifespanTimer -= Time.deltaTime;

        if (lifespanTimer <= 0 && attackStats.CanAttack())
        {
            LaunchAttack();
            lifespanTimer = attackStats.currentLifespan;
        }
    }

    protected override void LaunchAttack()
    {
        float ang = 2 * Mathf.PI / attackStats.currentCount;

        for (int i = 0; i < attackStats.currentCount; i++)
        {
            GameObject attack = Instantiate(attackStats.BaseStats.Prefab);
            attack.transform.position = transform.position;

            float angle = i * ang;
            Vector3 displace = new(Mathf.Cos(angle), Mathf.Sin(angle));
            displace *= attackStats.currentRange;

            attack.transform.position += displace;

            RevolvingAttackBehavior attackBehavior = attack.GetComponent<RevolvingAttackBehavior>();
        
            attackBehavior.SetAttackData(GetAttackData());
            attackBehavior.currentAngle = angle;
        }
    }
}
