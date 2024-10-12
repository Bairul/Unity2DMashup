using UnityEngine;

/// <summary>
/// Moves straight in a direction
/// </summary>
public class StraightProjectileBehavior : ProjectileBehavior
{
    protected override void Start()
    {
        base.Start();
    }

    void FixedUpdate() {
        transform.position += attackData.speed * Time.deltaTime * direction;
    }
}
