using UnityEngine;

/// <summary>
/// Put behaviors in the attack's prefab
/// </summary>
public class ProjectileBehavior : AttackBehavior
{
    protected Vector3 direction;

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
    }

    public void RotateToDirection(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}
