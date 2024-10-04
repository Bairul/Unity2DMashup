using UnityEngine;

/// <summary>
/// To be placed as a new child object in the Player object
/// </summary>
public class ArcaneBoltController : AttackController
{
    [SerializeField]
    private MouseIndicator mouseIndicator;

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
