using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBoltBehavior : ProjectileBehavior
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    void FixedUpdate() {
        transform.position += attackStats.currentSpeed * Time.deltaTime * direction;
    }
}
