using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBoltController : AttackController
{
    [SerializeField]
    private MouseIndicator mouseIndicator;

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
        
        Vector3 toMouse = new(mouseIndicator.mouseDir.x, mouseIndicator.mouseDir.y);
        arcaneBolt.GetComponent<ArcaneBoltBehavior>().DirectionChecker(toMouse.normalized);
    }
}
