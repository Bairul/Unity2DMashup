using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBoltController : AttackController
{
    public Transform target;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    private protected override void LaunchAttack()
    {
        base.LaunchAttack();
        GameObject arcaneBolt = Instantiate(attackStats.baseData.Prefab);
        arcaneBolt.transform.position = transform.position;
        arcaneBolt.GetComponent<ArcaneBoltBehavior>().DirectionChecker((target.position - transform.position).normalized);
    }
}
