using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBoltBehavior : AttackBehavior
{
    ArcaneBoltController controller;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        controller = FindObjectOfType<ArcaneBoltController>();
    }

    void FixedUpdate() {
        transform.position += controller.speed * Time.deltaTime * direction;
    }
}
