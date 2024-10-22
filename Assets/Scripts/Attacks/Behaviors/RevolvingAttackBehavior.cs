using System;
using UnityEngine;

public class RevolvingAttackBehavior : ProjectileBehavior
{
    [HideInInspector] public float currentAngle;

    protected override void ReducePierce() 
    {

    }

    // some dumb code lol
    void FixedUpdate() {
        float x = Mathf.Cos(currentAngle);
        float y = Mathf.Sin(currentAngle);

        direction = new Vector3(x, y, 0f) * attackData.range;
        Vector3 perp = new(-y, x);

        transform.position = direction + GameWorld.Instance.playerTransform.position;
        transform.position += attackData.speed * Time.deltaTime * perp;

        Vector3 newDir = transform.position - GameWorld.Instance.playerTransform.position;

        currentAngle = Mathf.Atan2(newDir.y, newDir.x);

        RotateToDirection(Quaternion.Euler(0, 0, Mathf.Atan2(perp.y, perp.x) * Mathf.Rad2Deg));
    }
}
