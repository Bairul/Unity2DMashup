using UnityEngine;

/// <summary>
/// Moves straight in a direction
/// </summary>
public class ArcaneBoltBehavior : ProjectileBehavior
{
    protected override void Start()
    {
        base.Start();
    }

    void FixedUpdate() {
        transform.position += attackStats.currentSpeed * Time.deltaTime * direction;
    }
}
